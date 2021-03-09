using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorMethods
{
    public static System.Type GetType ( SerializedProperty property )
    {
        System.Type parentType = property.serializedObject.targetObject.GetType();
        System.Reflection.FieldInfo fi = parentType.GetField(property.propertyPath);
        return fi.FieldType;
    }
}

[CustomPropertyDrawer( typeof( coefficient ) )]
public class CoefficientInspector : PropertyDrawer
{
    Texture2D t = Texture2D.whiteTexture;

    SerializedProperty X, Y;
    string name;
    bool cache = false;

    public override float GetPropertyHeight ( SerializedProperty property , GUIContent label )
    {
        // The 6 comes from extra spacing between the fields (2px each)
        return EditorGUIUtility.singleLineHeight * 1 + 1;
    }

    public override void OnGUI ( Rect position , SerializedProperty property , GUIContent label )
    {
        if (!cache)
        {
            //get the name before it's gone
            name = property.displayName;

            //get the X and Y values
            property.Next( true );
            X = property.Copy();
            property.Next( true );
            Y = property.Copy();

            cache = true;
        }

        Rect contentPosition = EditorGUI.PrefixLabel(position, new GUIContent(name));

        //Check if there is enough space to put the name on the same line (to save space)
        if (position.height > 20f)
        {
            position.height = 20f;
            //EditorGUI.indentLevel += 1;
            contentPosition = EditorGUI.IndentedRect( position );
            //contentPosition.y += 18f;
            contentPosition.x += 150f;
        }

        GUI.skin.label.padding = new RectOffset( 3 , 3 , 6 , 6 );

        //show the X and Y from the point
        EditorGUIUtility.labelWidth = 14f;
        contentPosition.width = 50f;
        //EditorGUI.indentLevel = 0;

        // Begin/end property & change check make each field
        // behave correctly when multi-object editing.
        EditorGUI.BeginProperty( contentPosition , label , X );
        {
            EditorGUI.BeginChangeCheck();
            float newVal = EditorGUI.FloatField(contentPosition, new GUIContent(""), X.floatValue);
            if (EditorGUI.EndChangeCheck())
                X.floatValue = newVal;
        }
        EditorGUI.EndProperty();

        contentPosition.x += 50;

        EditorGUI.BeginProperty( contentPosition , new GUIContent( "" ) , Y );
        {
            EditorGUI.LabelField( new Rect( 210 , contentPosition.y , 150 , 20 ) , new GUIContent( "| override" ) );
            contentPosition.x += 70;
            EditorGUI.BeginChangeCheck();
            bool newVal = EditorGUI.Toggle(contentPosition, new GUIContent( "" ), Y.boolValue);
            if (EditorGUI.EndChangeCheck())
                Y.boolValue = newVal;
        }
        EditorGUI.EndProperty();

        contentPosition.x += 50;
        if (GUI.Button( contentPosition , new GUIContent( "Reset" ) )) { X.floatValue = coefficient.Default.value; Y.boolValue = coefficient.Default.overide; }
    }
}

[CustomPropertyDrawer( typeof( small ) )]
public class SmallInspector : PropertyDrawer
{
    Texture2D t = Texture2D.whiteTexture;
    public override float GetPropertyHeight ( SerializedProperty property , GUIContent label )
    {
        // The 6 comes from extra spacing between the fields (2px each)
        return EditorGUIUtility.singleLineHeight * 1 + 1;
    }

    public override void OnGUI ( Rect position , SerializedProperty property , GUIContent label )
    {
        EditorGUI.BeginProperty( position , label , property );

        EditorGUI.LabelField( position , label );

        var smallRect = new Rect( position.x + 100, position.y, 5, 5 );

        //t.Resize( 1 , 1 );

        //EditorGUI.PropertyField( smallRect , property.FindPropertyRelative( "v" ) ,new GUIContent(t),false);

        EditorGUI.EndProperty();
    }
}

[CustomPropertyDrawer( typeof( trilean ) )]
public class TrileanInspector : PropertyDrawer
{
    Texture2D t = Texture2D.whiteTexture;

    SerializedProperty X, Y;
    string name;
    bool cache = false;

    public override float GetPropertyHeight ( SerializedProperty property , GUIContent label )
    {
        // The 6 comes from extra spacing between the fields (2px each)
        return EditorGUIUtility.singleLineHeight * 1 + 1;
    }

    public override void OnGUI ( Rect position , SerializedProperty property , GUIContent label )
    {
        EditorGUI.BeginProperty( position , label , property );

        EditorGUI.LabelField(new Rect( 19 , position.position.y , 300 , position.height ) , property.displayName );

        property.Next(true);
        int value = property.intValue;
        string str = value == 0 ? "Null" : value == 1 ? "True" : "False";
        if (GUI.Button( new Rect( position.width / 2.35f , position.position.y , 70 , position.height ) , str )) { property.intValue = value == 0 ? 1 : value == 1 ? -1 : 0; };

        var smallRect = new Rect( position.x + 100, position.y, 5, 5 );

        //t.Resize( 1 , 1 );

        //EditorGUI.PropertyField( smallRect , property.FindPropertyRelative( "v" ) ,new GUIContent(t),false);

        EditorGUI.EndProperty();
    }
}

public class ReadOnlyAttribute : PropertyAttribute
{

}

