using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GravityField : MonoBehaviour
{
    //Main Properties

    public Type type = Type.Spherical;

    //Values

    public float range = 10;
    public float lenght = 10;
    public float width = 10;
    public float groundHeigh = 20;

    public float intensity = 3000;

    //Properties

    public float totalRange { get { return range + groundHeigh; } }
    public Vector3 center { get { return transform.position; } }
    #region Corners
    public Vector3 corner0 { get { return center + new Vector3(width*.5f,range*.5f,lenght*0.5f); } }
    public Vector3 corner1 { get { return center + new Vector3(-width*.5f,range*.5f,lenght*0.5f); } }
    public Vector3 corner2 { get { return center + new Vector3(-width*.5f,-range*.5f,lenght*0.5f); } }
    public Vector3 corner3 { get { return center + new Vector3(width*.5f,-range*.5f,lenght*0.5f); } }
    public Vector3 corner4 { get { return center + new Vector3( width * .5f , range * .5f , -lenght * 0.5f ); } }
    public Vector3 corner5 { get { return center + new Vector3( -width * .5f , range * .5f , -lenght * 0.5f ); } }
    public Vector3 corner6 { get { return center + new Vector3( -width * .5f , -range * .5f , -lenght * 0.5f ); } }
    public Vector3 corner7 { get { return center + new Vector3( width * .5f , -range * .5f , -lenght * 0.5f ); } }
    #endregion
    public Vector3 size { get { return new Vector3(width , range , lenght ); } set { width = value.x; range = value.y;lenght = value.z; } }

    public Vector3 GravityAt(Vector3 _position )
    {
        switch (type)
        {
            default: return Vector3.zero;
            case Type.Spherical:
                Vector3 d = (center - _position);
                return (d.magnitude <= 0 || d.magnitude > totalRange) ? Vector3.zero : ( d.normalized * intensity ) / Mathf.Pow( d.magnitude , 2f );
        }
    }

    private void OnDrawGizmos ()
    {
        Gizmos.color = new IColor( Color.green , .1f , 1 , 1 );

        switch (type)
        {
            default:break;
            case Type.Spherical:
                Gizmos.DrawSphere( center , totalRange );
                break;
            case Type.Flat:
                Gizmos.DrawCube( center , new Vector3(width,range,lenght) );
                break;
        }        
    }

    public enum Type
    {
        Spherical,
        Flat
    }
    static int TypeCount = 2;

    public void SwitchType ( int add )
    {
        type += add;
        if((int)type >= TypeCount) { type = 0; }
        if((int)type < 0) { type = ( Type)( TypeCount - 1); }
    }

    public static string TypeString(Type t )
    {
        switch (t)
        {
            default: return "";
            case Type.Flat: return "Flat";
            case Type.Spherical: return "Spherical";
        }
    }
}
#if UNITY_EDITOR

[CustomEditor( typeof( GravityField ) )]
[CanEditMultipleObjects]
public class GravityFieldEditor : Editor
{
    GUIStyle titleStyle;
    float titleHeight = 50; int titleFontSize = 25;

    GUIStyle enumStyle;
    float enumHeight = 20;  int enumFontSize = 25;

    GUIStyle arrowStyle;
    int arrowFontSize = 40;

    void Styles ()
    {
        titleStyle = GUI.skin.label; titleStyle.alignment = TextAnchor.MiddleCenter; titleStyle.fontStyle = FontStyle.Bold; titleStyle.fontSize = titleFontSize;
        arrowStyle = GUI.skin.label; arrowStyle.alignment = TextAnchor.MiddleCenter; arrowStyle.fontStyle = FontStyle.Bold; arrowStyle.fontSize = arrowFontSize;
        enumStyle = GUI.skin.label; enumStyle.alignment = TextAnchor.MiddleCenter; enumStyle.fontStyle = FontStyle.Normal; enumStyle.fontSize = enumFontSize;
    }

    public override void OnInspectorGUI ()
    {
        Styles();

        var field = target as GravityField;

        serializedObject.Update();

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button( "<",arrowStyle ,GUILayout.Height(50)))    {  field.SwitchType( -1 );   }

        EditorGUILayout.LabelField(GravityField.TypeString(field.type) + " Field" , titleStyle , GUILayout.Height( titleHeight ) );

        if (GUILayout.Button( ">" , arrowStyle , GUILayout.Height( 50 ) ))    {  field.SwitchType( 1 );  }

        EditorGUILayout.EndHorizontal();

        switch (field.type)
        {
            default: break;
            case GravityField.Type.Flat:
                field.size = EditorGUILayout.Vector3Field("Size : ",field.size);               
                break;
            case GravityField.Type.Spherical:
                field.range = EditorGUILayout.FloatField( "Radius : ",field.range);
                field.groundHeigh = EditorGUILayout.FloatField( "Ground Radius : ",field.groundHeigh );
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}

#endif