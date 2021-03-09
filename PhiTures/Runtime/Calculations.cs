using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Calculations : MonoBehaviour
{
    public static List<char> numbers = new List<char>(10) { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',',','-' };
    public static List<char> alphabetUp = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    public static List<char> alphabetLow = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', };
    public static List<char> signs = new List<char>() { ' '};
    public static List<List<char>> priorities = new List<List<char>>(3)
    {
 new List<char>() { '+','-'},
 new List<char>() { '*','/'},
 new List<char>() { '^'}
    };

    public static List<string> qwerty_azerty = new List<string>(){"aq","wz",";m"};

    public static float CharSize(char c, float letterLowSize = 1)
    {
        if (alphabetUp.Contains( c )) { return letterLowSize * 1.25f; }
        if (alphabetLow.Contains( c )) { return letterLowSize ; }
        if (numbers.Contains( c )) { return letterLowSize * 1.25f ; }
        if(c == ' ') { return letterLowSize * 1.1f; }
        return letterLowSize * .5f;
    }

    public static float Formula ( string formula , float x )
    {
        int i = 0;
        while (i < formula.Length)
        {
            string n = "";

            int o = i + 1;
            while (o < formula.Length && numbers.Contains( formula[o] ))
            {
                n += formula[o];
                o++;
            }

            if (formula[i] == '^')
            {
                x = Mathf.Pow( x , float.Parse( n ) );
            }

            if (formula[i] == '*')
            {
                x = x * float.Parse( n );
            }

            if (formula[i] == '/')
            {
                x = x / float.Parse( n );
            }

            if (formula[i] == '+')
            {
                x = x + float.Parse( n );
            }

            i = o;
        }

        return x;
    }
    
    public static string Command(string command, out string steps)
    {
        string str = ""; steps = "";

        List<string> parts = new List<string>();

        for (int i = 0 ; i < command.Length ; i++)
        {
            string n = "";

            bool isN = numbers.Contains( command[i] );

            i--; bool b = true;
            while (b)
            {
                i++;

                b = ( i < command.Length ) ? numbers.Contains( command[i] ) == isN : false;

                if (b) { n += command[i]; }
            }
            i--;

            parts.Add( n );
        } //RECCOVERING THE DIFFERENTS PART OF THE COMMMAND

        float result = 0;

        for (int priority = 2 ; priority >= 0 ; priority--)
        {
            float x=0; float y = 0; string s = ""; int i = 0;

            trilean b = true;
            while (b) {
                i++;
                if (i < parts.Count) { if (priorities[priority].Contains( parts[i][0] )) { b = false; } }
                else { b = trilean.Null; }
            }

            if (b.Active)
            {
                List<char> chars = new List<char>();
                foreach (string ss in parts) { for (int c = 0 ; c <= 3 && c < ss.Length ; c++) { chars.Add( ss[c] ); } }

                x = float.Parse( parts[i - 1] );
                s = parts[i];
                y = float.Parse( parts[i + 1] );

                steps += ( ListToString( chars ) + "  : " + x.ToString( "F2" ) + " " + s + " " + y.ToString( "F2" ) + '\n' );

                x = Formula( s + y.ToString() , x );

                parts[i] = x.ToString();
                parts.RemoveAt( i - 1 );
                parts.RemoveAt( i );
                priority++;
                result = x;
            }
        }

        str = result.ToString( "F3" );

        return str;
    }

    public static List<float> ListSorter ( List<float> toSort , bool returnOrder = true )
    {
        List<float> output = new List<float>();
        List<int> nums = new List<int>();
        List<float> ne = new List<float>() ;
        List<float> n2 = new List<float>() ;


        int i = 0;
        while (i < toSort.Count)
        {
            ne.Add( toSort[i] );
            nums.Add( i );
            i++;
        }


        i = 0; int m = ne.Count;
        while (ne.Count > 0 && i < m)
        {
            float min = 10000000; int minId = 0;
            int o = 0;
            while (o < ne.Count)
            {
                if (ne[o] < min)
                {
                    min = ne[o];
                    minId = o;
                }
                o++;
            }
            output.Add( nums[minId] );
            n2.Add( ne[minId] );
            ne.RemoveAt( minId );
            nums.RemoveAt( minId );
            i++;
        }

        /*
        foreach(float a in n2)
        {
            print(a);
        }
        */

        return returnOrder ? output : n2;
    }

    public static List<char> ListTroncer ( List<char> toTronc , int start , int end )
    {
        List<char> ne = new List<char>();

        for (int i = start ; i < toTronc.Count - end ; i++)
        {
            ne.Add( toTronc[i] );
        }

        return ne;
    }

    public static string ListToString ( List<char> chars )
    {
        string str = "";

        foreach (char c in chars)
        {
            str += c;
        }

        return str;
    }
    public static List<char> StringToList ( string str )
    {
        List<char> lst = new List<char>();

        foreach (char c in str)
        {
            lst.Add( c );
        }

        return lst;
    }

    public static char LowToUpChar(char c )
    {
        return alphabetLow.Contains(c) ? alphabetUp[alphabetLow.IndexOf( c )] : c;
    }

    public static string Capitalize(string str )
    {
        string n = LowToUpChar(str[0]).ToString();
        for(int i = 1 ; i < str.Length ; i++)
        {
            n += str[i];
        }
        return n;
    }

    public static int StringHeight ( string str , char returnChar = '\n' )
    {
        int r = str.Length > 0 ? 1 : 0;

        foreach (char c in str)
        {
            if (c == returnChar) { r++; }
        }

        return r;
    }

    public static int StringWidth ( string str , char returnChar = '\n' )
    {
        int r = 0; int t = 0;

        int i = 0;
        while (i < str.Length)
        {
            if (str[i] == returnChar)
            {
                r = t > r ? t : r;
                t = 0;
            }
            t++;
            i++;
        }
        r = t > r ? t : r;

        return r;
    }

    public static string RandomStringSticker ( string[] one , string[] two = null , string[] three = null , string[] four = null , string[] five = null , string[] six = null , string[] seven = null )
    {
        string str = "";

        if (one != null)
        {
            str += one[Random.Range( 0 , one.Length - 1 )];
        }
        if (two != null)
        {
            str += two[Random.Range( 0 , two.Length - 1 )];
        }
        if (three != null)
        {
            str += three[Random.Range( 0 , three.Length - 1 )];
        }
        if (four != null)
        {
            str += four[Random.Range( 0 , four.Length - 1 )];
        }
        if (five != null)
        {
            str += five[Random.Range( 0 , five.Length - 1 )];
        }
        if (six != null)
        {
            str += six[Random.Range( 0 , six.Length - 1 )];
        }
        if (seven != null)
        {
            str += seven[Random.Range( 0 , seven.Length - 1 )];
        }

        return str;
    }

    public static List<string> MultiLineToList ( string str )
    {
        List<string> list = new List<string>();

        for (int i = 0 ; i < str.Length ; i++)
        {
            string s = "";
            while (str[i] != '\n' && i < str.Length - 1)
            {
                s += str[i];
                i++;
            }
            list.Add( s );
        }

        return list;
    }

    public static string ListToMultiline ( string[] list )
    {
        string str = "";

        for (int i = 0 ; i < list.Length ; i++)
        {
            str += list[i] + ( i < list.Length - 1 ? "\n" : " " );
        }

        return str;
    }

    public static List<string> CharToStringList ( List<char> c )
    {
        List<string> s = new List<string>();

        foreach (char a in c)
        {
            string st = ""; st += a;
            s.Add( st );
        }

        return s;
    }

    public static Color LerpColor ( Color from , Color to , float lerp )
    {
        Vector4 vfrom = new Vector4(from.r,from.g,from.b,from.a);
        Vector4 vto = new Vector4(to.r,to.g,to.b,to.a);

        Vector4 volor = Vector4.Lerp(vfrom,vto,lerp);

        return new Color( volor.x , volor.y , volor.z , volor.w );
    }
    public static Color ColorLerping ( Color min , Color mid , Color max , float h )
    {
        float h2 = h >= 0.5f ? Mathf.Lerp(-1,1,h) :  Mathf.Lerp(1,-1,0.5f-h);

        Vector4 maxC = new Vector4(min.r,min.g,min.b,min.a);
        Vector4 midC = new Vector4(mid.r,mid.g,mid.b,mid.a);
        Vector4 minC = new Vector4(max.r,max.g,max.b,max.a);

        Vector4 vc = Vector4.Lerp(h >= 0.5f ? midC : minC,h >= 0.5f ? maxC : midC,h2);

        return new Color( vc.x , vc.y , vc.z , vc.w );
    }

    /// <summary>
    /// Smoothly transition between the given colors, using the given cursor.
    /// </summary>
    /// <param name="cursor">A value between 0 and 1 that is used to lerp between the colors.</param>
    /// <param name="colors">The list of the colors to lerp between.</param>
    /// <returns></returns>
    public static Color LerpBetweenColors (float cursor,params Color[] colors)
    {
        if(colors.Length <= 0) { return Color.white; }
        if(cursor >= 1) { return colors[colors.Length - 1]; }
        if(cursor <= 0) { return colors[0]; }

        float unit = 1f / colors.Length;
        int id = (int)Mathf.Floor( cursor / unit);

        cursor = (cursor - (id*unit));

        return Calculations.LerpColor( colors[id] , colors[id + 1] , cursor ) ;
    }
    public static Color ColorFromVector(Vector4 vector )
    {
        return new Color( vector.x , vector.y , vector.z , vector.w );
    }

    public static Vector4 VectorFromColor(Color color )
    {
        return new Vector4( color.r , color.g , color.b , color.a );
    }

    public static Vector3 RandomVector3 ( Vector3 min, Vector3 max )
    {
        return new Vector3( Random.Range( min.x , max.x ) , Random.Range( min.y , max.y ) , Random.Range( min.z , max.z ) );
    }

    public static void ValueTowards(ref float value, float to,float speed )
    {
        value = Mathf.MoveTowards( value , to , speed );
    }

    public static void RecordingArray<T>(ref T[] array, T newValue )
    {
        for (int i = array.Length - 1 ; i > 0 ; i--)
        {
            array[i - 1] = array[i];
        }
        array[array.Length - 1] = newValue;
    }

    public static Vector4 VectorPower(Vector4 vector, float power )
    {
        return new Vector4( Mathf.Pow(vector.x,power) , Mathf.Pow( vector.y , power ) , Mathf.Pow( vector.z , power ) , Mathf.Pow( vector.w , power ) );
    }

    /// <summary>
    /// Return the index of the first true-bool in the given ones. -1 if none are.
    /// </summary>
    public static int BoolsToIndex(params bool[] bools )
    {
        for(int i = 0 ;i<bools.Length ; i++)
        {
            if (bools[i]) { return i; }
        }
        return -1;
    }

    public static float FloatTransfert(ref float from,ref float to )
    {
        float to0 = to;
        to += from;
        float t = to - to0;
        from = 0;

        return t;
    }

    public static Vector2 vector2up = Vector2.up;

    public static object RandomObject<T>( int[] baseProbabilities,T[] objects)
    {
        List<T> selector = new List<T>();
        for (int i = 0 ; i < objects.Length ; i++)
        {
            for (int r = baseProbabilities[i] ; r > 0 ; r--)
            {
                selector.Add( objects[i] );
            }
        }

        return selector[Random.Range( 0 , selector.Count - 1 )];
    }

    public static float ConcatFloats(params float[] f )
    {
        float r = 0;

        for(int i = 0 ; i < f.Length ; i++)
        {
            r += Mathf.Pow( 10 , i ) * f[i];
        }

        return r;
    }

    public static float Restric(float value, float max,float min )
    {
        return value > max ? max : value < min ? min : value;
    }
    
    public static float ListSum(object[] list, string varName)
    { 
        float r = 0;
        if(list.Length < 1) { return 0; }
        System.Type type = list[0].GetType();

        foreach (var f in list)
        {
            r += (float)( type.GetField( varName ).GetValue( f) );
        }

        if(float.IsNaN(r) || float.IsInfinity( r )) { r = 0; }

        return r ;
    }

    public static List<object> ConcatFieldsInList(object[] list , string varName )
    {
        List<object> nl = new List<object>();

        if (list.Length < 1) { return null; }

        System.Type type = list[0].GetType();

        foreach (var f in list)
        {
            nl.AddRange ( (object[])type.GetField( varName ).GetValue( f ) );
        }

        return nl;
    }

    public static float ListMax(float[] list, out int index )
    {
        float max = 0; int i = 0; index = 0;
        foreach(float f in list)
        {
            if(f > max)
            {
                index = i;
                max = f;
            }
            i++;
        }

        return max;
    }

    public static string TimeToString ( float _seconds , string secondMinutesSeparator )
    {
        string str = "";

        float seconds= _seconds % 60;
        int minutes = Mathf.RoundToInt((_seconds - seconds) / 60);

        str = minutes.ToString() +
            secondMinutesSeparator +
            ( seconds >= 10 ? "" : "0" ) +
            seconds.ToString( "F0" );


        return str;
    }

    public static string QwertyAzerty ( string c )
    {
        int azerty=0,qwerty=1;

        foreach(string s in qwerty_azerty)
        {
            if (s[azerty].ToString() == c)
            {
                c = s[qwerty].ToString();
            }
            else if (s[qwerty].ToString() == c)
            {
                c = s[azerty].ToString();
            }
        }

        return c;
    }

    public static float ColorSimilarity ( Color c1 , Color c2 , bool ignoreOpacity = true )
    {
        float r = Mathf.Abs(c1.r - c2.r);
        float g = Mathf.Abs(c1.g - c2.g);
        float b = Mathf.Abs(c1.b - c2.b);
        float a = Mathf.Abs(c1.a - c2.a);

        return 1 - ( ( r + g + b + ( ignoreOpacity ? 0 : a ) ) / ( ignoreOpacity ? 3 : 4 ) );
    }

    public static bool ContainSimilarColor(Color[] arr,Color color, float threshold, bool ignoreOpacity = true)
    {
        foreach(Color c in arr)
        {
            if (ColorSimilarity( color , c,ignoreOpacity ) >= threshold) { return true; }
        }

        return false;
    }

    public static string IntToString(int n, int charCount = 1 )
    {
        for(float N = n ; N >= 10 ; N /= 10f) { charCount--; }

        string str = charCount > 0?  RepeatString( "0" , charCount ) : "";

        return str + n.ToString( "F0" );
    }

    public static string RepeatString(string str, int count)
    {
        while(count > 0) { str += str; count--; } return str;
    }

    public static Color CleanColor( Color c )
    {
        float coef = 3f/(c.r+c.g+c.b);

        return new Color( c.r * coef , c.g * coef , c.b * coef );
    }
    public static T RandomFromArray<T> ( T[] array, out int index )
    {
        index = Random.Range( 0 , array.Length );
        return array[index];
    }

    public static T RandomFromArray<T>( T[] array )
    {
        int i;
        return RandomFromArray( array, out i );
    }

    public static T[] GetTransformChilds<T> ( Transform transform )
    {
        T[] c = new T[transform.childCount];

        for (int i = 0 ; i < transform.childCount ; i++) { c[i] = transform.GetChild( i ).GetComponent<T>(); }

        return c;
    }

    public static Transform[] GetTransformChilds ( Transform transform )
    {
        Transform[] c = new Transform[transform.childCount];

        for (int i = 0 ; i < transform.childCount ; i++) { c[i] = transform.GetChild( i ); }

        return c;
    }

    public static Gradient GradientFromColor(Color color )
    {
        Gradient gradient = new Gradient();

        gradient.colorKeys = new GradientColorKey[1] { new GradientColorKey( color , 0 ) };
        gradient.alphaKeys = new GradientAlphaKey[1] { new GradientAlphaKey( color.a , 0 ) };

        return gradient;
    }

    public static void AddToArray<T>(ref T[] array, T add)
    {
        List<T> list = new List<T>(array);
        list.Add( add );
        array = list.ToArray();
    }
    public static void RemoveFromArray<T> ( ref T[] array, int index )
    {
        if(array.Length <= 0) { return; }
        index = index < 0 ? 0 : index >= array.Length ? array.Length - 1 : index;
        List<T> list = new List<T>(array);
        list.RemoveAt( index );
        array = list.ToArray();
    }

    public static int ToLinearArrayID(int[] ids, int[] lenghts )
    {
        if(ids.Length <= 0) { return ids[0]; }

        int r =  ids[0];
        int max = Mathf.Min(ids.Length,lenghts.Length);

        for (int i = 1 ; i < max ; i++)
        {
            r += ids[i] * lenghts[i];
        }

        return r;
    }
}

public static class TextFile
{
    public static bool Write ( string path , string content )
    {
        StreamWriter writer = new StreamWriter(path, false);
        writer.Write( content );
        writer.Close();
        return true;
    }

    public static string GetLine(StreamReader reader )
    {
        return reader.ReadLine();
    }

    public static bool EOF(StreamReader reader )
    {
        return reader.EndOfStream;
    }

    public static string Read ( string path )
    {
        return File.ReadAllText( path );
    }

    public static StreamReader GetReader(string path)
    {
        return new StreamReader(path);
    }


    public static void CloseReader(StreamReader reader ) { reader.Close(); }

#if UNITY_EDITOR    
    public static FileStream Create ( string path )
    {
        return Create( path , "" );
    }
    public static FileStream Create ( string path , string content )
    {
        FileStream file = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        StreamWriter writer = new StreamWriter(file);
        writer.Write( content );
        return file;
    }

    public static bool Exists(string path )
    {
        return File.Exists( path );
    }
#endif
}

public static class PlayerData
{
    public static string cacheColorName;

    public static void SetColor ( string name , Color color )
    {
        PlayerPrefs.SetFloat( name + ".r" , color.r );
        PlayerPrefs.SetFloat( name + ".g" , color.g );
        PlayerPrefs.SetFloat( name + ".b" , color.b );
        PlayerPrefs.SetFloat( name + ".a" , color.a );
    }

    public static Color GetColor ( string name )
    {
        if (PlayerPrefs.HasKey( name + ".r" ))
        {
            return new Color(
                PlayerPrefs.GetFloat( name + ".r" ) ,
                PlayerPrefs.GetFloat( name + ".g" ) ,
                PlayerPrefs.GetFloat( name + ".b" ) ,
                PlayerPrefs.GetFloat( name + ".a" )
                );
        }
        else
        {
            return Color.black;
        }
    }

    public static Color GetCacheColor ()
    {
        string t = cacheColorName; cacheColorName = "";
        return GetColor(t);
    }
}

[System.Serializable]
public struct writing
{
    [SerializeField] private string[] strings;
    private int languageCount
    {
        get { return ( strings != null ? strings.Length : 0 ); }
        set
        {
            if (strings == null)
            {
                strings = new string[value];
            }
            else
            {
                string[] strs = new string[value];
                for (int i = 0 ; i < value ; i++) { strs[i] = strings[i]; }
                strings = strs;
            }
        }
    }

    private static int language { get { if (PlayerPrefs.HasKey( "language" )) { return PlayerPrefs.GetInt( "language" ); } else { PlayerPrefs.SetInt( "language" , 0 ); return 0; } } }

    public writing ( string str , int _languageIndex = 0, int _languageCount = 4 )
    {
        _languageCount = Mathf.Max( _languageCount , 0 );
        _languageIndex = Mathf.Min( _languageCount , _languageIndex );

        strings = new string[_languageCount];
        strings[_languageIndex] = str;
    }

    public writing (params string[] strs )
    {
        strings = strs;
    }

    public static implicit operator string ( writing a )
    {
        return a.strings[language];
    }
    public static implicit operator writing ( string a )
    {
        return new writing( a );
    }
}

[System.Serializable]
public class CList<T>
{
    [SerializeField]
    public List<CList_Category<T>> categories = new List<CList_Category<T>> ();
    [SerializeField]
    public List<T> list = new List<T>();

    //Methods

    public bool Contains(T obj, out CList_Category<T> category ) { category = CList_CategoryOf( obj ); return list.Contains( obj ); }

    public bool Remove ( T obj )
    {
        foreach(CList_Category<T> c in categories)
        {
            if (c.list.Contains( obj )) { c.list.Remove( obj ); list.Remove( obj ); return true; }
        }

        return false;
    }

    public bool Add ( T obj, string category , int createCategoryCapacity = 0)
    {
        bool b = false; int index = 0; int i = 0;
       foreach(CList_Category<T> c in categories) { if (c.name == category) { b = true; index = i;  break; }i++; }

        if(!b)
        {
            if (createCategoryCapacity > 0) { categories.Add( new CList_Category<T>( category , createCategoryCapacity ) ); index = categories.Count - 1; }
            else { return false; }
        }

        if (categories[index].list.Count + 1 <= categories[index].capacity)
        {
            list.Add( obj );
            categories[index].list.Add( obj );
        }
        else
        {
            return false;
        }

        return true;
    }

    public CList_Category<T> CList_CategoryOf ( T obj)
   {
        foreach(CList_Category<T> c in categories)
        {
            if (c.list.Contains( obj )) { return c; }
        }
        return null;
    }

    public void AddCList_Category(List<CList_Category<T>> cat )
    { int i =0;
        foreach (CList_Category<T> s in cat)
        {
            categories.Add( new CList_Category<T>( s.name != "" ? s.name : i.ToString(), s.capacity > 0 ? s.capacity : 10000 ) );i++;
        }
    }

    public List<T> Get () { return list.GetRange(0,list.Capacity); }
    public List<T> Get (string category) {
        CList_Category<T> c = GetCategory(category);
        return c != null ? c.list != null ? c.list : new List<T>( 0 ) : new List<T>(0);
    }

    public CList_Category<T> GetCategory(string category )
    {
        foreach(CList_Category<T> c in categories)
        {
            if(c.name == category) { return c; }
        }
        return null;
    }

    public void Setup (string[] catsNames, int[] catsCapacities)
    {
        list = new List<T>();
        categories = new List<CList_Category<T>>();
        for (int i = 0 ; i < catsNames.Length ; i ++)
        {
            categories.Add(new CList_Category<T>(catsNames[i], catsCapacities[i]) );
        }
    }

    //Constructors

    public CList ( int capacity , int categoriesCount = 1 )
    {
        list.Capacity = capacity;
        categories = new List<CList_Category<T>>();
        AddCList_Category( new List<CList_Category<T>>(categoriesCount ) );
    }
    public CList ( List<T> source , int categoriesCount = 1 )
    {
        list = new List<T>( source );
        categories = new List<CList_Category<T>>();
        AddCList_Category( new List<CList_Category<T>>( categoriesCount ) ) ;
    }
    public CList ( List<T> source , List<CList_Category<T>> categoriesAdd )
    {
        list = new List<T>( source );
        categories = new List<CList_Category<T>>();
        AddCList_Category( categoriesAdd );
    }

    public static implicit operator List<T> ( CList<T> c) { return c.list; }

    //Subclasses

}
[System.Serializable]
public class CList_Category<T>
{
    public string name;
    public int capacity;
    [SerializeField]
    public List<T> list = new List<T>();

    public CList_Category ( string _name , int _capacity )
    {
        name = _name; capacity = _capacity;
    }
}
[System.Serializable]
public struct trilean 
{
    [HideInInspector]
    public short v;

    /// <summary>
    /// True value of a trilean.
    /// </summary>
    public static trilean True { get { return new trilean( 1 ); } }
    /// <summary>
    /// Neutral value of a trilean.
    /// </summary>
    public static trilean Null { get { return new trilean( 0 ); } }
    /// <summary>
    /// False value of a trilean.
    /// </summary>
    public static trilean False { get { return new trilean( -1 ); } }

    public bool IsTrue { get { return v == 1; } }
    public bool IsFalse { get { return v == -1; } }
    public bool IsNull { get { return v == 0; } }
    public bool Active { get { return v != 0; } }


    /// <summary>
    /// A trilean is like a boolean, but with a neutral value. So it can be true, false, or neither.
    /// </summary>
    public trilean (short val)
    {
        v = val;
        v = v < -1 ? (short)-1 : v > 1 ? (short)1 : v;
    }

    /// <summary>
    /// Combine two bools into a trilean.
    /// </summary>
    public static trilean Combine ( bool a, bool b )
    {
        if(a == b) { return a ? trilean.True : trilean.False; }
        else { return trilean.Null; }
    }

    public static implicit operator trilean ( bool a )
    {
        return new trilean( a );
    }

    public static implicit operator bool ( trilean a )
    {
        return a.v > 0;
    }

    /// <summary>
    /// Combine two trilean a and b. True if both are true, False if both are false, Null otherwise.
    /// </summary>
    public static trilean Combine ( trilean a , trilean b )
    {
        if(a == trilean.Null || b == trilean.Null) { return trilean.Null; }

        if(a == b) { return a; }
        else { return trilean.Null; }
    }

    /// <summary>
    /// Set this trilean as Null
    /// </summary>
    public void Release () { v = 0; }

    /// <summary>
    /// A trilean is like a boolean, but with a neutral value. So it can be true, false, or neither.
    /// </summary>
    public trilean ( bool a , bool b )
    {
        v = trilean.Combine( a , b ).v;
    }

    public trilean ( bool b ) { v = (short)( b  ? 1 : -1 ); }

    /// <summary>
    /// Return true if the trilean is not equal to Null
    /// </summary>

    public string ToString (int simple_number_both=0) { string str = "";
        switch (v) { case -1: str += "False";break; case 1: str+= "True";break; default: str += "Null";break; }
        switch (simple_number_both) { default: break; case 1:str = v.ToString();break; case 2: str += v.ToString();break; }
        return str;
    }
    public static bool operator ==(trilean a, trilean b )
    {
        return a.Equals( b );
    }
    public static bool operator != ( trilean a , trilean b )
    {
        return !a.Equals( b );
    }

    /// <summary>
    /// Return true if both trileans are true
    /// </summary>
    public static bool operator & ( trilean a , trilean b )
    {
        if (a == trilean.True && b == trilean.True) { return true; }
        else { return false; }
    }
    /// <summary>
    /// Return true if only one trilean is true
    /// </summary>
    public static bool operator ^ ( trilean a , trilean b )
    {
        if (a != b && (a == trilean.True ||b == trilean.True)) { return true; }
        else { return false; }
    }
    /// <summary>
    /// Return true if at least one trilean is true
    /// </summary>
    public static bool operator | ( trilean a , trilean b )
    {
        if(a == trilean.True ||b == trilean.True) { return true; }
        else {  return false;}
    }

    /// <summary>
    /// Combine two trilean a and b. 'True' if both are true, 'False' if both are false, 'Null' otherwise.
    /// </summary>
    public static trilean operator * ( trilean a , trilean b )
    {
        return trilean.Combine(a,b);
    }
    /// <summary>
    /// Add two trilean together. Literally add their values : (True=1 | False=-1 | Null=0)
    /// </summary>
    public static trilean operator + ( trilean a , trilean b )
    {
        return new trilean( (short)( a.v + b.v ) );
    }
    /// <summary>
    /// Substract two trilean together. Literally Substract their values : (True=1 | False=-1 | Null=0)
    /// </summary>
    public static trilean operator - ( trilean a , trilean b )
    {
        return new trilean( (short)( a.v - b.v ) );
    }
    /// <summary>
    /// Divide two trilean together. Literally Divide their values : (True=1 | False=-1 | Null=0). If the divider is Null, return Null (avoid dividing by 0)
    /// </summary>
    public static trilean operator / ( trilean a , trilean b )
    {
        if(b == trilean.Null) { return trilean.Null; }
        return new trilean( (short)( a.v / b.v ) );
    }

}

[System.Serializable]
public struct small
{
    public sbyte v;

    public static small A { get { return new small( 1 ); } }
    public static small B { get { return new small( 0 ); } }
    public static small C { get { return new small( -1 ); } }

    public small(int value ) { int v2 = value; v2 = v2 > 1 ? 1 : v2 < -1 ? -1 : v2; v = (sbyte)v2; }

    public static bool operator == ( small a , small b )
    {
        return a.Equals( b );
    }
    public static bool operator != ( small a , small b )
    {
        return !a.Equals( b );
    }

}

[System.Serializable]
public struct flaot
{
    public float value;

    public flaot(float a ) { value = a; }

    public static implicit operator flaot (float a)
    {
        return new flaot( a );
    }

    public static implicit operator float ( flaot a )
    {
        return a.value;
    }

    public static flaot operator * ( flaot a , flaot b )
    {
        return new flaot( ( a.value * b.value ) * Mathf.Sign( a.value ) );
    }
    public static flaot operator / ( flaot a , flaot b )
    {
        return new flaot( ( a.value / b.value ) * Mathf.Sign( a.value ) );
    }
    public static flaot operator + ( flaot a , flaot b )
    {
        return new flaot( a.value + b.value );
    }
    public static flaot operator - ( flaot a , flaot b )
    {
        return new flaot( a.value - b.value );
    }
    public static bool operator == ( flaot a , flaot b )
    {
        return a.Equals( b );
    }
    public static bool operator != ( flaot a , flaot b )
    {
        return !a.Equals( b );
    }
    public static bool operator & ( flaot a , flaot b )
    {
        return Mathf.Sign( a.value ) == Mathf.Sign( b.value );
    }
    public static bool operator ^ ( flaot a , flaot b )
    {
        return Mathf.Sign( a.value ) != Mathf.Sign( b.value );
    }
    public static bool operator | ( flaot a , flaot b )
    {
        return ( a.value != 0 ) || ( b.value != 0 );
    }
}


[System.Serializable]
public struct coefficient 
{
    public float value;
    public bool overide;

    public float Apply ( float source )
    {
        if (overide) { return value; }
        else { return source * value; }
    }

    public static coefficient Default { get { return new coefficient( 1 , false ); } }
    public static coefficient Neutral { get { return new coefficient( 1 , false ); } }

    public coefficient (float val, bool over )
    {
        value = val;
        overide = over;
    }
}

[System.Serializable]
public struct interval
{
    public float _bottom;
    public float _top;

    public float top { get { return _top; } set { _top = value; _bottom = _bottom > _top ? _top : _bottom; } }
    public float bottom { get { return _bottom; } set { _bottom = value; _top = _top < _bottom ? _bottom : _top; } }
    public float amplitude { get { return top - bottom; } }
    public float random { get { return Random.Range( bottom , top ); } }

    public static interval Null { get { return new interval( 0 , 0 ); } }
    public static interval Default { get { return new interval( -1 , 1 ); } }
    public interval ( float _top_,float _bottom_ ) { _bottom = _bottom_; _top = _top_; bottom = _bottom; top = _top; }
    public static interval CreateFromTop ( float _top_,float _amplitude_) { return new interval( _top_,_top_ - _amplitude_); }
    public static interval CreateFromBottom ( float _bottom_ , float _amplitude_ ) { return new interval( _bottom_ + _amplitude_ , _bottom_ ); }
    public bool Contains(float v )
    {
        return v >= _bottom && v <= _top;
    }

    public string ToString (string F)
    {
        return _bottom.ToString( F ) + " | " + _top.ToString( F );
    }

    public float GetRatio(float v,bool crop = false )
    {
        float r = ( v - bottom ) / ( top - bottom );
        return !crop ? r : ( r < 0 ? 0 : r > 1 ? 1 : r );
    }

    public float Get(float r )
    {
        return Mathf.Lerp( bottom , top , r );
    }

    public static implicit operator float (interval i ) { return i.random; }
}

[System.Serializable]
public struct AudioElement
{
    public AudioClip clip;
    public float probability;
    public interval pitch;

    public static AudioElement NULL { get { return new AudioElement( null , 0 ); } }

    public AudioElement(AudioClip _clip ) { clip = _clip; probability = 1; pitch = new interval( 1 , 1 ); }
    public AudioElement(AudioClip _clip, float _p) { clip = _clip; probability = _p; pitch = new interval( 1 , 1 ); }
    public AudioElement(AudioClip _clip, float _p , interval _pitch) { clip = _clip; probability = _p; pitch = _pitch; }

    public static void SetupAudioSource(AudioSource a, AudioElement ae, float _pitch = -1 )
    {
        a.clip = ae.clip;
        a.pitch = _pitch < 0 ? ae.pitch : _pitch;
    }

    public static implicit operator AudioClip ( AudioElement a ) { return a.clip; }
    public static implicit operator float ( AudioElement a ) { return a.probability; }
    public static implicit operator interval ( AudioElement a ) { return a.pitch; }

    public static AudioElement GetRandom (AudioElement[] audioElements )
    {
        ObjectProbalized<AudioElement>[] list = new  ObjectProbalized<AudioElement>[audioElements.Length];

        for(int i = 0 ;i < audioElements.Length ; i++) { list[i] = new ObjectProbalized<AudioElement>(audioElements[i],audioElements[i].probability); }

        return ObjectProbalized<AudioElement>.Get( list );
    }
}
[System.Serializable]
public class ObjectProbalize 
{

}
[System.Serializable]
public class ObjectProbalized<T> : ObjectProbalize
{
    public T obj;
    public float probability=1;
    interval interval = interval.Null;
    bool isNull;

    public static ObjectProbalized<T> NULL { get { return new ObjectProbalized<T>( true ); } }
    public static ObjectProbalized<T>[] NULLARRAY { get { return new ObjectProbalized<T>[1] { NULL }; } }

    public ObjectProbalized () { obj = default; probability = 1; }
    public ObjectProbalized (T _obj, float _p){obj = _obj; probability = _p;}
    public ObjectProbalized (bool _null) { obj = default; probability = 1; isNull = _null; }

    public bool IsNull () { return isNull; }

    public static void SetInterval(ref ObjectProbalized<T>[] array )
    {
        ObjectProbalized<T>[] _arr = new ObjectProbalized<T>[array.Length]; array.CopyTo( _arr , 0 );
        if(Sum(_arr) > 1) { Flatten( ref _arr ); }

        //Debug.Log( _arr.Length );
        float bottom= 0; int id = 0;
        foreach (ObjectProbalized<T> obj in array) 
        {
            //Debug.Log( bottom.ToString("F2") + " | " + _arr[id].probability.ToString("F2") + " or " + obj.probability.ToString("F2"));

            obj.interval = interval.CreateFromBottom( bottom , _arr[id].probability );
            bottom += _arr[id].probability;
            id++;

            Debug.Log( obj.interval.ToString( "F2" ) );
        }
    }

    public static float Sum( ObjectProbalized<T>[] array )
    {
        float sum =0;
        foreach (ObjectProbalized<T> o in array) { sum += o.probability; }
        return sum;
    }

    public static float Flatten ( ref ObjectProbalized<T>[] array )
    {
        float sum = Sum( array ); if(sum == 0) { return 0; }
        foreach (ObjectProbalized<T> o in array) { o.probability /= sum; }
        return sum ;
    }

    public static ObjectProbalized<T> Get ( ObjectProbalized<T>[] array, out int id )
    {
        if(array.Length == 0) { id = -1; return null; }
        if(array.Length == 1 && array[0].isNull) { id = -1; return null; }

        ObjectProbalized<T>[] _arr = new ObjectProbalized<T>[array.Length]; array.CopyTo( _arr,0 ); Flatten( ref _arr );

        SetInterval( ref _arr );
        float v = Random.Range(0,1f);

        id = 0;
        foreach (ObjectProbalized<T> obj in _arr)
        {
            if (obj.interval.Contains(v) ) { return obj; }
            id++;
        }
        id = -1;

        return null; 
    }

    public static ObjectProbalized<T> Get( ObjectProbalized<T>[] array )
    {
        int id = 0;
        ObjectProbalized<T> selected = Get(array, out id);
        /*
        id = 0;int a = 0;
        foreach (ObjectProbalized<T> o in array) { if (o == selected) { id = a; a++; } }
        */
        return selected;
    }

    public static implicit operator T ( ObjectProbalized<T> objectProbalizeds )
    {
        return objectProbalizeds.obj;
    }

    public static T[] ConvertArray( ObjectProbalized<T>[] objectProbalizeds )
    {
        T[] array = new T[objectProbalizeds.Length];

        for(int i = 0 ; i < array.Length ; i++) { array[i] = objectProbalizeds[i].obj; }

        return array;
    }

    public static ObjectProbalized<T>[] SetArray( T[] array , float[] probas = null)
    {
        ObjectProbalized<T>[] arr = new ObjectProbalized<T>[array.Length];

        for(int i = 0 ; i < array.Length ; i++)
        {
            arr[i] = new ObjectProbalized<T>( array[i] , probas == null ? 1 : probas[i] );
        }

        return arr;
    }
}

[System.Serializable]
public class ObjectProbalizedTyped<T> : ObjectProbalized<T>
{
    public string type;
    public ObjectProbalizedTyped ( T _obje , float _p, string _t ) { obj = _obje; probability = _p; type = _t; }
}

[System.Serializable]
public struct RandomScalar
{
    public static float __1_1 { get { return Random.Range( -1f , 1f ); } }
    public static float __10_10 { get { return Random.Range( -10f , 10f ); } }
    public static float __100_100 { get { return Random.Range( -100f , 100f ); } }
    public static float __1000_1000 { get { return Random.Range( -1000f , 1000f ); } }
    public static float _0_1 { get { return Random.Range( 0 , 1f ); } }
    public static float _0_10 { get { return Random.Range( 0 , 10f ); } }
    public static float _0_100 { get { return Random.Range( 0 , 100f ); } }
    public static float _0_1000 { get { return Random.Range( 0 , 1000f ); } }

    public static float GetAround(float center,float amplitude,float curve )
    {
        return GetBetween( center - amplitude / 2f , center + amplitude / 2f , curve );
    }

    public static float GetBetween(float min, float max, float curve = 1)
    {
        return Mathf.Pow( 1f / curve ,
            Random.Range( 
                Mathf.Pow( min , curve ) , Mathf.Pow( max , curve ) ) );
    }
}

