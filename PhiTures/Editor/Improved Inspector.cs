using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class ColorPalette_Editor : EditorWindow
{
    List<ColorPalette> palettes;

    GUIStyle style;

    int palettePerLine = 3;
    float paletteSize = 200;

    public bool selecting;

    public ColorInspector inspector;

    [MenuItem( "Window/Color Palette" )]
    public static void ShowWindow ()
    {
        EditorWindow.GetWindow<ColorPalette_Editor>( "Color Palette" );
    }

    void OnGUI ()
    {
        List<ColorPalette> foundPalettes = Asseting.FindAssetsByType<ColorPalette>();
        List<float> orders = new List<float>(); foreach(ColorPalette p in foundPalettes) { orders.Add( p.order ); }

        List<int> sorted; ArrayProcess.ListSorter(orders,out sorted);

        palettes = new List<ColorPalette>();
        for (int i = 0 ; i < foundPalettes.Count ;i++)
        {
            palettes.Add( foundPalettes[sorted[i]] );
        }

        GUILayout.BeginHorizontal();

        GUING.Title( "Found : " + palettes.Count + " Color Palettes." ,false,false,200);

        palettePerLine = (int)GUING.Slider( palettePerLine , 2 , 7 , 1 , new Vector2(0,0) , "Palette Per Line",100 );
        paletteSize = (int)GUING.Slider( paletteSize , 200 , 400 , 1 , new Vector2(0,0) , "Palette Size",100 );

        GUILayout.EndHorizontal();

        DisplayPalettes(50, paletteSize , palettePerLine );

        /*
        Color gc = GUI.backgroundColor; GUI.backgroundColor = Color.grey;
        if (GUI.Button(new Rect(0,this.position.height-25,500,25), "CLOSE" )) { this.Close(); }GUI.backgroundColor = gc;*/
    }

    void DisplayPalettes (float yPos,float paletteSize,int countPerLine = 3)
    {
        int x=0,y=0;float width=paletteSize,height=paletteSize;
        int colorPerLine = 3; float colorW=60,colorH = 50;
        foreach (ColorPalette palette in palettes)
        {
            DisplayPalette( palette,new Rect(x*(Screen.width/(float)(countPerLine-1)),(y*height)+ yPos , width , height + ( colorH*( palette.colors.Count/colorPerLine ) )), colorW, colorH, colorPerLine );

            x += 1;
            if (x % countPerLine == 0) { y += 1; }
        }
        if(GUI.Button( new Rect(this.position.width-(width/2f), ( y * height ) + yPos , width/2f , height ),"New Palette" )) {
            Object newPalette =Instantiate(palettes[palettes.Count-1]); string path =AssetDatabase.GetAssetPath( palettes[palettes.Count - 1] );
            AssetDatabase.CreateAsset( newPalette , path.Remove( path.Length -6,6 )+"_2.asset");
        }
    }

    void DisplayPalette(ColorPalette palette,Rect rect ,float colorW,float colorH, int countPerLine = 3)
    {
        Texture2D t = new Texture2D((int)rect.width , (int)rect.height); 

        GUILayout.BeginArea( rect, t);

        //GUING.Title("<b>"+ palette.name + "</b>",false,true);
        style = new GUIStyle( GUI.skin.textField );style.fontStyle = FontStyle.Bold; style.alignment = TextAnchor.MiddleCenter;
        palette.name  = GUILayout.TextField( palette.name,style,GUILayout.Width(rect.width),GUILayout.Height(16));

        GUILayout.BeginHorizontal();

        int x=0; Color gc = GUI.contentColor; style = new GUIStyle( GUI.skin.label ); style.fontSize = 10; style.alignment = TextAnchor.MiddleCenter;
        for (int i = 0 ; i <= palette.colors.Count ;i++)
        {
            if (x % countPerLine == 0) { GUILayout.EndHorizontal(); GUILayout.BeginHorizontal(); }

            if (i < palette.colors.Count)
            {
                int variation =palette.variation;
                if(palette.variation >= palette.colors[i].colors.Count) { palette.colors[i].colors.Add( palette.colors[i].colors[palette.colors[i].colors.Count - 1] ); }

                GUILayout.BeginVertical();

                Couleur couleur =palette.colors[i]; Color c = GUI.backgroundColor; GUI.backgroundColor = couleur.colors[variation];
                couleur.name = GUILayout.TextField( palette.colors[i].name , style , GUILayout.Width( colorW ) , GUILayout.Height( 15 ) );

                if (!selecting) { couleur.colors[variation] = EditorGUILayout.ColorField( GUIContent.none , palette.colors[i].colors[variation] , false , false , false , GUILayout.Width( colorW ) , GUILayout.Height( colorH ) ); }

                else if(GUILayout.Button("",GUILayout.Width(colorW),GUILayout.Height(colorH)))
                {
                    PlayerData.cacheColorName = ( (int)Random.Range( 9999 , 999999 ) ).ToString();
                    PlayerData.SetColor( PlayerData.cacheColorName , couleur.colors[variation] );
                    inspector.paleting = 2;
                    this.Close();
                }

                palette.colors[i] = couleur; GUI.backgroundColor = c;

                GUILayout.EndVertical();
            }
            else
            {
                GUILayout.BeginVertical();

                GUILayout.Label( "NEW" ,style, GUILayout.Width( colorW ) );
                style = new GUIStyle( GUI.skin.button ); style.fontSize = 35; style.alignment = TextAnchor.MiddleCenter; GUI.contentColor = Color.white;
                if (GUILayout.Button("+",style , GUILayout.Width( colorW ) , GUILayout.Height( colorH ) )) { palette.colors.Add( new Couleur("color") ); }

                GUILayout.EndVertical();
            }
            x += 1;
        }
        GUILayout.EndHorizontal(); GUI.contentColor = gc;

        palette.variation = (int)GUING.Slider( palette.variation , 0 , 5 , 1 , new Vector2( 0 , 0 ) , "Variation " + palette.variation , rect.width*0.95f );

        GUING.Line( 2 , 0.1f );

        GUILayout.BeginHorizontal();
        if (GUILayout.Button( "<" )) { palette.order--; }
        style = new GUIStyle( GUI.skin.label ); style.fontSize = 8; style.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label( (-palette.order).ToString(), style , GUILayout.Width(50) );
        if (GUILayout.Button( ">" )) { palette.order++; }
        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }
}

