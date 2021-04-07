using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor( typeof( Input_Profile ) )]
public class Input_Editor : Editor
{
    SerializedProperty serializedProperty;
    GUIStyle textfieldstyle; GUIStyle labelstyle; GUIStyle buttonStyle;

    public override void OnInspectorGUI ()
    {
        serializedObject.Update();
        Input_Profile profile = (Input_Profile)serializedObject.targetObject;

        textfieldstyle = new GUIStyle( GUI.skin.textField ); textfieldstyle.alignment = TextAnchor.MiddleCenter;
        labelstyle = new GUIStyle( GUI.skin.label ); labelstyle.alignment = TextAnchor.MiddleCenter;
        buttonStyle = new GUIStyle( GUI.skin.button ); buttonStyle.alignment = TextAnchor.MiddleCenter; buttonStyle.normal.background = Texture2D.blackTexture;

        /*
        string s = serializedObject.FindProperty( "layout" ).stringValue;
        string s2 = s == "qwerty" ? "azerty" : "qwerty";
        if (GUILayout.Button( s , buttonStyle , GUILayout.Width( 300 ) , GUILayout.Height( 20 ) )) { ( profile ).ChangeLayout( s2 ); }
        */
        SerializedProperty sp = serializedObject.FindProperty( "inputs" );
        EditorGUILayout.PropertyField( sp );

        sp = serializedObject.FindProperty( "_lookProfile" );
        EditorGUILayout.PropertyField( sp );

        sp = serializedObject.FindProperty( "lookSensibility" );
        EditorGUILayout.PropertyField( sp );

        serializedObject.ApplyModifiedProperties();
    }

    void DisplayMovement ()
    {
        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical();

        string[] movstrs = new string[4]{ "forward" , "rightward" , "leftward" , "backward" };
        for (int i = 0 ; i < 4 ; i++)
        {
            serializedProperty = serializedObject.FindProperty( movstrs[i] );

            if (i == 1) { GUILayout.BeginHorizontal(); }

            if (i == 0 || i == 3) { GUILayout.BeginHorizontal(); GUILayout.Space( 20 ); }

            if (i != 2) { EditorGUILayout.LabelField( movstrs[i] , labelstyle , GUILayout.Width( 60 ) , GUILayout.Height( 27 ) ); }

            serializedProperty.stringValue = EditorGUILayout.TextField( serializedProperty.stringValue , textfieldstyle , GUILayout.Width( 30 ) , GUILayout.Height( 27 ) );

            if (i == 2) { EditorGUILayout.LabelField( movstrs[i] , labelstyle , GUILayout.Width( 60 ) , GUILayout.Height( 27 ) ); }

            if (i == 0 || i == 3) { GUILayout.Space( 20 ); GUILayout.EndHorizontal(); }

            if (i == 2) { GUILayout.EndHorizontal(); }
        }

        GUILayout.EndVertical();
        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal(); EditProperty( serializedObject, "jump"); GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal(); EditProperty( serializedObject, "sneak" ); GUILayout.EndHorizontal();

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }

    void DisplayRest ()
    {
        string last = "sneak"; bool ok = false;
        foreach (System.Reflection.FieldInfo field in typeof( Input_Profile ).GetFields())
        {
            if (ok) { GUILayout.BeginHorizontal();

                if (field.Name == "lookSensibility")
                {
                    SerializedProperty sp = serializedObject.FindProperty( "lookSensibility" );
                    EditorGUILayout.PropertyField( sp );
                }
                else
                {
                    EditProperty( serializedObject , field.Name , 100 , 25 );
                }

                GUILayout.EndHorizontal(); }
            ok = !ok ? last == field.Name : ok;
        }
    }

    void EditProperty( SerializedObject serializedObject , string displayName , float width = 60 , float height = 27)
    {
        SerializedProperty sp = serializedObject.FindProperty( displayName );
        EditorGUILayout.LabelField( displayName , labelstyle , GUILayout.Width( width ) , GUILayout.Height( height ) );
        sp.stringValue = EditorGUILayout.TextField( sp.stringValue , textfieldstyle , GUILayout.Width( width ) , GUILayout.Height( height ) );
    }
}

[CustomPropertyDrawer( typeof( Input_Profile.InputElement ) )]
public class InputElementInspector : PropertyDrawer
{
    Texture2D t = Texture2D.whiteTexture;

    SerializedProperty NAME, KEY;
    string name;
    bool cache = false;

    public override float GetPropertyHeight ( SerializedProperty property , GUIContent label )
    {
        // The 6 comes from extra spacing between the fields (2px each)
        return EditorGUIUtility.singleLineHeight * 1 + 1;
    }

    public override void OnGUI ( Rect position , SerializedProperty property , GUIContent label )
    {
        //get the name before it's gone
        name = property.displayName;

        //get the X and Y values
        property.Next( true );
        NAME = property.Copy();
        property.Next( false );
        KEY = property.Copy();

        Rect contentPosition = EditorGUI.PrefixLabel(position, new GUIContent(""));

        //Check if there is enough space to put the name on the same line (to save space)
        if (position.height > 20f)
        {
            position.height = 20f;
            //EditorGUI.indentLevel += 1;
            contentPosition = EditorGUI.IndentedRect( position );
            //contentPosition.y += 18f;
            contentPosition.x += 0;
        }

        GUI.skin.label.padding = new RectOffset( 3 , 3 , 6 , 6 );

        //show the X and Y from the point
        EditorGUIUtility.labelWidth = 14f;

        contentPosition.width = 90;

        //EditorGUI.indentLevel = 0;

        // Begin/end property & change check make each field
        // behave correctly when multi-object editing.
        EditorGUI.BeginProperty( contentPosition ,new GUIContent("") , NAME );

        EditorGUI.PropertyField( contentPosition , NAME );

        contentPosition.x += contentPosition.width;

        EditorGUILayout.BeginHorizontal();
        for (int i = 0 ; i < KEY.arraySize ; i++)
        {
            EditorGUI.PropertyField( contentPosition , KEY.GetArrayElementAtIndex(i) );
            contentPosition.x += contentPosition.width * .85f;
        }
        contentPosition.x += contentPosition.width * .15f;
        contentPosition.width = 20;

        if (GUI.Button(contentPosition, "+" )) { KEY.arraySize++; }
        contentPosition.x += contentPosition.width;
        if (GUI.Button(contentPosition, "-" )) { KEY.arraySize--; }
        EditorGUILayout.EndHorizontal();

        EditorGUI.EndProperty();
    }

}
