using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LineArrayDisplayer : MonoBehaviour
{
    public LineArray<Element> array;
    public float width;

    public bool addLeft;
    public bool addRight;
    public bool addRandom;
    public bool addMiddle;
    public bool clear;

    public Element prefab;
    public Transform parent;

    public List<GameObject> visuals;
    public TextMesh text;
    public Transform line;

    private void Update ()
    {
        if (visuals == null) { visuals = new List<GameObject>(); }

        if (array == null) { array = new LineArray<Element>( width ); text.text = ""; }
        else if (array.empty) { array = new LineArray<Element>( width ); text.text = ""; }

        if (addLeft)
        {
            array.Add( Instance( prefab ) , prefab.width , LineArray<Element>.AddMode.Left );
            addLeft = false;
        }
        if (addRight)
        {
            array.Add( Instance( prefab ) , prefab.width , LineArray<Element>.AddMode.Right );
            addRight = false;
        }
        if (addMiddle)
        {
            array.Add( Instance( prefab ) , prefab.width , LineArray<Element>.AddMode.Center );
            addMiddle = false;
        }
        if (addRandom)
        {
            array.Add( Instance( prefab ) , prefab.width , LineArray<Element>.AddMode.Random );
            addRandom = false;
        }

        if (clear)
        {
            array = new LineArray<Element>( width );

            foreach (GameObject g in visuals) { DestroyImmediate( g ); }
            text.text = "";

            clear = false;
        }

        line.localScale = new Vector3( array.getWidth , 1 , 1 );

        float w = 0; int i = 0; string str = array.count.ToString() + " >>  ";
        foreach (LineArray<Element>.Element<Element> e in array.ToArray())
        {
            if (e.obj.obj == null) { array.RemoveAt( i ); break; }

            str += e.position.ToString( "F1" , '[' , ';' , ']' ) + "    ";

            w = e.position.bottom + ( e.obj.width / 2f );

            e.obj.obj.transform.position = new Vector3( w , 0 , 0 );

            w += e.obj.width / 2f;
            i++;
        }
        str += "\n" + array.getIntervals.Length.ToString("F0") + " >>  ";
        foreach(LineArray<Element>.intervalID a in array.getIntervals)
        {
            str += a.id + a.ToString( "F1" , '[' , ';' , ']') + "    ";
        }

        text.text = str;

    }

    Element Instance(Element e )
    {
        Element g = new Element(Instantiate(e.obj,parent),e.width);
        visuals.Add( g.obj );
        return g;
    }

    [System.Serializable]
    public class Element
    {
        public float width;
        public GameObject obj;

        public Element(GameObject g , float w ) { obj = g; width = w; }
    }
}