[CustomPropertyDrawer( typeof( Color ),false )]
public class ColorInspector : PropertyDrawer
{
    public int paleting;
    public override float GetPropertyHeight ( SerializedProperty property , GUIContent label )
    {
        // The 6 comes from extra spacing between the fields (2px each)
        return EditorGUIUtility.singleLineHeight * 1 + 4;
    }
    
    public override void OnGUI ( Rect position , SerializedProperty property , GUIContent label )
    {
        //base.OnGUI( position , property , label );
        EditorGUI.BeginProperty( position , label , property );

        EditorGUI.LabelField( position , label );

        if (paleting==0)
        {
            Color color = property.colorValue;
            color = EditorGUI.ColorField( new Rect( position.x + position.width / 2.5f , position.y , 150 , position.height ) , GUIContent.none , color , false , true , false );
            property.colorValue = color;
        }
        else if(paleting == 2)
        {
            property.colorValue = PlayerData.GetCacheColor(); paleting = 0;
        }

        if(GUI.Button( new Rect( position.x + position.width / 1.25f , position.y , 75 , position.height ),new GUIContent("Palettes") )) {
            //EditorWindow.GetWindow<ColorPalette_Editor>( "Color Palette" );
            ColorPalette_Editor window = EditorWindow.CreateWindow<ColorPalette_Editor>( "Color Palette" );
            window.selecting = true;
            window.inspector = this;
            paleting = 1;
        }

        EditorGUI.EndProperty( );

        //if (GUILayout.Button( "" , GUILayout.Width( 10 ) , GUILayout.Height( 10 ) )) { EditorWindow.GetWindow<ColorPalette_Editor>( "Color Palette" ); }

    }
}

[System.Serializable]
public struct Couleur
{
    public string name;

    public List<Color> colors;
    public Couleur ( string nam = "color")
    {
        name = nam;
        colors = new List<Color>( 1 ) { Color.white };
    }
}

public class Asseting
{
    public static List<T> FindAssetsByType<T> () where T : UnityEngine.Object
    {
        List<T> assets = new List<T>();
        string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
        for (int i = 0 ; i < guids.Length ; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath( guids[i] );
            T asset = AssetDatabase.LoadAssetAtPath<T>( assetPath );
            if (asset != null)
            {
                assets.Add( asset );
            }
        }
        return assets;
    }
}