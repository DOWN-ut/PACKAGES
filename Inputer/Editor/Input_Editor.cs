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

        textfieldstyle = new GUIStyle( GUI.skin.textField ); textfieldstyle.alignment = TextAnchor.MiddleCenter;
        labelstyle = new GUIStyle ( GUI.skin.label ); labelstyle.alignment = TextAnchor.MiddleCenter;
        buttonStyle = new GUIStyle ( GUI.skin.button ); buttonStyle.alignment = TextAnchor.MiddleCenter; buttonStyle.normal.background = Texture2D.blackTexture;

        string s = serializedObject.FindProperty( "layout" ).stringValue;
        string s2 = s == "qwerty" ? "azerty" : "qwerty"; 
        if (GUILayout.Button(s,buttonStyle, GUILayout.Width( 300 ) , GUILayout.Height( 20 ) )) { ((Input_Profile) serializedObject.targetObject).ChangeLayout( s2); }

        DisplayMovement();

        DisplayRest();

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
