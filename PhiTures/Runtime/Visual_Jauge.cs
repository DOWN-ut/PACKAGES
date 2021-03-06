using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class Visual_Jauge : MonoBehaviour
{
    //Values
    [SerializeField]private float _value; public float value { get { return _value; } set { _value = value > _max ? _max : value < _min ? _min : value; } }
    [SerializeField]private float _min; public float min { get { return _min; } set { _min = value > _max ? _max : value; } }
    [SerializeField]private float _max; public float max { get { return _max; } set { _max = value < _min ? _min : value; } }

    //Transform-controlled jauges

    public Transform[] transformsRefs;
    public Vector3[] transformsMins;
    public Vector3[] transformsMaxs;
    public float[] transformsSpeeds;

    //UI-controlled jauges

    public Slider[] slidersRef;
    public float[] slidersSpeeds;

    //Parameters

    public float updateDelay = .05f;

    //Ingame

    public Coroutine updateCoroutine;

    private void Awake ()
    {
        Correct();
    }

    public bool Correct ()
    {
        bool r = true;

        if (transformsMaxs.Length < transformsRefs.Length)
        {
            Vector3[] t = new Vector3[transformsRefs.Length];
            for (int i = 0 ; i < t.Length ; i++)
            {
                t[i] = transformsMaxs[i];
            }
            transformsMaxs = t; r = false;
        }
        if (transformsMins.Length < transformsRefs.Length)
        {
            Vector3[] t = new Vector3[transformsRefs.Length];
            for (int i = 0 ; i < t.Length ; i++)
            {
                t[i] = transformsMins[i];
            }
            transformsMins = t; r = false;
        }
        if (slidersSpeeds.Length < slidersRef.Length)
        {
            float[] t = new float[slidersRef.Length];
            for (int i = 0 ; i < t.Length ; i++)
            {
                t[i] = slidersSpeeds[i];
            }
            slidersSpeeds = t; r = false;
        }

        return r;
    }

    public float GetRatio ( float val )
    {
        return ( _max == _min ) ? 1 : ( val - _min ) / ( _max - _min );
    }

    public void UpdateVisuals ( float val )
    {
        float r = GetRatio(val);

        for (int i = 0 ; i < transformsRefs.Length ; i++)
        {
            transformsRefs[i].localScale = Vector3.MoveTowards( transformsRefs[i].localScale , Vector3.Lerp( transformsMins[i] , transformsMaxs[i] , r ) , Time.deltaTime * transformsSpeeds[i] );
        }
        for (int i = 0 ; i < slidersRef.Length ; i++)
        {
            slidersRef[i].maxValue = max;
            slidersRef[i].minValue = min;
            slidersRef[i].value = Mathf.MoveTowards( slidersRef[i].value , val , Time.deltaTime * slidersSpeeds[i] );
        }
    }

    public void SetValue ( float val )
    {
        value = val;

        if (updateCoroutine != null) { StopCoroutine( updateCoroutine ); }

        updateCoroutine = StartCoroutine( Updater( value , updateDelay ) );
    }

    public Vector3 GetTransformVector ( float val , int index )
    {
        return Vector3.Lerp( transformsMins[index] , transformsMaxs[index] , GetRatio( val ) );
    }

    IEnumerator Updater ( float val , float delay )
    {
        while (!Test( val ))
        {
            UpdateVisuals( val );
            yield return new WaitForSeconds( delay );
        }
        yield return null;
    }

    public bool Test ( float val )
    {
        for (int i = 0 ; i < transformsRefs.Length ; i++)
        {
            if (transformsRefs[i].localScale != GetTransformVector( val , i )) { return false; }
        }
        for (int i = 0 ; i < slidersRef.Length ; i++)
        {
            if (slidersRef[i].value != val) { return false; }
        }

        return true;
    }

    public enum Type
    {

    }
}

#if UNITY_EDITOR

[CustomEditor( typeof( Visual_Jauge ) )]
[CanEditMultipleObjects]
public class Visual_JaugeEditor : Editor
{
    GUIStyle titleStyle;
    float titleHeight = 50; int titleFontSize = 25;

