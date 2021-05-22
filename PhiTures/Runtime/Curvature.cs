using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


[System.Serializable]
public class Curvature {

    public List<Vector3> points = new List<Vector3>();

    public List<Vector3> curve = new List<Vector3>();

    public Vector3[] curveArray { get { return curve.ToArray(); } }

    public void SetPoints(Vector3[] p )
    {
        points = new List<Vector3>( p );
    }

    public static Curvature FromPathes (Curvature[] froms,float tolerance = 1 , bool addPoints = false)
    {
        Curvature p = new Curvature();
        p.curve = new List<Vector3>();
        p.points = new List<Vector3>();

        foreach(Curvature c in froms)
        {
            foreach(Vector3 a in c.curve)
            {
                p.curve.Add( a );
            }
            if (addPoints)
            {
                foreach (Vector3 a in c.points)
                {
                    p.points.Add( a );
                }
            }
        }

        p.curve = new List<Vector3>( Clean( p.curve.ToArray() , tolerance ) );
        if (addPoints) { p.points = new List<Vector3>( Clean( p.points.ToArray() , tolerance ) ); }

        return p;
    }

    public static Vector3[] Smoother(Vector3[] pees )
    {
        if(pees.Length <= 2) { return pees; }

        pees = Clean( pees );

        Vector3[] medians = GetMedians(pees);

        int count = ((pees.Length-3)*2) + 4;
        Vector3[] npees = new Vector3[count];

        npees[0] = pees[0];
        npees[npees.Length-1] = pees[pees.Length - 1];

        for (int i = 0 ; i < npees.Length - 1; i++)
        {
            if(i == 0) { continue; }
            int y = i-1;

            int medID =  Mathf.FloorToInt( y/2f);
            int p1ID = Mathf.CeilToInt(y/2f);
            int p2ID = Mathf.CeilToInt( (y+1.1f) / 2f );

            //Debug.Log( p2ID );

            Vector3 med = medians[medID];
            Vector3 p1 = pees[p1ID];
            Vector3 p2 = pees[p2ID];

            npees[i] = TransformProcess.Average( p1,p2,med );
        }

        return npees;
    }

    public static Vector3[] Clean(Vector3[] pees, float tolerance = 1)
    {
        List<Vector3> npees = new List<Vector3>();
        npees.Add(pees[0]);

        Vector3 last = pees[0];
        for(int i = 1 ; i < pees.Length ; i++)
        {
            if( (pees[i] - last).magnitude > tolerance)
            {
                npees.Add( pees[i] );
                last = pees[i];
            }
        }

        return npees.ToArray();
    }

    public void Process ( int smoothSteps = 1 , float maxDistance = 100)
    {
        List<Vector3> precurve = new List<Vector3>();

        for(int i = 0 ; i < points.Count - 1 ; i++)
        {
            Vector3 d = (points[i+1] - points[i] );

            if (d.magnitude >= maxDistance)
            {
                Vector3 np = points[i];
                for (int z = 0 ; z < 20 && d.magnitude > maxDistance ; z++ )
                {
                    precurve.Add( np );
                    np += d.normalized * maxDistance;
                    d = (points[i + 1] - np);
                }
            }
            else
            {
                precurve.Add( points[i] );
            }
        }
        precurve.Add( points[points.Count -1] );

        for (int i = 0 ; i < smoothSteps ; i++)
        {
            Vector3[] pre = Smoother( precurve.ToArray());
            precurve = new List<Vector3>( pre );
        }

        curve = precurve;
    }

    public static Vector3[] GetMedians ( Vector3[] pees )
    {
        if(pees.Length <= 2) { return new Vector3[0]; }

        Vector3[] medians = new Vector3[pees.Length-2];

        for (int i = 0 ; i + 2 < pees.Length && i < medians.Length; i++)
        {
            medians[i] = TransformProcess.Average( pees[i] , pees[i + 1] , pees[i + 2] );
        }

        return medians;
    }


    public static implicit operator Vector3[] ( Curvature o ) { return o.curve.ToArray(); }
}

#if UNITY_EDITOR

#endif
