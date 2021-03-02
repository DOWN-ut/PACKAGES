using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Terminal_Editor : EditorWindow
{
    public List<string> lastCommands = new List<string> ();  

    public List<string> lastResults = new List<string> ();

    public string lastSteps;

    public string command;

    [SerializeField]
    public static Color backgroundColor = new Color(0.3f,0.3f,0.3f,1);
    [SerializeField]
    public static Color inputColor =Color.black;
    [SerializeField]
    public static Color textColor = Color.white;

    [MenuItem( "Window/Terminal" )]
    public static void ShowWindow ()
    {
        EditorWindow.GetWindow<Terminal_Editor>( "Terminal" );
    }

    void OnGUI ()
    {
        GUING.BackgroundColor( backgroundColor );
        GUI.Box( new Rect( 0 , 0 , position.width , position.height ),GUIContent.none );
        GUING.BackgroundColor( default,false );

        GUILayout.BeginHorizontal();

        Rect rect =  new Rect( 0 , 0 , position.width * 0.95f , position.height  );

        GUILayout.BeginArea( rect);

        DisplayCommands();

        InputCommand();

        GUILayout.EndArea();

        rect = new Rect( position.width * 0.95f , 0, position.width * 0.05f , position.height ); 
        GUILayout.BeginArea( rect );

        rect = new Rect(0 ,0, position.width * 0.05f , position.height*0.75f );
        GUILayout.BeginArea( rect );

        if (GUILayout.Button( "Clear" ))
        {
            lastCommands = new List<string>();
            lastResults = new List<string>();
            lastSteps = "";
        }
        GUILayout.EndArea();

        rect = new Rect( 0, position.height * 0.75f , position.width * 0.05f , position.height*0.25f);
        GUILayout.BeginArea( rect );

        if (GUILayout.Button( "GO" ))
        {
            lastCommands.Add( command ); string steps = "";
            lastResults.Add( Calculate( command,out steps ) );
            lastSteps = steps;
        }
        GUILayout.EndArea();

        GUILayout.EndArea();

        GUILayout.EndHorizontal();
    }

    void InputCommand ()
    {
        Rect rect =new Rect( 0 , position.height * 0.75f , position.width , position.height * 0.25f );

        GUILayout.BeginArea( rect);

        var style = new GUIStyle(GUI.skin.textField); style.normal.textColor = textColor; style.focused.textColor = textColor;

        GUING.BackgroundColor( inputColor );
        command = GUILayout.TextField( command, style , GUILayout.MinWidth( position.width*0.9f) );
        GUING.BackgroundColor(default,false );

        GUILayout.EndArea();
    }

    void DisplayCommands ()
    {
        Rect rect =  new Rect( 0 , 0 , position.width , position.height * 0.75f );
        //     Texture2D t = new Texture2D((int)rect.width , (int)rect.height);      

        GUILayout.BeginArea( rect );

        var styleC = new GUIStyle(GUI.skin.label); styleC.alignment = TextAnchor.LowerRight; styleC.normal.textColor = textColor;
        var styleR = new GUIStyle(GUI.skin.label); styleR.alignment = TextAnchor.LowerLeft; styleR.normal.textColor = textColor;

        int count = (int)(position.height/30f );
        count = ( count > lastCommands.Count ) ? lastCommands.Count : count;

        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();
        for (int i = lastCommands.Count - count ; i < lastCommands.Count ; i++)
        {
            GUILayout.Label( lastCommands[i] , styleC );
        }
        GUILayout.EndVertical();
        GUILayout.Box( GUIContent.none , GUILayout.Width( 20 ) , GUILayout.Height( position.height * 0.75f ) );
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();
        for (int i = lastCommands.Count - count ; i < lastResults.Count ; i++)
        {
            GUILayout.Label( lastResults[i] , styleR );
        }
        GUILayout.EndVertical();
        GUILayout.Box( GUIContent.none , GUILayout.Width( 10 ) , GUILayout.Height( position.height * 0.75f ) );
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();
        GUILayout.Label( lastSteps , styleR , GUILayout.Width( 150 ) );
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    string Calculate(string str,out string steps)
    {
        steps = "";
        return Calculations.Command(str, out steps );
    }
}

public static class GUING
{
    static Color backgroundColorStorer;

    public static void BackgroundColor ( Color color  = default, bool set = true )
    {
        if (set)
        {
            backgroundColorStorer = GUI.backgroundColor;
            GUI.backgroundColor = color;
        }
        else
        {
            backgroundColorStorer = GUI.backgroundColor;
            GUI.backgroundColor = backgroundColorStorer;
        }
    }

    public static void Line ( float returnHeight , float LineHeight , float width = 0 , float returnWidth = 0 )
    {
        Color c = GUI.color;
        GUI.color = new Color( 0 , 0 , 0 , 0 );
        GUILayout.Box( "" , GUILayout.Height( returnHeight ) , GUILayout.Width( ( returnWidth == 0 ) ? Screen.width : returnWidth ) , GUILayout.ExpandWidth( false ) );
        GUI.color = Color.black;
        GUILayout.Box( "" , GUILayout.Height( LineHeight ) , GUILayout.Width( ( width == 0 ) ? Screen.width : width ) , GUILayout.ExpandWidth( false ) );
        GUI.color = c;
    }

    public static void Title ( string text , bool returnAtSpaces = false , bool center = false , float setWidth = 0 , int setFontSize = 0, GUIStyle style = default )
    {
        style = style == default ? new GUIStyle( GUI.skin.label ) : style; style.richText = true ;
        if (center) { style.alignment = TextAnchor.MiddleCenter; }
        if (setFontSize != 0) { style.fontSize = setFontSize; }

        if (returnAtSpaces)
        {
            string t = "";
            for (int i = 0 ; i < text.Length ; i++)
            {
                t += text[i];
                if (text[i] == ' ') { t += '\n'; }
            }
            text = t;
        }


        if (setWidth != 0) { GUILayout.Label( text , style , GUILayout.Width( setWidth ) , GUILayout.ExpandWidth( false ) ); }
        else { GUILayout.Label( text , style ); }
    }

    public static float Slider ( float value , float min , float max , float precision , Vector2 belowSeparation = default , string title = "" , float width = 500 , bool vertical = false , float height = 0 )
    {
        //belowSeparation = belowSeparation == default ? new Vector2( 10 , 1 ) : belowSeparation;
        GUILayout.BeginVertical();

        GUING.Title( title , false , true , width );

        //if(GUING.Title != "") { GUILayout.BeginHorizontal(); }

        //float w =5000/(float)GUING.Title.Length;
        //GUILayout.BeginArea( new Rect( (GUING.Title.Length * 6), yPos , w , 20 ) );
        float v = value;
        if (!vertical)
        {
            v = Mathf.RoundToInt( GUILayout.HorizontalSlider( value , min , max , GUILayout.Width( width ) , GUILayout.ExpandWidth( false ) ) / precision ) * precision;
        }
        else
        {
            v = Mathf.RoundToInt( GUILayout.VerticalSlider( value , min , max , GUILayout.Height( height ) , GUILayout.MaxWidth( width ) , GUILayout.ExpandWidth( false ) ) / precision ) * precision;
        }

        //if (GUING.Title != "")  { GUILayout.EndHorizontal(); }
        if (belowSeparation != default) { GUING.Line( belowSeparation.x , belowSeparation.y , width , width ); }
        GUILayout.EndVertical();

        return v;
    }

    public static void FloatField ( ref float value , string label , float valueWidth , float labelWidth , float labelHeight )
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField( label , GUILayout.Width( labelWidth ) , GUILayout.Height( labelHeight ) );
        value = EditorGUILayout.FloatField( value , GUILayout.Width( valueWidth ) , GUILayout.Height( labelHeight ) );
        EditorGUILayout.EndHorizontal();
    }
    public static void Vector3Field ( ref Vector3 value , string label , float valueWidth , float labelWidth , float labelHeight, GUIStyle labelStyle= null )
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField( label, labelStyle != null ? labelStyle : GUIStyle.none , GUILayout.Width( labelWidth ) , GUILayout.Height( labelHeight ) );
        value = EditorGUILayout.Vector3Field("", value , GUILayout.Width( valueWidth ) , GUILayout.Height( labelHeight ) );
        EditorGUILayout.EndHorizontal();
    }

    public static void Space ( int count , float width = default)
    {
        for (int i = 0 ; i < count ; i++)
            if (width == 0) { EditorGUILayout.Space( ); }
            else { EditorGUILayout.Space( width ); }
    }

    public static void CircleInTexture(ref Texture2D text, float diameterRatio, float ellipsity, Color color)
    {
        float dia = Mathf.Min(text.width,text.height) * diameterRatio; float ra = dia/2f;
        float w = ra; if(ellipsity > 1) { w /= ellipsity; }
        float h = ra; if (ellipsity < 1) { h *= ellipsity; }
        Vector2Int center = new Vector2Int(Mathf.RoundToInt( text.width/2f),Mathf.RoundToInt( text.height/2f));

        for (float c = -1 ; c <=1 ;c += 1f/ (float)text.width)
        {
            float acos = Mathf.Acos( c);
            float y = Mathf.Sin(acos) * h;
            float x = c*w;
    
            text.SetPixel( Mathf.RoundToInt( center.x + x ), Mathf.RoundToInt( center.y + y ), color ) ;
            text.SetPixel( Mathf.RoundToInt( center.x + x ), Mathf.RoundToInt( center.y - y ), color );
        }
    }
}
