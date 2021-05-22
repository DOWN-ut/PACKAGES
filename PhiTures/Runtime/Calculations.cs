using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Miscelaneous calculus and operation functions
/// </summary>
public class Calculations : MonoBehaviour
{
    public static List<List<char>> priorities = new List<List<char>>(3)
    {
 new List<char>() { '+','-'},
 new List<char>() { '*','/'},
 new List<char>() { '^'}
    };
    public static void ValueTowards(ref float value, float to,float speed )
    {
        value = Mathf.MoveTowards( value , to , speed );
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
    /// <summary>
    /// Applies the given formula to the given value
    /// </summary>
    /// <param name="formula"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static float Formula ( string formula , float x )
    {
        int i = 0;
        while (i < formula.Length)
        {
            string n = "";

            int o = i + 1;
            while (o < formula.Length && StringProcess.numbers.Contains( formula[o] ))
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
    public static string Command ( string command , out string steps )
    {
        string str = ""; steps = "";

        List<string> parts = new List<string>();

        for (int i = 0 ; i < command.Length ; i++)
        {
            string n = "";

            bool isN = StringProcess.numbers.Contains( command[i] );

            i--; bool b = true;
            while (b)
            {
                i++;

                b = ( i < command.Length ) ? StringProcess.numbers.Contains( command[i] ) == isN : false;

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
            while (b)
            {
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

                steps += (StringProcess.ListToString( chars ) + "  : " + x.ToString( "F2" ) + " " + s + " " + y.ToString( "F2" ) + '\n' );

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

    /// <summary>
    /// Creates a loop using the gives function
    /// </summary>
    /// <param name="f">Function</param>
    /// <param name="x">Given value, between 0 and 2 : 0 to 1 first part loop, 1 to 2 second part loop </param>
    /// <param name="lerpBetween"></param>
    /// <returns></returns>
    public static float LoopFunctionProcess( LoopFunction f , float x)
    {
        x = (x <= 1) ? (x) : ( 2 - x );

        switch(f.function)
        {
            default:goto case LoopFunctionType.Linear;
            case LoopFunctionType.Linear:
                return x;
            case LoopFunctionType.Sinus:
                return Mathf.Sin( x * Mathf.PI );
            case LoopFunctionType.Cosinus:
                return Mathf.Cos( x * Mathf.PI );
            case LoopFunctionType.Power:
                return Mathf.Pow( x , f.argument );
        }
    }

    public struct LoopFunction
    {
        public LoopFunctionType function;
        public float argument;

        public static LoopFunction Sinus { get { return new LoopFunction( LoopFunctionType.Sinus , 0 ); } }

        public LoopFunction(LoopFunctionType f ) { function = f; argument = 0; }
        public LoopFunction(LoopFunctionType f , float a) { function = f; argument = a; }
    }

    public enum LoopFunctionType
    {
        Linear,
        Sinus,
        Cosinus,
        Power
    }
}
/// <summary>
/// Transform and Vector functions
/// </summary>
public class TransformProcess
{
    public static Vector2 vector2up = Vector2.up;
    public static Vector3 RandomVector3 ( Vector3 min , Vector3 max )
    {
        return new Vector3( Random.Range( min.x , max.x ) , Random.Range( min.y , max.y ) , Random.Range( min.z , max.z ) );
    }
    public static Vector4 VectorPower ( Vector4 vector , float power )
    {
        return new Vector4( Mathf.Pow( vector.x , power ) , Mathf.Pow( vector.y , power ) , Mathf.Pow( vector.z , power ) , Mathf.Pow( vector.w , power ) );
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
    public static Vector3 AveragePosition(params Transform[] t )
    {
        return AveragePosition( PositionsOf( t ) );
    }
    public static Vector3 AveragePosition(params Vector3[] v)
    {
        if(v.Length <= 0) { return default; }
        if(v.Length == 1) { return v[0]; }

        Vector3 r = Vector3.zero;

        foreach(Vector3 a in v)
        {
            r += a;
        }

        return r/v.Length;
    }
    public static Vector3[] PositionsOf(params Transform[] t )
    {
        if (t.Length <= 0) { return default; }

        Vector3[] v = new Vector3[t.Length];
        for(int i = 0 ; i<v.Length ; i++) { v[i] = t[i].position; }

        return v;
    }
    public static void MoveTowards ( Transform transfor , Vector3 position , float speed )
    {
        MoveTowards( transfor , position , speed , speed , 0 );
    }
    public static void MoveTowards(Transform transfor, Vector3 position, float speed, float maxSpeed , float distancePow )
    {
        if (distancePow != 0)
        {
            float d = Mathf.Pow( Mathf.Max( (position-transfor.position).magnitude , 1 ) ,distancePow) ;
            transfor.position = Vector3.MoveTowards( transfor.position , position , Mathf.Min( speed * d , maxSpeed));
        }
        else
        {
            transfor.position = Vector3.MoveTowards( transfor.position , position , speed );
        }
    }

    public static Vector3 Average(params Vector3[] p)
    {
        Vector3 r = Vector3.zero;

        if(p.Length <= 0) { return r; }

        foreach(Vector3 l in p) { r += l; }

        return r / p.Length;
    }
}   
/// <summary>
/// String functions
/// </summary>
public class StringProcess
{
    public static List<char> numbers = new List<char>(10) { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',',','-' };
    public static List<char> alphabetUp = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    public static List<char> alphabetLow = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', };
    public static List<char> signs = new List<char>() { ' '};
    public static List<string> qwerty_azerty = new List<string>(){"aq","wz",";m"};

    /// <summary>
    /// Gives the display size of the given char
    /// </summary>
    /// <param name="c"></param>
    /// <param name="letterLowSize"></param>
    /// <returns></returns>
    public static float CharSize ( char c , float letterLowSize = 1 )
    {
        if (alphabetUp.Contains( c )) { return letterLowSize * 1.25f; }
        if (alphabetLow.Contains( c )) { return letterLowSize; }
        if (numbers.Contains( c )) { return letterLowSize * 1.25f; }
        if (c == ' ') { return letterLowSize * 1.1f; }
        return letterLowSize * .5f;
    }
    /// <summary>
    /// Capitalize the given char
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static char LowToUpChar ( char c )
    {
        return alphabetLow.Contains( c ) ? alphabetUp[alphabetLow.IndexOf( c )] : c;
    }
    /// <summary>
    /// Capitalize the given string
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string Capitalize ( string str )
    {
        if(str.Length <= 0) { return str; }

        string n = LowToUpChar(str[0]).ToString();
        for (int i = 1 ; i < str.Length ; i++)
        {
            n += str[i];
        }
        return n;
    }
    /// <summary>
    /// Returns the numbre of lines of the given string, using 'returnChar' as \n
    /// </summary>
    /// <param name="str"></param>
    /// <param name="returnChar"></param>
    /// <returns></returns>
    public static int StringHeight ( string str , char returnChar = '\n' )
    {
        int r = str.Length > 0 ? 1 : 0;

        foreach (char c in str)
        {
            if (c == returnChar) { r++; }
        }

        return r;
    }
    /// <summary>
    /// Returns the maximum line-lenght of the given string, using 'returnChar' as \n
    /// </summary>
    /// <param name="str"></param>
    /// <param name="returnChar"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Adds a random-string amongs the given string-arrays to the given string; ones then twos then threes ect...
    /// </summary>
    /// <param name="one"></param>
    /// <param name="two"></param>
    /// <param name="three"></param>
    /// <param name="four"></param>
    /// <param name="five"></param>
    /// <param name="six"></param>
    /// <param name="seven"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Returns an array containing the differents lines of the given string, using 'returnChar' as \n
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static List<string> MultiLineToList ( string str , char returnChar = '\n' )
    {
        List<string> list = new List<string>();

        for (int i = 0 ; i < str.Length ; i++)
        {
            string s = "";
            while (str[i] != returnChar && i < str.Length - 1)
            {
                s += str[i];
                i++;
            }
            list.Add( s );
        }

        return list;
    }
    /// <summary>
    /// Returns a string concataining all the strings of  the given array, separated by 'returnChar'
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static string ListToMultiline ( string[] list , char returnChar = '\n' )
    {
        string str = "";

        for (int i = 0 ; i < list.Length ; i++)
        {
            str += list[i] + ( i < list.Length - 1 ? returnChar.ToString() : " " );
        }

        return str;
    }
    /// <summary>
    /// Converts a char-array to a string-array
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Concats the chars into a string
    /// </summary>
    /// <param name="chars"></param>
    /// <returns></returns>
    public static string ListToString ( List<char> chars )
    {
        string str = "";

        foreach (char c in chars)
        {
            str += c;
        }

        return str;
    }
    /// <summary>
    /// Converts a string into a list containing all its chars
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static List<char> StringToList ( string str )
    {
        List<char> lst = new List<char>();

        foreach (char c in str)
        {
            lst.Add( c );
        }

        return lst;
    }
    /// <summary>
    /// Generate a corretly-formated strig to represents the given number of seconds
    /// </summary>
    /// <param name="_seconds"></param>
    /// <param name="secondMinutesSeparator"></param>
    /// <returns></returns>
    public static string TimeToString ( float _seconds , string secondMinutesSeparator = ":", string minutesHoursSeparator = ":")
    {
        float seconds= _seconds % 60;
        int _minutes =(60 > _seconds) ?  Mathf.RoundToInt((_seconds - seconds) / 60) : 0;
        int minutes = _minutes % 60;
        int hours = (60 > _minutes) ? Mathf.RoundToInt((_minutes - minutes) / 60) : 0;

        return TimeToString( (int)seconds , minutes , hours, secondMinutesSeparator,minutesHoursSeparator );
    }
    /// <summary>
    /// Generates a correctly-formated string the represents the given seconds-minutes-hours
    /// </summary>
    /// <param name="_seconds"></param>
    /// <param name="_minutes"></param>
    /// <param name="_hours"></param>
    /// <param name="secondMinutesSeparator"></param>
    /// <param name="minutesHoursSeparator"></param>
    /// <returns></returns>
    public static string TimeToString ( int _seconds , int _minutes = 0 , int _hours = 0 , string secondMinutesSeparator = ":" , string minutesHoursSeparator = ":" )
    {
        return _hours.ToString() + minutesHoursSeparator +
     ( _minutes >= 10 ? "" : "0" ) +
     _minutes.ToString() + secondMinutesSeparator +
     ( _seconds >= 10 ? "" : "0" ) +
     _seconds.ToString( "F0" );
    }
    /// <summary>
    /// Converts qwuertys to azerys and azertys to qwertys
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static string QwertyAzerty ( string c )
    {
        int azerty=0,qwerty=1;

        foreach (string s in qwerty_azerty)
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
    /// <summary>
    /// Generates a string reprenting the given integer with the given amount of chars, by adding "0" in front  of it
    /// </summary>
    /// <param name="n"></param>
    /// <param name="charCount"></param>
    /// <returns></returns>
    public static string IntToString ( int n , int charCount = 1 )
    {
        for (float N = n ; N >= 10 ; N /= 10f) { charCount--; }

        string str = charCount > 0?  RepeatString( "0" , charCount ) : "";

        return str + n.ToString( "F0" );
    }
    /// <summary>
    /// Return a string containing a repetition of the given string
    /// </summary>
    /// <param name="str"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static string RepeatString ( string str , int count )
    {
        while (count > 0) { str += str; count--; }
        return str;
    }
}
/// <summary>
/// Array and List functions
/// </summary>
public class ArrayProcess
{
    /// <summary>
    /// Returns the larger value in the array, outing its index
    /// </summary>
    /// <param name="list"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static float ListMax ( float[] list , out int index )
    {
        float max = 0; int i = 0; index = 0;
        foreach (float f in list)
        {
            if (f > max)
            {
                index = i;
                max = f;
            }
            i++;
        }

        return max;
    }
    /// <summary>
    /// Sums the attributes named 'varName' of the objects-array ; must be a float-int-double-ect attribute
    /// </summary>
    /// <param name="list"></param>
    /// <param name="varName"></param>
    /// <returns></returns>
    public static float ListSum ( object[] list , string varName )
    {
        float r = 0;
        if (list.Length < 1) { return 0; }
        System.Type type = list[0].GetType();

        foreach (var f in list)
        {
            r += (float)( type.GetField( varName ).GetValue( f ) );
        }

        if (float.IsNaN( r ) || float.IsInfinity( r )) { r = 0; }

        return r;
    }
    /// <summary>
    /// Depreciated - use ObjectProbalized instead ; Returns a random element from the array, using the array of probabilities given
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="baseProbabilities"></param>
    /// <param name="objects"></param>
    /// <returns></returns>
    public static object RandomObject<T> ( int[] baseProbabilities , T[] objects )
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
    /// <summary>
    /// Sorts the given array and outs array of the indexes of toSort after being reorganized
    /// </summary>
    /// <param name="toSort"></param>
    /// <param name="order"></param>
    /// <returns></returns>
    public static List<float> ListSorter ( List<float> toSort , out List<int> order)
    {
        List<int> nums = new List<int>();
        order = new List<int>();
        List<float> n2 = new List<float>() ;

        int i = 0;
        while (i < toSort.Count)
        {
            nums.Add( i );
            i++;
        }

        i = 0; int m = toSort.Count;
        while (toSort.Count > 0 && i < m)
        {
            float min = float.MaxValue; int minId = 0;
            int o = 0;
            while (o < toSort.Count)
            {
                if (toSort[o] < min)
                {
                    min = toSort[o];
                    minId = o;
                }
                o++;
            }
            order.Add( nums[minId] );
            n2.Add( toSort[minId] );
            toSort.RemoveAt( minId );
            nums.RemoveAt( minId );
            i++;
        }

        return n2;
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

    public static T[] ConcatElementToArrays<T>(T p, params T[][] arr )
    {
        List<T> l = new List<T>();
        if (p != null) { l.Add( p ); }

        foreach (T[] os in arr)
        {
            foreach (T o in os)
            {
                l.Add( o );
            }
        }

        return l.ToArray();
    }

    public static T[] ConcatElements<T> ( params T[] p )
    {
        return p;
    }

    public static T[] ConcatArrays<T> ( params T[][] arr )
    {
        return ConcatElementToArrays<T>( default(T) , arr );
    }

    public static void RecordingArray<T> ( ref T[] array , T newValue )
    {
        for (int i = array.Length - 1 ; i > 0 ; i--)
        {
            array[i - 1] = array[i];
        }
        array[array.Length - 1] = newValue;
    }
    public static T RandomFromArray<T> ( T[] array , out int index )
    {
        index = Random.Range( 0 , array.Length );
        return array[index];
    }

    public static T RandomFromArray<T> ( T[] array )
    {
        int i;
        return RandomFromArray( array , out i );
    }
    public static void AddToArray<T> ( ref T[] array , T add )
    {
        List<T> list = new List<T>(array);
        list.Add( add );
        array = list.ToArray();
    }
    public static void RemoveFromArray<T> ( ref T[] array , int index )
    {
        if (array.Length <= 0) { return; }
        index = index < 0 ? 0 : index >= array.Length ? array.Length - 1 : index;
        List<T> list = new List<T>(array);
        list.RemoveAt( index );
        array = list.ToArray();
    }

    public static int ToLinearArrayID ( int[] ids , int[] lenghts )
    {
        if (ids.Length <= 0) { return ids[0]; }

        int r =  ids[0];
        int max = Mathf.Min(ids.Length,lenghts.Length);

        for (int i = 1 ; i < max ; i++)
        {
            r += ids[i] * lenghts[i];
        }

        return r;
    }
}
/// <summary>
/// Color functions
/// </summary>
public class ColorProcess
{
    public static float ColorSimilarity ( Color c1 , Color c2 , bool ignoreOpacity = true )
    {
        float r = Mathf.Abs(c1.r - c2.r);
        float g = Mathf.Abs(c1.g - c2.g);
        float b = Mathf.Abs(c1.b - c2.b);
        float a = Mathf.Abs(c1.a - c2.a);

        return 1 - ( ( r + g + b + ( ignoreOpacity ? 0 : a ) ) / ( ignoreOpacity ? 3 : 4 ) );
    }

    public static bool ContainSimilarColor ( Color[] arr , Color color , float threshold , bool ignoreOpacity = true )
    {
        foreach (Color c in arr)
        {
            if (ColorSimilarity( color , c , ignoreOpacity ) >= threshold) { return true; }
        }

        return false;
    }
    public static Color CleanColor ( Color c )
    {
        float coef = 3f/(c.r+c.g+c.b);

        return new Color( c.r * coef , c.g * coef , c.b * coef );
    }
    public static Gradient GradientFromColor ( Color color )
    {
        Gradient gradient = new Gradient();

        gradient.colorKeys = new GradientColorKey[1] { new GradientColorKey( color , 0 ) };
        gradient.alphaKeys = new GradientAlphaKey[1] { new GradientAlphaKey( color.a , 0 ) };

        return gradient;
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
    public static Color LerpBetweenColors ( float cursor , params Color[] colors )
    {
        if (colors.Length <= 0) { return Color.white; }
        if (cursor >= 1) { return colors[colors.Length - 1]; }
        if (cursor <= 0) { return colors[0]; }

        float unit = 1f / colors.Length;
        int id = (int)Mathf.Floor( cursor / unit);

        cursor = ( cursor - ( id * unit ) );

        return LerpColor( colors[id] , colors[id + 1] , cursor );
    }
    public static Color ColorFromVector ( Vector4 vector )
    {
        return new Color( vector.x , vector.y , vector.z , vector.w );
    }

    public static Vector4 VectorFromColor ( Color color )
    {
        return new Vector4( color.r , color.g , color.b , color.a );
    }

}
/// <summary>
///HUD and UI functions
/// </summary>
public class HUDProcess
{
    public static Vector2 WorldToScreen ( Vector3 worldPosition , Camera camera, Vector2 screenSize = default )
    {
        Vector2 a;
        return WorldToScreen( worldPosition , camera , out a , screenSize );
    }
    public static Vector2 WorldToScreen ( Vector3 worldPosition , Camera camera, out Vector2 angles , Vector2 screenSize = default )
    {
        screenSize = screenSize == default ? defaultScreen : screenSize;

        Vector3 d = worldPosition - camera.transform.position;

        angles = new Vector2(
            Vector3.SignedAngle(camera.transform.forward,d.normalized,camera.transform.up) ,/// Camera.VerticalToHorizontalFieldOfView(camera.fieldOfView,defaultRatio),
            Vector3.SignedAngle(camera.transform.forward,d.normalized,camera.transform.right) /// camera.fieldOfView
            );

        return new Vector2(
            ( -screenSize.y / 2 ) + ( screenSize.y * angles.y ) ,
            ( -screenSize.x / 2 ) + ( screenSize.x * angles.x )
            );
    }

    public static Vector2 defaultScreen = new Vector2(1920,1080);
    public static float defaultRatio = defaultScreen.x / defaultScreen.y;
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

        public static string GetLine ( StreamReader reader )
        {
            return reader.ReadLine();
        }

        public static bool EOF ( StreamReader reader )
        {
            return reader.EndOfStream;
        }

        public static string Read ( string path )
        {
            return File.ReadAllText( path );
        }

        public static StreamReader GetReader ( string path )
        {
            return new StreamReader( path );
        }


        public static void CloseReader ( StreamReader reader ) { reader.Close(); }

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

        public static bool Exists ( string path )
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
            return GetColor( t );
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

        public writing ( string str , int _languageIndex = 0 , int _languageCount = 4 )
        {
            _languageCount = Mathf.Max( _languageCount , 0 );
            _languageIndex = Mathf.Min( _languageCount , _languageIndex );

            strings = new string[_languageCount];
            strings[_languageIndex] = str;
        }

        public writing ( params string[] strs )
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

        public bool Contains ( T obj , out CList_Category<T> category ) { category = CList_CategoryOf( obj ); return list.Contains( obj ); }

        public bool Remove ( T obj )
        {
            foreach (CList_Category<T> c in categories)
            {
                if (c.list.Contains( obj )) { c.list.Remove( obj ); list.Remove( obj ); return true; }
            }

            return false;
        }

        public bool Add ( T obj , string category , int createCategoryCapacity = 0 )
        {
            bool b = false; int index = 0; int i = 0;
            foreach (CList_Category<T> c in categories) { if (c.name == category) { b = true; index = i; break; } i++; }

            if (!b)
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

        public CList_Category<T> CList_CategoryOf ( T obj )
        {
            foreach (CList_Category<T> c in categories)
            {
                if (c.list.Contains( obj )) { return c; }
            }
            return null;
        }

        public void AddCList_Category ( List<CList_Category<T>> cat )
        {
            int i =0;
            foreach (CList_Category<T> s in cat)
            {
                categories.Add( new CList_Category<T>( s.name != "" ? s.name : i.ToString() , s.capacity > 0 ? s.capacity : 10000 ) ); i++;
            }
        }

        public List<T> Get () { return list.GetRange( 0 , list.Capacity ); }
        public List<T> Get ( string category )
        {
            CList_Category<T> c = GetCategory(category);
            return c != null ? c.list != null ? c.list : new List<T>( 0 ) : new List<T>( 0 );
        }

        public CList_Category<T> GetCategory ( string category )
        {
            foreach (CList_Category<T> c in categories)
            {
                if (c.name == category) { return c; }
            }
            return null;
        }

        public void Setup ( string[] catsNames , int[] catsCapacities )
        {
            list = new List<T>();
            categories = new List<CList_Category<T>>();
            for (int i = 0 ; i < catsNames.Length ; i++)
            {
                categories.Add( new CList_Category<T>( catsNames[i] , catsCapacities[i] ) );
            }
        }

        //Constructors

        public CList ( int capacity , int categoriesCount = 1 )
        {
            list.Capacity = capacity;
            categories = new List<CList_Category<T>>();
            AddCList_Category( new List<CList_Category<T>>( categoriesCount ) );
        }
        public CList ( List<T> source , int categoriesCount = 1 )
        {
            list = new List<T>( source );
            categories = new List<CList_Category<T>>();
            AddCList_Category( new List<CList_Category<T>>( categoriesCount ) );
        }
        public CList ( List<T> source , List<CList_Category<T>> categoriesAdd )
        {
            list = new List<T>( source );
            categories = new List<CList_Category<T>>();
            AddCList_Category( categoriesAdd );
        }

        public static implicit operator List<T> ( CList<T> c ) { return c.list; }

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
        public trilean ( short val )
        {
            v = val;
            v = v < -1 ? (short)-1 : v > 1 ? (short)1 : v;
        }

        /// <summary>
        /// Combine two bools into a trilean.
        /// </summary>
        public static trilean Combine ( bool a , bool b )
        {
            if (a == b) { return a ? trilean.True : trilean.False; }
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
            if (a == trilean.Null || b == trilean.Null) { return trilean.Null; }

            if (a == b) { return a; }
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

        public trilean ( bool b ) { v = (short)( b ? 1 : -1 ); }

        /// <summary>
        /// Return true if the trilean is not equal to Null
        /// </summary>

        public string ToString ( int simple_number_both = 0 )
        {
            string str = "";
            switch (v) { case -1: str += "False"; break; case 1: str += "True"; break; default: str += "Null"; break; }
            switch (simple_number_both) { default: break; case 1: str = v.ToString(); break; case 2: str += v.ToString(); break; }
            return str;
        }
        public static bool operator == ( trilean a , trilean b )
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
            if (a != b && ( a == trilean.True || b == trilean.True )) { return true; }
            else { return false; }
        }
        /// <summary>
        /// Return true if at least one trilean is true
        /// </summary>
        public static bool operator | ( trilean a , trilean b )
        {
            if (a == trilean.True || b == trilean.True) { return true; }
            else { return false; }
        }

        /// <summary>
        /// Combine two trilean a and b. 'True' if both are true, 'False' if both are false, 'Null' otherwise.
        /// </summary>
        public static trilean operator * ( trilean a , trilean b )
        {
            return trilean.Combine( a , b );
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
            if (b == trilean.Null) { return trilean.Null; }
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

        public small ( int value ) { int v2 = value; v2 = v2 > 1 ? 1 : v2 < -1 ? -1 : v2; v = (sbyte)v2; }

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

        public flaot ( float a ) { value = a; }

        public static implicit operator flaot ( float a )
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

        public coefficient ( float val , bool over )
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
    public float middle { get { return ( _top + _bottom ) * .5f; } }
    public float amplitude { get { return top - bottom; } }
    public float random { get { return Random.Range( bottom , top ); } }

    public static interval Null { get { return new interval( 0 , 0 ); } }
    public static interval Default { get { return new interval( -1 , 1 ); } }
    public interval ( float _top_ , float _bottom_ ) { _bottom = _bottom_; _top = _top_; bottom = _bottom; top = _top; }
    public static interval CreateFromTop ( float _top_ , float _amplitude_ ) { return new interval( _top_ , _top_ - _amplitude_ ); }
    public static interval CreateFromBottom ( float _bottom_ , float _amplitude_ ) { return new interval( _bottom_ + _amplitude_ , _bottom_ ); }

    /// <summary>
    /// Cut the interval at the given positions
    /// </summary>
    /// <param name="ats"></param>
    /// <returns></returns>
    public interval[] Cut(params float[] ats )
    {
        if(ats.Length <= 0) { return this; }

        List<interval> ints = new List<interval>();

        foreach(float at in ats)
        {
            if(at <= bottom || at >= top) { continue; }

            if(ints.Count <= 0) { ints.Add(new interval(at,bottom)); ints.Add( new interval( top , at ) ); }
            else
            {
                foreach(interval i in ints)
                {
                    if (i.Contains( at ))
                    {
                        ints.Add( new interval( at , i.bottom ) ); ints.Add( new interval( i.top , at ) ); break;
                    }
                }
            }
        }

        return ints.ToArray();
    }

    /// <summary>
    /// Separate the interval into to parts, separated by a gap from b to t
    /// </summary>
    /// <param name="b"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public interval[] Separate ( float b , float t )
    {
        if (b <= bottom) { return new interval[1] { new interval( top , t ) }; }

        if (t >= top) { return new interval[1] { new interval( b , bottom ) }; }

        return new interval[2] { new interval( b , bottom ) , new interval( top , t ) };
    }

    public float DistanceFrom(float v )
    {
        if (Contains( v )) { return 0; }

        return Mathf.Min( Mathf.Abs( v - bottom ) , Mathf.Abs( v - top ) );
    }

    public float MinEdgeDistance(float v ){  return Mathf.Min( TopDistance( v ) , BottomDistance( v ) ); }
    public float MaxEdgeDistance(float v ){  return Mathf.Max( TopDistance( v ) , BottomDistance( v ) ); }
    public float TopDistance(float v ) { return Mathf.Abs( top - v ); }
    public float BottomDistance(float v ) { return Mathf.Abs( bottom - v ); }

    /// <summary>
    /// True if the closest edge is Top, false if it is Bottom | True if a the same distance
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public bool ClosestEdge(float v ) { return TopDistance( v ) <= BottomDistance( v ); }
    public bool Contains ( float v , bool exclusiveBot = false , bool exclusiveTop = false )
    {
        return  exclusiveBot ?  ( v > _bottom && v <= _top ) :
                exclusiveTop ?  ( v >= _bottom && v < _top ) :
                                ( v >= _bottom && v <= _top ); 
    }
    public bool Intersect ( interval a )
    {
        return Contains( a.bottom ) || Contains( a.top );
    }
    public void MoveRight(float v ) { _bottom += v; _top += v; }
    public void MoveLeft(float v ) { _bottom -= v; _top -= v; }

    public string ToString (string F = "F1", char a = '[' , char b = ',' ,  char c = ']')
    {
        return a.ToString() + " " + _bottom.ToString( F ) + " " + b.ToString() + " " + _top.ToString( F ) + " " + c.ToString() ;
    }

    public float GetRatio ( float v , bool crop = false )
    {
        float r = ( v - bottom ) / ( top - bottom );
        return !crop ? r : ( r < 0 ? 0 : r > 1 ? 1 : r );
    }

    public float Get ( float r )
    {
        return Mathf.Lerp( bottom , top , r );
    }

    public static implicit operator float ( interval i ) { return i.random; }
    public static implicit operator interval[] ( interval i ) { return new interval[1] { i }; }
}

[System.Serializable]
public struct AudioElement
    {
        public AudioClip clip;
        public float probability;
        public interval pitch;

        public static AudioElement NULL { get { return new AudioElement( null , 0 ); } }

        public AudioElement ( AudioClip _clip ) { clip = _clip; probability = 1; pitch = new interval( 1 , 1 ); }
        public AudioElement ( AudioClip _clip , float _p ) { clip = _clip; probability = _p; pitch = new interval( 1 , 1 ); }
        public AudioElement ( AudioClip _clip , float _p , interval _pitch ) { clip = _clip; probability = _p; pitch = _pitch; }

        public static void SetupAudioSource ( AudioSource a , AudioElement ae , float _pitch = -1 )
        {
            a.clip = ae.clip;
            a.pitch = _pitch < 0 ? ae.pitch : _pitch;
        }

        public static implicit operator AudioClip ( AudioElement a ) { return a.clip; }
        public static implicit operator float ( AudioElement a ) { return a.probability; }
        public static implicit operator interval ( AudioElement a ) { return a.pitch; }

        public static AudioElement GetRandom ( AudioElement[] audioElements )
        {
            ObjectProbalized<AudioElement>[] list = new  ObjectProbalized<AudioElement>[audioElements.Length];

            for (int i = 0 ; i < audioElements.Length ; i++) { list[i] = new ObjectProbalized<AudioElement>( audioElements[i] , audioElements[i].probability ); }

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
        public ObjectProbalized ( T _obj , float _p ) { obj = _obj; probability = _p; }
        public ObjectProbalized ( bool _null ) { obj = default; probability = 1; isNull = _null; }

        public bool IsNull () { return isNull; }

        public static void SetInterval ( ref ObjectProbalized<T>[] array )
        {
            ObjectProbalized<T>[] _arr = new ObjectProbalized<T>[array.Length]; array.CopyTo( _arr , 0 );
            if (Sum( _arr ) > 1) { Flatten( ref _arr ); }

            //Debug.Log( _arr.Length );
            float bottom= 0; int id = 0;
            foreach (ObjectProbalized<T> obj in array)
            {
                //Debug.Log( bottom.ToString("F2") + " | " + _arr[id].probability.ToString("F2") + " or " + obj.probability.ToString("F2"));

                obj.interval = interval.CreateFromBottom( bottom , _arr[id].probability );
                bottom += _arr[id].probability;
                id++;

                //Debug.Log( obj.interval.ToString( "F2" ) );
            }
        }

        public static float Sum ( ObjectProbalized<T>[] array )
        {
            float sum =0;
            foreach (ObjectProbalized<T> o in array) { sum += o.probability; }
            return sum;
        }

        public static float Flatten ( ref ObjectProbalized<T>[] array )
        {
            float sum = Sum( array ); if (sum == 0) { return 0; }
            foreach (ObjectProbalized<T> o in array) { o.probability /= sum; }
            return sum;
        }

        public static ObjectProbalized<T> Get ( ObjectProbalized<T>[] array , out int id )
        {
            if (array.Length == 0) { id = -1; return null; }
            if (array.Length == 1 && array[0].isNull) { id = -1; return null; }

            ObjectProbalized<T>[] _arr = new ObjectProbalized<T>[array.Length]; array.CopyTo( _arr , 0 ); Flatten( ref _arr );

            SetInterval( ref _arr );
            float v = Random.Range(0,1f);

            id = 0;
            foreach (ObjectProbalized<T> obj in _arr)
            {
                if (obj.interval.Contains( v )) { return obj; }
                id++;
            }
            id = -1;

            return null;
        }

        public static ObjectProbalized<T> Get ( ObjectProbalized<T>[] array )
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

        public static T[] ConvertArray ( ObjectProbalized<T>[] objectProbalizeds )
        {
            T[] array = new T[objectProbalizeds.Length];

            for (int i = 0 ; i < array.Length ; i++) { array[i] = objectProbalizeds[i].obj; }

            return array;
        }

        public static ObjectProbalized<T>[] SetArray ( T[] array , float[] probas = null )
        {
            ObjectProbalized<T>[] arr = new ObjectProbalized<T>[array.Length];

            for (int i = 0 ; i < array.Length ; i++)
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
        public ObjectProbalizedTyped ( T _obje , float _p , string _t ) { obj = _obje; probability = _p; type = _t; }
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

        public static float GetAround ( float center , float amplitude , float curve )
        {
            return GetBetween( center - amplitude / 2f , center + amplitude / 2f , curve );
        }

        public static float GetBetween ( float min , float max , float curve = 1 )
        {
            return Mathf.Pow( 1f / curve ,
                Random.Range(
                    Mathf.Pow( min , curve ) , Mathf.Pow( max , curve ) ) );
        }
    }

[System.Serializable]
public class toggler
{
    private List<trilean> toggles;
    public trilean[] trileans { get { List < trilean > t = new List<trilean>( toggles ); t.Add( main ); return t.ToArray(); } }
    [SerializeField]private trilean main;
    [SerializeField]private Type type;

    public bool Get ()
    {
        if (toggles.Count <= 0) { return main; }
        switch (type)
        {
            default: goto case Type.AND;
            case Type.AND:
                foreach (trilean b in trileans) { if (b.IsFalse) { return false; } }
                return true;
            case Type.OR:
                foreach (trilean b in trileans) { if (b.IsTrue) { return true; } }
                return false;
        }
    }

    public int Set(bool b, int i = -1 ) { return Set( new trilean( b ) , i ); }

    public int Set ( trilean b  , int i = -1 )
    {
        if (i < 0 || i >= toggles.Count)
        {
            toggles.Add( b ); return toggles.Count - 1;
        }
        else
        {
            toggles[i] = b; return i;
        }
    }

    public void Remove ( int id ) { if (id >= 0 && id < toggles.Count) { toggles.RemoveAt( id ); } }

    public toggler ( Type t , bool m, params bool[] b )
    {
        type = t;
        main = new trilean( m );
        toggles = new List<trilean>();
        foreach (bool ba in b)
        {
            toggles.Add( new trilean( ba ) );
        }
    }
    public toggler ( Type t ,trilean m, params trilean[] b )
    {
        type = t;
        main = m;
        toggles = new List<trilean>( b );
    }
    public static toggler ORYES { get { return new toggler( Type.OR , true ); } }
    public static toggler ORNO { get { return new toggler( Type.OR , false ); } }
    public static toggler ANDYES { get { return new toggler( Type.AND , true ); } }
    public static toggler ANDNO { get { return new toggler( Type.AND , false ); } }

    public static implicit operator bool ( toggler t ) { return t.Get(); }

    [System.Serializable]
    public struct actor
    {
        private toggler toggle;
        private int id;

        public actor ( toggler t ) { toggle = t; id = toggle.Set( false ); }

        public bool Get () { return toggle; }
        public void Yes () { toggle.Set( true , id ); }
        public void No () { toggle.Set( false , id ); }
        public void Neutral () { toggle.Set( trilean.Null , id ); }
        public void Release () { toggle.Remove( id ); }

        public static implicit operator bool ( actor t ) { return t.Get(); }
    }

    public struct replica
    {
        private toggler toggle;

        public replica ( toggler t ) { toggle = t; }

        public bool Get () { return toggle; }

        public static implicit operator bool ( replica t ) { return t.Get(); }
    }

    public enum Type
    {
        AND,
        OR
    }
}


[System.Serializable]
public class combiner
{
    private List<float> coefs = new List<float>();
    public float[] floats { get { List < float > t = new List<float>( coefs ); t.Add( main ); return t.ToArray(); } }
    [SerializeField]private float main = 1;
    [SerializeField]private Type type = Type.MULT;

    public float Get ()
    {
        float r = main;

        if (coefs != null)
        {
            foreach (float f in coefs)
            {
                switch (type)
                {
                    default: goto case Type.MULT;
                    case Type.MULT: r *= f; break;
                    case Type.ADD: r += f; break;
                }
            }
        }
        else
        {
            coefs = new List<float>();
        }

        return r;
    }

    public int Set(float f = float.NaN, int i = -1 )
    {
        if(f == float.NaN)
        {
            f = NeutralValue;
        }

        coefs ??= new List<float>();

        if (i < 0 || i >= coefs.Count)
        {
            coefs.Add( f ); return coefs.Count - 1;
        }
        else
        {
            coefs[i] = f; return i;
        }
    }

    public void Remove ( int id ) { if (coefs == null) { return; } if (id >= 0 && id < coefs.Count) { coefs.RemoveAt( id ); } }

    public combiner ( Type t , float m , params float[] f )
    {
        type = t;
        main = m;
        coefs = new List<float>();
        foreach (float a in f)
        {
            coefs.Add( a );
        }
    }

    public static combiner MULT { get { return new combiner( Type.MULT , 1 ); } }
    public static combiner ADD { get { return new combiner( Type.ADD , 0 ); } }

    public static implicit operator float ( combiner t ) { return t.Get(); }

    [System.Serializable]
    public struct actor
    {
        private combiner combine;
        private int id;

        public actor ( combiner t ) { combine = t; id = combine.Set(  ); }

        public float Get () { return combine; }
        public void Release () { combine.Remove( id ); }

        public static implicit operator float ( actor t ) { return t.Get(); }
    }

    public float NeutralValue { get
        {
            switch (type)
            {
                default:goto case Type.ADD; 
                case Type.ADD: return 0;
                case Type.MULT: return 1; 
            }
        } }

    public enum Type
    {
        ADD,
        MULT
    }
}

[System.Serializable]
public struct Normal
{
    public Vector3 point;
    public Vector3 vector;
    public float height;
    public bool isNull;

    public static Normal Null{get{return new Normal ( Vector3.zero, Vector3.zero,true); }}

    public Normal(Vector3 p, Vector3 v ) { point = p; vector = v; isNull = false; height = vector.magnitude; }
    public Normal(Vector3 p, Vector3 v, bool n) { point = p; vector = v; isNull = n; height = vector.magnitude; }

    public static Normal AverageOf(params Normal[] normals)
    {
        Vector3 p = Vector3.zero; Vector3 d = Vector3.zero; bool nu = true;

        foreach(Normal n in normals)
        {
            if (nu) { nu = nu && n.isNull; }
            if (n.isNull) { continue; }
            p += n.point;
            d += n.vector;
        }

        if (nu) { return Null; }

        return new Normal( p / normals.Length , d / normals.Length, nu );
    }
}

[System.Serializable]
public class LineArray<T>
{
    private float width = 1; public float getWidth { get { return width; } }

    [SerializeField] private List<Element<T>> list; public Element<T>[] getList { get { return list.ToArray(); } } public int count { get { return list == null ? 0 : list.Count; } }

    private List<intervalID> intervals; public intervalID[] getIntervals { get { return intervals.ToArray(); } }

    public int Add(T obj,float wid,AddMode mode = AddMode.Random )
    {
        if(intervals.Count <= 0) { return -1; }

        int r = -1; interval ninte = interval.Null;
        switch (mode)
        {
            default:goto case AddMode.Random;

            case AddMode.Random:
                List<intervalID> rlist = new List<intervalID>(intervals);

                for (int i = 0 ; i < intervals.Count ; i++)
                {
                    int ran = Random.Range(0,rlist.Count);
                    intervalID itd = rlist[ran];

                    if (itd.inter.amplitude < wid) { rlist.RemoveAt( ran ); }
                    else
                    {
                        float wid2 = wid/2f;
                        float mid =(itd.inter.top+itd.inter.bottom)/2f;
                        ninte = new interval( mid + wid2 , mid - wid2 );
                        r = itd.id ;
                        break;
                    }
                }
                break;

            case AddMode.Center:
                List<intervalID> nlist = new List<intervalID>(intervals);

                for (int i = 0 ; i < intervals.Count ; i++)
                {
                    int itdID; float w2 = width/2f;
                    intervalID itd = GetIntervalAtPosition( w2 , out itdID , nlist);
                    if (itd.inter.amplitude < wid) { nlist.RemoveAt( itdID ); }
                    else
                    {
                        float wid2 = wid/2f;
                        float mid = 
                            itd.inter.Contains(w2) ? 
                                (itd.inter.MinEdgeDistance(w2) >= wid ? (w2) :
                                                                        (itd.inter.ClosestEdge(w2) ?    (itd.inter.top - wid2) :
                                                                                                        (itd.inter.bottom + wid2) )) :
                            (itd.inter.bottom >= w2) ?  (itd.inter.bottom + wid2) :
                            (itd.inter.top <= w2) ?     (itd.inter.top - wid2) :
                                                        ((itd.inter.top+itd.inter.bottom)/2f);

                        ninte = new interval( mid + wid2 , mid - wid2 );
                        r = (itd.id == 0) ? 0 : (itd.id + 1) ;
                        break;
                    }
                }
                break;

            case AddMode.Right: goto case AddMode.Left;
            case AddMode.Left:
                int min = (mode == AddMode.Left) ? 0 : (intervals.Count - 1);
                int max = (mode == AddMode.Left) ? (intervals.Count - 1) : 0;

                for (int i = min ; ( mode == AddMode.Left ) ? ( i <= max ) : ( i >= max ) ; i += ( mode == AddMode.Left ) ? 1 : -1)
                {
                    if (intervals[i].inter.amplitude >= wid)
                    {
                        ninte = ( mode == AddMode.Left ) ? interval.CreateFromBottom( intervals[i].inter.bottom , wid ) : interval.CreateFromTop( intervals[i].inter.top , wid );
                        r = intervals[i].id + 1; break;
                    }
                }
                break;
        }

        if (r == -1) { return -1; }

        if (r >= count || count <= 0)
        {
            list.Add( new Element<T>( obj , ninte ) ); Debug.Log( "Added at :" + r);
        }
        else
        {
            list.Insert( r , new Element<T>( obj , ninte ) ); Debug.Log( "Inserted at :" + r );
        }

        UpdateIntervals();
        return r;
    }

    void UpdateIntervals ()
    {
        intervals = new List<intervalID>();
        interval last = new interval(width,0);

        if (count <= 0) { intervals.Add(new intervalID( last , 0)); return; }

        for(int i = 0 ; i < count ;i++)
        {
            intervalID[] arr = intervalID.FromIntervalArray(last.Separate( Get(i).position.bottom , Get(i).position.top ));

            string str = i.ToString() + "   |   ";
            foreach(intervalID idt in arr) { str += idt.ToString() + " "; } 

            if(i == (count - 1))
            {
                arr[0].id = i;  
                intervals.Add( arr[0] );

                if (arr.Length == 2)
                {
                    arr[1].id = i + 1;
                    intervals.Add( arr[1] );
                }

                last = arr[0];
            }
            else if (arr.Length == 2)
            {
                arr[0].id = i;
                intervals.Add( arr[0] );        //If the separation returns 2 intervals, then the first is bellow the current element ; if only 1, it is above it
                last = arr[1];
            }
            else { last = arr[0];}

            str += "    " + last.ToString();
            Debug.Log( str );
        }

        foreach(intervalID it in intervals.ToArray())
        {
            if(it.inter.amplitude <= 0) { intervals.Remove( it ); }
        }

    }


    /*
    public int Add(T obj, float wid, out interval[] valids , AddMode mode = AddMode.Random )
    {
        if (list == null) { list = new List<Element<T>>(); }

        if (list.Count > 0)
        {
            interval inter = interval.CreateFromBottom(0,wid);
            List<interval> li = new List<interval>();
            List<int> ids = new List<int>();

            int i = -1;
            foreach (Element<T> e in list)
            {
                i++;

                if (inter.Contains( width , true )) { break; }                                 //Stop if we reach the top-limit of the LineArray
                else if (inter.Contains( e.position.bottom ))
                {
                    inter.MoveRight( e.position.top - inter.bottom ); continue;      //If we intersect an element on the right, jump over it to the right 
                }
                else if (inter.Contains( e.position.top ))
                {
                    inter.MoveRight( e.position.top - inter.bottom );  continue;         //If we intersect an element on the left, move to the right
                }
                else
                {
                    li.Add( inter ); ids.Add( i );                              //If we do not intersect with anything, add the current interval to the list, and move to the right
                    inter.MoveRight( wid ); continue;
                }
            }

            while( !inter.Contains(width,false,true) && i < 50)
            {
                li.Add( inter ); ids.Add( i ); i++;
                inter.MoveRight( wid );
            }

            if (li.Count <= 0) { valids = new interval[0]; return -1; }
            valids = li.ToArray();

            int selected = 0;
            switch (mode)
            {
                default: goto case AddMode.Random;
                case AddMode.Random:
                    selected = Random.Range( 0 , ids.Count );
                    break;

                case AddMode.Left:
                    selected = 0;
                    break;

                case AddMode.Right:
                    selected = valids.Length - 1;
                    break;

                case AddMode.Center:
                    float mid = width/2f; selected = 0; float min = float.MaxValue;
                    for (i = 0 ; i < valids.Length ; i++)
                    {
                        if (valids[i].Contains( mid )) { selected = i; break; }

                        float topD = (mid - valids[i].top);
                        if (topD > 0 && topD < min) { min = topD; selected = i; continue; }

                        float botD = (valids[i].bottom - mid);
                        if (botD > 0 && botD < min) { min = botD; selected = i; continue; }
                    }

                    break;
            }

            list.Insert(Mathf.Min(ids[selected],list.Count-1) , new Element<T>( obj , valids[selected] ) );
            return selected;
        }
        else
        {
            switch (mode)
            {
                default: goto case AddMode.Random;
                case AddMode.Random:
                    float bot1 = Random.Range(0,width-wid);
                    list.Add( new Element<T>( obj , interval.CreateFromBottom(bot1, bot1 + wid ) ));
                    break;

                case AddMode.Left:
                    list.Add(new Element<T>( obj , interval.CreateFromBottom( 0 , wid ) ) );
                    break;

                case AddMode.Right:
                    list.Add( new Element<T>( obj , interval.CreateFromBottom( width - wid , width ) ) );
                    break;

                case AddMode.Center:
                    float bot0 = (width/2f) - (wid/2f); 
                    list.Add( new Element<T>( obj , interval.CreateFromBottom( bot0 , bot0 + wid ) ) );
                    break;
            }
            valids = new interval[0];
            return 0;
        }
    }
    */

    public Element<T> Get(int id ) { return list == null ? null : id < 0 ? null : id > list.Count ? null : list[id]; }
    public void Set ( T obj , int id )
    {
        if (list == null ? true : ( id < 0 || id > list.Count )) { return; }

        list[id].obj = obj;
    }

    public intervalID GetIntervalAtPosition(float p , out int itdID , List<intervalID> nlist = null  )
    {
        if (nlist == null) { nlist = intervals; }
        intervalID inter = intervals.Count <= 0 ? (intervalID)interval.Null : intervals[0];

        float minD = float.MaxValue; int i = 0; itdID = 0;
        foreach (intervalID id in nlist)
        {
            if (id.inter.Contains( p )) { itdID =i; return id; }

            float d = id.inter.DistanceFrom(p);
            if(d < minD) { inter = id; minD = d; itdID = i; }

            i++;
        }

        return inter;
    }

    public void RemoveAt(int i )
    {
        if(i < 0 || i >= list.Count) { return; }
        list.RemoveAt( i );
    }

    public bool empty { get { return list == null; } }

    public Element<T>[] ToArray ()
    {
        return list.ToArray();
    }

    public LineArray(float w ) { width = w; list = new List<Element<T>>(); intervals = new List<intervalID>(); UpdateIntervals(); }

    public class Element<T>
    {
        public T obj;
        public interval position;

        public Element (T o, interval i) {obj = o ; position = i;}
    }

    public struct intervalID
    {
        public interval inter;
        public int id;

        public intervalID ( interval i ) { inter = i; id = 0; }
        public intervalID ( interval i, int d ) { inter = i; id = d; }

        public static implicit operator interval (intervalID i ) { return i.inter; }
        public static implicit operator intervalID (interval i ) { return new intervalID(i); }

        public static intervalID[] FromIntervalArray ( interval[] ints )
        {
            intervalID[] arr = new intervalID[ints.Length];
            for(int i = 0 ; i < ints.Length ;i++)
            {
                arr[i] = new intervalID( ints[i] );
            }
            return arr;
        }

        public string ToString ( string F = "F1" , char a = '[' , char b = ',' , char c = ']' ) { return inter.ToString(F,a,b,c); }
    }

    public static implicit operator Element<T>[] (LineArray<T> l ) { return l.list.ToArray(); }

    public enum AddMode
    {
        Random,
        Left,
        Right,
        Center
    }
}