    GUIStyle enumStyle;
    float enumHeight = 20; int enumFontSize = 25;

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
        float width = EditorGUIUtility.currentViewWidth;

        Styles();

        serializedObject.Update();

        var value = serializedObject.FindProperty("_value");
        var max = serializedObject.FindProperty("_max");
        var min = serializedObject.FindProperty("_min");
        var transformsRefs = serializedObject.FindProperty("transformsRefs");
        var transformsMaxs = serializedObject.FindProperty("transformsMaxs");
        var transformsMins = serializedObject.FindProperty("transformsMins");
        var transformsSpeeds = serializedObject.FindProperty("transformsSpeeds");
        var slidersRef = serializedObject.FindProperty("slidersRef");
        var slidersSpeeds = serializedObject.FindProperty("slidersSpeeds");
        var updateDelay = serializedObject.FindProperty("updateDelay");

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField( "MIN ", GUILayout.Width( width/3f ) ); EditorGUILayout.LabelField( "-- value --" , GUILayout.Width( width / 3f ) ); EditorGUILayout.LabelField( " MAX" , GUILayout.Width( width / 3f ) );
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        min.floatValue = EditorGUILayout.FloatField( min.floatValue );
        value.floatValue = EditorGUILayout.FloatField( value.floatValue );
        max.floatValue = EditorGUILayout.FloatField( max.floatValue );

        EditorGUILayout.EndHorizontal();

        GUING.Line( 1 , 1 );

        EditorGUILayout.BeginHorizontal();
        transformsRefs.arraySize = EditorGUILayout.IntField( transformsRefs.arraySize, GUILayout.Width( width * .2f ) );
        transformsMaxs.arraySize = transformsRefs.arraySize; transformsMins.arraySize = transformsRefs.arraySize; transformsSpeeds.arraySize = transformsRefs.arraySize;
        EditorGUILayout.Space(); EditorGUILayout.LabelField( "Min Scale" , GUILayout.Width( width * .25f ) );
        EditorGUILayout.Space(); EditorGUILayout.LabelField( "Max Scale" , GUILayout.Width( width * .25f ) );
        EditorGUILayout.Space(); EditorGUILayout.LabelField( "Move Speed" , GUILayout.Width( width * .25f ) );
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        for (int i = 0 ; i < transformsRefs.arraySize ; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField( "°" , GUILayout.Width( width * .025f ) );
            EditorGUILayout.PropertyField( transformsRefs.GetArrayElementAtIndex( i ),GUIContent.none,GUILayout.Width(width * .2f) );
            EditorGUILayout.Space();
            transformsMins.GetArrayElementAtIndex( i ).vector3Value = EditorGUILayout.Vector3Field("", transformsMins.GetArrayElementAtIndex( i ).vector3Value,GUILayout.Width(width * .25f) );
            EditorGUILayout.Space();
            transformsMaxs.GetArrayElementAtIndex( i ).vector3Value = EditorGUILayout.Vector3Field("", transformsMaxs.GetArrayElementAtIndex( i ).vector3Value,GUILayout.Width(width * .25f) );
            EditorGUILayout.Space();
            transformsSpeeds.GetArrayElementAtIndex( i ).floatValue = EditorGUILayout.FloatField( transformsSpeeds.GetArrayElementAtIndex( i ).floatValue,GUILayout.Width(width * .075f) );
            EditorGUILayout.EndHorizontal();
        }

        /*
        EditorGUILayout.PropertyField( transformsRefs,GUILayout.Width( width) ); EditorGUILayout.Space();
        transformsMaxs.arraySize = transformsRefs.arraySize; transformsMins.arraySize = transformsRefs.arraySize; transformsSpeeds.arraySize = transformsRefs.arraySize;
        EditorGUILayout.PropertyField( transformsMaxs , GUILayout.Width( width ) ); EditorGUILayout.Space();
        EditorGUILayout.PropertyField( transformsMins , GUILayout.Width( width ) ); EditorGUILayout.Space();
        EditorGUILayout.PropertyField( transformsSpeeds , GUILayout.Width( width ) );
        */


        serializedObject.ApplyModifiedProperties();

    }
}  

#endif
