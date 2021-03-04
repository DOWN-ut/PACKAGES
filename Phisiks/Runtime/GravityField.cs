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

    [SerializeField] private float _range = 10;
    [SerializeField] private float _lenght = 10;
    [SerializeField] private float _width = 10;
    [SerializeField]  private float _groundHeigh = 20;

    [SerializeField] private Vector2 _intensities = new Vector2(15,15);
    [SerializeField] private float _distancePow = 0;
    [SerializeField] private float _distanceLerper = 1;

    [SerializeField] private Vector3 _center;

    //Properties 

    public float totalRange { get { return _range + _groundHeigh; } }
    public Vector3 center { get { return transform.position + _center; } set { _center = value; } }
    public Vector2 intensity { get { return _intensities; } set { _intensities = value; } }
    #region Corners
    public Vector3 corner0 { get { return center + new Vector3(_width*.5f,_range*.5f,_lenght*0.5f); } }
    public Vector3 corner1 { get { return center + new Vector3(-_width*.5f,_range*.5f,_lenght*0.5f); } }
    public Vector3 corner2 { get { return center + new Vector3(-_width*.5f,-_range*.5f,_lenght*0.5f); } }
    public Vector3 corner3 { get { return center + new Vector3(_width*.5f,-_range*.5f,_lenght*0.5f); } }
    public Vector3 corner4 { get { return center + new Vector3( _width * .5f , _range * .5f , -_lenght * 0.5f ); } }
    public Vector3 corner5 { get { return center + new Vector3( -_width * .5f , _range * .5f , -_lenght * 0.5f ); } }
    public Vector3 corner6 { get { return center + new Vector3( -_width * .5f , -_range * .5f , -_lenght * 0.5f ); } }
    public Vector3 corner7 { get { return center + new Vector3( _width * .5f , -_range * .5f , -_lenght * 0.5f ); } }
    #endregion
    public Vector3 size { get { return new Vector3(_width , _range , _lenght ); } set { _width = value.x; _range = value.y;_lenght = value.z; } }
    public float range { get { return _range; } set { _range = value; } } public float lenght { get { return _lenght; } set { _lenght = value; } } public float width { get { return _width; } set { _width = value; } }  public float groundHeigh { get { return _groundHeigh; } set { _groundHeigh = value; } }

    float IntensityLerp(Vector2 _intens,float _dist,float _rang,float gheight,float _lerper )
    {
        return _lerper == 0 ? _intens.x : Mathf.Lerp( _intens.x , _intens.y , Mathf.Pow( ( _dist - gheight ) / ( _rang - gheight ) , _lerper ));
    }

    public Vector3 GravityAt(Vector3 _position )
    {
        Vector3 g = Vector3.zero;
        Vector3 d = (center - _position);
        Vector3 p = transform.InverseTransformPoint(_position);
        bool tooFar = Mathf.Abs(_position.x) > center.x + width || Mathf.Abs( _position.y ) > center.y + range || Mathf.Abs( _position.z ) > center.z + lenght;
        float dist = 0;
        float inte = 0;

        switch (type)
        {
            default: return Vector3.zero;
            case Type.Spherical:
                g = ( d.magnitude <= 0 || d.magnitude > totalRange ) ? Vector3.zero : d ;
                dist = d.magnitude;
                inte = IntensityLerp(intensity,dist,range,groundHeigh,_distanceLerper );
                break;
            case Type.Flat:
                g = tooFar ? Vector3.zero : -transform.up;
                dist = Mathf.Abs(p.y);
                inte = IntensityLerp( intensity , dist , range , 0 , _distanceLerper );
                break;
            case Type.TwoSided:
                g = tooFar ? Vector3.zero : p.y > 0 ? -transform.up : transform.up;
                dist = Mathf.Abs( p.y );
                inte = IntensityLerp( intensity , dist , range , 0 , _distanceLerper );
                break;
        }

        if(g.magnitude <= 0) { return Vector3.zero; }
        Vector3 r = (g.normalized * inte );
        return _distancePow == 0 ? r : (r / Mathf.Pow(Mathf.Max(dist,1) , _distancePow )); 
    }

    private void OnDrawGizmos ()
    {
        Gizmos.color = new IColor( Color.green , .1f , 1 , 1 );

        switch (type)
        {
            default:break;
            case Type.TwoSided: goto case Type.Flat;
            case Type.Spherical:
                Gizmos.DrawSphere( center , totalRange );
                break;
            case Type.Flat:
                Gizmos.DrawCube( center , size );
                break;
        }        
    }

    public enum Type
    {
        First = 0,
        Spherical = 1,
        Flat = 2,
        TwoSided = 3,
        Last
    }
    static int TypeCount = (Type.Last - Type.First) + 1;

    public void SwitchType ( int add )
    {
        int t = (int)type;
        SwitchType( ref t , add );
        type = (Type)t;
    }

    public static void SwitchType(ref int index, int add )
    {
        index += add;
        if ((int)index >= TypeCount - 1) { index = 1; }
        if ((int)index < 1) { index = TypeCount - 2; }
    }

    public static string TypeString(Type t )
    {
        switch (t)
        {
            default: return "";
            case Type.Flat: return "Flat";
            case Type.Spherical: return "Spherical";
            case Type.TwoSided: return "TwoSided";
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

        serializedObject.Update();

        var intensity = serializedObject.FindProperty( "_intensities" );
        var range = serializedObject.FindProperty( "_range" );
        var width = serializedObject.FindProperty( "_width" );
        var lenght = serializedObject.FindProperty( "_lenght" );
        var ground = serializedObject.FindProperty( "_groundHeigh" );
        var distancePow = serializedObject.FindProperty( "_distancePow" );
        var distanceLerp = serializedObject.FindProperty( "_distanceLerper" );

        EditorGUILayout.BeginHorizontal();

        int typeIndex = serializedObject.FindProperty( "type" ).enumValueIndex;

        if (GUILayout.Button( "<",arrowStyle ,GUILayout.Height(50)))    {  GravityField.SwitchType( ref typeIndex, -1 );   }

        EditorGUILayout.LabelField(GravityField.TypeString((GravityField.Type) typeIndex) + " Field" , titleStyle , GUILayout.Height( titleHeight ) );

        if (GUILayout.Button( ">" , arrowStyle , GUILayout.Height( 50 ) ))    { GravityField.SwitchType( ref typeIndex , 1 ); }

        serializedObject.FindProperty( "type" ).enumValueIndex = typeIndex;

        EditorGUILayout.EndHorizontal();

        intensity.vector2Value = EditorGUILayout.Vector2Field( "Intensity : " , intensity.vector2Value );
        distancePow.floatValue = EditorGUILayout.FloatField( "Distance Pow : " , distancePow.floatValue );
        distanceLerp.floatValue = EditorGUILayout.FloatField( "Distance Lerp Pow : " , distanceLerp.floatValue );

        switch ((GravityField.Type)typeIndex)
        {
            default: break;
            case GravityField.Type.TwoSided: goto case GravityField.Type.Flat;
            case GravityField.Type.Flat:
                Vector3 size = new Vector3(width.floatValue,range.floatValue,lenght.floatValue);
                size = EditorGUILayout.Vector3Field("Size : ", size );
                width.floatValue = size.x; range.floatValue = size.y; lenght.floatValue = size.z;
                break;
            case GravityField.Type.Spherical:
               range.floatValue = EditorGUILayout.FloatField( "Radius : " , range.floatValue );
                ground.floatValue = EditorGUILayout.FloatField( "Ground Radius : ", ground.floatValue );
                break;
        }

        serializedObject.ApplyModifiedProperties();

    }
}

#endif