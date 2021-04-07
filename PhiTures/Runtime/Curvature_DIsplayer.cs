using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curvature_DIsplayer : MonoBehaviour
{
    public Curvature curve;

    public int smoothness = 1;

    public Vector3[] pees { get { return curve.points.ToArray(); } }
    public Vector3[] medians { get { return Curvature.GetMedians(curve.points.ToArray()); } }
    public Vector3[] Step(int count, Vector3[] p) 
    {
        if (count <= 0) { return p; }
        else { return Step(count-1, Curvature.Smoother( p )); }
    }

    private void OnDrawGizmos ()
    {
        Gizmos.color = new Color(1,0,0,0.5f);

        foreach(Vector3 p in pees)
        {
            Gizmos.DrawSphere( p , 0.4f );
        }

        Gizmos.color = Color.cyan;

        foreach (Vector3 p in medians)
        {
            Gizmos.DrawSphere( p , 0.25f );
        }

        Gizmos.color = Color.yellow;

        Vector3[] step = Step(smoothness, curve.points.ToArray());

        Vector3 last = default;
        
        foreach (Vector3 p in step)
        {
            Gizmos.DrawSphere( p , 0.1f );
            
            Gizmos.DrawLine( last , p );
            
            last = p;
        }
    }
}