[CustomPropertyDrawer( typeof( ReadOnlyAttribute ) )]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight ( SerializedProperty property ,
                                            GUIContent label )
    {
        return EditorGUI.GetPropertyHeight( property , label , true );
    }

    public override void OnGUI ( Rect position ,
                               SerializedProperty property ,
                               GUIContent label )
    {
        GUI.enabled = false;
        EditorGUI.PropertyField( position , property , label , true );
        GUI.enabled = true;
    }
}

[CustomPropertyDrawer( typeof( interval ) )]
public class IntervalDrawer : PropertyDrawer
{
    Texture2D t = Texture2D.whiteTexture;

    SerializedProperty bottom,top;
    string name;
    string nameD;

    bool cache = false;
    public override float GetPropertyHeight ( SerializedProperty property , GUIContent label )
    {
        // The 6 comes from extra spacing between the fields (2px each)
        return EditorGUIUtility.singleLineHeight * 1 + 1;
    }

    public override void OnGUI ( Rect position , SerializedProperty property , GUIContent label )
    {
        if (true)
        {
            name = property.displayName;
            property.Next( true );
            bottom = property.Copy();
            property.Next( false );
            top = property.Copy();
            cache = true;
        }        
        float _top = top.floatValue,_bottom = bottom.floatValue;
        float amplitude = _top-_bottom;
        amplitude = Mathf.Max( amplitude , 10);

        nameD = name + "    " + _bottom.ToString( "F1" ) + " <> " + _top.ToString( "F1" );
        float w =nameD.Length * 7f;

        EditorGUI.BeginProperty( position , label , property );

        EditorGUI.LabelField( new Rect( 19 , position.position.y , w, position.height ) , nameD );

        position.x = w;
        position.width -= position.x;

        EditorGUI.MinMaxSlider( position,""  , ref _bottom , ref _top ,Mathf.RoundToInt(_bottom - amplitude) , Mathf.RoundToInt( _top + amplitude ) );

        top.floatValue = _top;bottom.floatValue= _bottom;

        EditorGUI.EndProperty();
    }
}


[CustomPropertyDrawer( typeof( AudioElement ) )]
public class AudioElementDrawer : PropertyDrawer
{
    Texture2D t = Texture2D.whiteTexture;

    SerializedProperty audio,proba,pitchT,pitchB;
    string name;

    bool cache = false;
    public override float GetPropertyHeight ( SerializedProperty property , GUIContent label )
    {
        // The 6 comes from extra spacing between the fields (2px each)
        return EditorGUIUtility.singleLineHeight * 1 + 1;
    }

    public override void OnGUI ( Rect position , SerializedProperty property , GUIContent label )
    {
        if (true)
        {
            name = property.displayName;
            property.Next( true );
            audio = property.Copy();
            property.Next( false );
            proba = property.Copy();
            property.Next( false );
            property.Next( true );
            pitchB = property.Copy();
            property.Next( false );
            pitchT = property.Copy();
            cache = true;
        }

        EditorGUI.BeginProperty( position , GUIContent.none , property );

        float w = position.width;

        position.x = -w * 0.3f;

        Rect rect = position; rect.width = w * 0.7f;

        EditorGUI.ObjectField( rect , audio );

        rect.x += rect.width * 1.05f; rect.width = w *0.075f;

        proba.floatValue = EditorGUI.FloatField( rect , proba.floatValue );

        rect.x += rect.width * 1.1f; rect.width = w * 0.4f;

        float b = pitchB.floatValue; float t = pitchT.floatValue;

        label = new GUIContent(b.ToString("F1")+"<>"+t.ToString("F1"));
        EditorGUI.LabelField( rect , label );

        rect.x += rect.width * .45f; rect.width = w * 0.4f;

        EditorGUI.MinMaxSlider(rect , ref b , ref t , 0 , 4 );
        pitchB.floatValue = b; pitchT.floatValue = t;

        EditorGUI.EndProperty();
    }
}

[CustomPropertyDrawer( typeof( ObjectProbalize ) ,true)]
public class ObjectProbalizedDrawer : PropertyDrawer
{
    Texture2D t = Texture2D.whiteTexture;

    SerializedProperty obj,proba,type = null;
    string name;

    float[] sizes;

    bool cache = false;
    public override float GetPropertyHeight ( SerializedProperty property , GUIContent label )
    {
        // The 6 comes from extra spacing between the fields (2px each)
        return EditorGUIUtility.singleLineHeight * 1 + 1;
    }

    public override void OnGUI ( Rect position , SerializedProperty property , GUIContent label )
    {
        if (true)
        {
            name = property.displayName;
            property.Next( true );
            obj = property.Copy();
            property.Next( false );
            proba = property.Copy();
            property.Next( false );

            if (property.propertyType == SerializedPropertyType.String )
            {
                type = property.Copy();

                sizes = new float[3] { 0.7f , 0.2f , 0.2f };
            }
            else
            {
                sizes = new float[2] { 0.85f , 0.3f};
            }
        }

        EditorGUI.BeginProperty( position , GUIContent.none , property );

        float w = position.width;

        position.x = -w * 0.15f;

        Rect rect = position; rect.width = w * sizes[0];

        EditorGUI.PropertyField( rect , obj );

        rect.x += rect.width; rect.width = w * sizes[1];

        proba.floatValue = EditorGUI.FloatField( rect , proba.floatValue );

        if(type != null)
        {
            rect.x += rect.width; rect.width = w * sizes[2];

            type.stringValue = EditorGUI.TextField( rect , type.stringValue );
        }

        EditorGUI.EndProperty();
    }
}
