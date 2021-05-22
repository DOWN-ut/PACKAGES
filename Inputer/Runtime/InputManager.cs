using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header( "Parameters" )]

    public bool debug;

    public Input_Profile profile;

    [Header( "Ingame" )]

    public Dictionary<KEY, InputElement> rawInputs;

    public List<InputElement> gameinputs;

    public Vector2 movement {
        get
        {
            return new Vector2( Get( "rightward" ) ? 1 : Get( "leftward" ) ? -1 : 0, Get( "forward" ) ? 1 : Get( "backward" ) ? -1 : 0 );
        }
    }
    public Vector2 look { get { _look = GetV( "look" ); return profile.Look(_look); } }
    [SerializeField]
    Vector2 _look ;

    [Header("References")]

    public INPUT inputActions;

    #region Main 

    private void Awake ()
    {
        instance = this;

        DontDestroyOnLoad( gameObject );

        SetupInputer(); SetupGameInputs();
    }

    #endregion

    #region Setup

    [System.Serializable]
    public enum KEY
    {
        A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,_0,_1,_2,_3,_4,_5,_6,_7,_8,_9,LEFTCTRL,RIGHTCTRL,LEFTSHIFT,RIGHTSHIFT,TAB,SPACE,ENTER,ECHAP,MOUSELEFT,MOUSERIGHT,MOUSEMIDDLE,MOUSE,WHEEL,WHEELUP,WHEELDOWN,
        L1,L2,L3,R1,R2,R3,LEFTJOYSTICK,RIGHTJOYSTICK,NORTH,SOUTH,EAST,WEST,PADUP,PADDOWN,PADLEFT,PADRIGHT
    }

    void SetupInputer ()
    {
        inputActions = new INPUT();

        rawInputs = new Dictionary<KEY , InputElement>();

        inputActions.Keyboard.a.performed += a => Key( KEY.A ); rawInputs.Add( KEY.A , false );
        inputActions.Keyboard.b.performed += b => Key( KEY.B ); rawInputs.Add(KEY.B , false );
        inputActions.Keyboard.c.performed += c => Key( KEY.C ); rawInputs.Add(KEY.C , false );
        inputActions.Keyboard.d.performed += d => Key( KEY.D); rawInputs.Add( KEY.D, false );
        inputActions.Keyboard.e.performed += e => Key( KEY.E ); rawInputs.Add(KEY.E, false );
        inputActions.Keyboard.f.performed += f => Key( KEY.F); rawInputs.Add( KEY.F, false );
        inputActions.Keyboard.g.performed += g => Key( KEY.G); rawInputs.Add( KEY.G, false );
        inputActions.Keyboard.h.performed += h => Key( KEY.H); rawInputs.Add( KEY.H, false );
        inputActions.Keyboard.i.performed += i => Key( KEY.I); rawInputs.Add( KEY.I, false );
        inputActions.Keyboard.j.performed += j => Key( KEY.J); rawInputs.Add( KEY.J, false );
        inputActions.Keyboard.k.performed += k => Key( KEY.K); rawInputs.Add( KEY.K, false );
        inputActions.Keyboard.l.performed += l => Key( KEY.L); rawInputs.Add( KEY.L, false );
        inputActions.Keyboard.m.performed += m => Key( KEY.M); rawInputs.Add( KEY.M, false );
        inputActions.Keyboard.n.performed += n => Key( KEY.N); rawInputs.Add( KEY.N, false );
        inputActions.Keyboard.o.performed += o => Key( KEY.O); rawInputs.Add( KEY.O, false );
        inputActions.Keyboard.p.performed += p => Key( KEY.P); rawInputs.Add( KEY.P, false );
        inputActions.Keyboard.q.performed += q => Key( KEY.Q); rawInputs.Add( KEY.Q, false );
        inputActions.Keyboard.r.performed += r => Key( KEY.R); rawInputs.Add( KEY.R, false );
        inputActions.Keyboard.s.performed += s => Key( KEY.S); rawInputs.Add( KEY.S, false );
        inputActions.Keyboard.t.performed += t => Key( KEY.T); rawInputs.Add( KEY.T, false );
        inputActions.Keyboard.u.performed += u => Key( KEY.U); rawInputs.Add( KEY.U, false );
        inputActions.Keyboard.v.performed += v => Key( KEY.V); rawInputs.Add( KEY.V, false );
        inputActions.Keyboard.w.performed += w => Key( KEY.W); rawInputs.Add( KEY.W, false );
        inputActions.Keyboard.x.performed += x => Key( KEY.X); rawInputs.Add( KEY.X, false );
        inputActions.Keyboard.y.performed += y => Key( KEY.Y); rawInputs.Add( KEY.Y, false );
        inputActions.Keyboard.z.performed += z => Key( KEY.Z); rawInputs.Add( KEY.Z, false );

        inputActions.Keyboard._1.performed += _1 => Key(KEY._1 ); rawInputs.Add( KEY._1 , false );
        inputActions.Keyboard._2.performed += _2 => Key(KEY._2); rawInputs.Add( KEY._2 , false );
        inputActions.Keyboard._3.performed += _3 => Key(KEY._3 ); rawInputs.Add( KEY._3 , false );
        inputActions.Keyboard._4.performed += _4 => Key(KEY._4); rawInputs.Add( KEY._4 , false );
        inputActions.Keyboard._5.performed += _5 => Key(KEY._5 ); rawInputs.Add( KEY._5 , false );
        inputActions.Keyboard._6.performed += _6 => Key(KEY._6 ); rawInputs.Add( KEY._6 , false );
        inputActions.Keyboard._7.performed += _7 => Key(KEY._7 ); rawInputs.Add( KEY._7 , false );
        inputActions.Keyboard._8.performed += _8 => Key(KEY._8 ); rawInputs.Add( KEY._8 , false );
        inputActions.Keyboard._9.performed += _9 => Key(KEY._9 ); rawInputs.Add( KEY._9 , false );
        inputActions.Keyboard._0.performed += _0 => Key( KEY._0 ); rawInputs.Add( KEY._0 , false );

        inputActions.Keyboard.left_ctrl.performed += left_ctrl => Key( KEY.LEFTCTRL); rawInputs.Add( KEY.LEFTCTRL, false );
        inputActions.Keyboard.right_ctrl.performed += right_ctrl => Key( KEY.RIGHTCTRL); rawInputs.Add( KEY.RIGHTCTRL, false );
        inputActions.Keyboard.left_shift.performed += left_shift => Key( KEY.LEFTSHIFT); rawInputs.Add( KEY.LEFTSHIFT, false );
        inputActions.Keyboard.right_shift.performed += right_shift => Key( KEY.RIGHTSHIFT ); rawInputs.Add( KEY.RIGHTSHIFT, false );
        inputActions.Keyboard.tab.performed += tab => Key( KEY.TAB); rawInputs.Add( KEY.TAB , false );
        inputActions.Keyboard.space.performed += space => Key( KEY.SPACE); rawInputs.Add( KEY.SPACE, false );
        inputActions.Keyboard.enter.performed += enter => Key( KEY.ENTER); rawInputs.Add( KEY.ENTER , false );
        inputActions.Keyboard.echap.performed += echap => Key( KEY.ECHAP ); rawInputs.Add( KEY.ECHAP , false );


        inputActions.Keyboard.mouse_left.performed += mouse_left => Key( KEY.MOUSELEFT); rawInputs.Add( KEY.MOUSELEFT, false );
        inputActions.Keyboard.mouse_right.performed += mouse_right => Key( KEY.MOUSERIGHT ); rawInputs.Add( KEY.MOUSERIGHT, false );
        inputActions.Keyboard.mouse_middle.performed += mouse_left => Key( KEY.MOUSEMIDDLE ); rawInputs.Add( KEY.MOUSEMIDDLE , false );

        inputActions.Keyboard.mouse.performed += mouse => Key( KEY.MOUSE, mouse.ReadValue<Vector2>(),true ); rawInputs.Add( KEY.MOUSE , Vector2.zero );

        inputActions.Keyboard.wheel.performed += wheel => Key( KEY.WHEEL, wheel.ReadValue<Vector2>(),true ); rawInputs.Add( KEY.WHEEL , Vector2.zero );
        inputActions.Keyboard.wheel.performed += wheel => Key( KEY.WHEELUP , wheel.ReadValue<Vector2>().y > 0 , true ); rawInputs.Add( KEY.WHEELUP , Vector2.zero );
        inputActions.Keyboard.wheel.performed += wheel => Key( KEY.WHEELDOWN , wheel.ReadValue<Vector2>().y < 0 , true ); rawInputs.Add( KEY.WHEELDOWN , Vector2.zero );

        inputActions.Keyboard.L1.performed += L1 => Key( KEY.L1 ); rawInputs.Add( KEY.L1 , false );
        inputActions.Keyboard.L2.performed += L2 => Key( KEY.L2 ); rawInputs.Add( KEY.L2 , false );
        inputActions.Keyboard.L3.performed += L3 => Key( KEY.L3 ); rawInputs.Add( KEY.L3 , false );
        inputActions.Keyboard.R1.performed += R1 => Key( KEY.R1 ); rawInputs.Add( KEY.R1 , false );
        inputActions.Keyboard.R2.performed += R2 => Key( KEY.R2 ); rawInputs.Add( KEY.R2 , false );
        inputActions.Keyboard.R3.performed += R3 => Key( KEY.R3 ); rawInputs.Add( KEY.R3 , false );

        inputActions.Keyboard.North.performed += R3 => Key( KEY.NORTH ); rawInputs.Add( KEY.NORTH , false );
        inputActions.Keyboard.South.performed += R3 => Key( KEY.SOUTH ); rawInputs.Add( KEY.SOUTH , false );
        inputActions.Keyboard.East.performed += R3 => Key( KEY.EAST ); rawInputs.Add( KEY.EAST , false );
        inputActions.Keyboard.West.performed += R3 => Key( KEY.WEST ); rawInputs.Add( KEY.WEST , false );

        inputActions.Keyboard.PadDown.performed += R3 => Key( KEY.PADDOWN ); rawInputs.Add( KEY.PADDOWN , false );
        inputActions.Keyboard.PadUp.performed += R3 => Key( KEY.PADUP ); rawInputs.Add( KEY.PADUP , false );
        inputActions.Keyboard.PadLeft.performed += R3 => Key( KEY.PADLEFT ); rawInputs.Add( KEY.PADLEFT , false );
        inputActions.Keyboard.PadRight.performed += R3 => Key( KEY.PADRIGHT ); rawInputs.Add( KEY.PADRIGHT , false );

        inputActions.Keyboard.RightJoystick.performed += lj => Key( KEY.RIGHTJOYSTICK , lj.ReadValue<Vector2>() , true ); rawInputs.Add( KEY.RIGHTJOYSTICK , Vector2.zero );
        inputActions.Keyboard.LeftJoystick.performed += rj => Key( KEY.LEFTJOYSTICK , rj.ReadValue<Vector2>() , true ); rawInputs.Add( KEY.LEFTJOYSTICK , Vector2.zero );

        inputActions.Enable();
    }

    void SetupGameInputs ()
    {
        gameinputs = new List<InputElement>();

        profile.Setup();

        foreach (Input_Profile.InputElement ie in profile.inputs)
        {
            if (!rawInputs.ContainsKey( ie.key )) { continue; }

            gameinputs.Add( new KeyValuePair<string , bool>( ie.name, false ) );
        }

        /*
        foreach (System.Reflection.FieldInfo field in typeof( Input_Profile ).GetFields())
        {
            if (field.FieldType != typeof( string )) { continue; }

            string str = (string)field.GetValue(profile);

            if (!rawInputs.ContainsKey( str )) { continue; }

            gameinputs.Add( new KeyValuePair<string , bool>( field.Name , false ) );
        }*/
    }

    #endregion

    #region Update
    private void Update ()
    {
        UpdateGameInputs();
    }
    void UpdateGameInputs ()
    {
        foreach (Input_Profile.InputElement ip in profile.inputs)
        {
            InputElement ie = InputElement.GetInList(ref gameinputs, ip.name);
            Vector2 v = Vector2.zero; bool b = false; int t = 0;
            foreach (KEY k in ip.keys)
            {
                if (!rawInputs.ContainsKey( k )) { continue; }

                v = v.magnitude < rawInputs[k].vectorValue.magnitude ? rawInputs[k].vectorValue : v;
                b = b ? true : rawInputs[k].boolValue;
                t = t < rawInputs[k].tapCount ? rawInputs[k].tapCount : t;

                if (debug) { print( ie.name + "    binded with :    " + k + "    state :    " + ie.boolValue ); }
            }
            ie.Set( b);
            ie.Set( v);
            ie.tapCount = t;
        }

        /*
        foreach (System.Reflection.FieldInfo field in typeof( Input_Profile ).GetFields())
        {
            if (field.FieldType != typeof( string )) { continue; }

            string str = (string)field.GetValue(profile);

            if (!rawInputs.ContainsKey( str )) { continue; }

            InputElement ie = InputElement.GetInList(ref gameinputs, field.Name);
            ie.Set( rawInputs[str].boolValue );
            ie.Set( rawInputs[str].vectorValue );

            ie.tapCount = rawInputs[str].tapCount;

            if (debug) { print( ie.name + "    binded with :    " + str + "    state :    " + ie.boolValue); }
        }*/
    }

    #endregion

    #region Methods

    void Key ( KEY _name , bool disableCoroutine = false )
    {
        rawInputs[_name].Set(!rawInputs[_name],this);
        //if (debug) { print( "Toggled : " + _name + " : " + rawInputs[_name].boolValue ); }
        if (disableCoroutine) { StartCoroutine( DisableCoroutine( rawInputs[_name] ) ); }
    }
    void Key ( KEY _name , Vector2 dir, bool disableCoroutine = false )
    {
        rawInputs[_name].Set(dir);
        //if (debug) { print( "Set : " + name + " : " + rawInputs[name].vectorValue ); }
        if (disableCoroutine) { StartCoroutine( DisableCoroutine( rawInputs[_name] ) ); }
    }
    void Key ( KEY _name , bool b , bool disableCoroutine = false )
    {
        rawInputs[_name].Set(b,this);
        //if (debug) { print( "Set : " + name + " : " + rawInputs[name].boolValue ); }
        if (disableCoroutine) { StartCoroutine( DisableCoroutine( rawInputs[_name] ) ); }
    }

    public bool Get ( string _key )
    {
        foreach (InputElement ie in gameinputs)
        {
            if (ie.name == _key) { return ie.boolValue; }
        }

        return false;
    }
    public Vector2 GetV( string _key )
    {
        foreach (InputElement ie in gameinputs)
        {
            if (ie.name == _key) { return ie.vectorValue; }
        }

        return Vector2.zero;
    }
    
    public InputStruct GetStruct(string _key )
    {
        foreach (InputElement ie in gameinputs)
        {
            if (ie.name == _key) { return ie; }
        }
        return InputStruct.Null;
    }

    public int GetCount( string _key )
    {
        foreach (InputElement ie in gameinputs)
        {
            if (ie.name == _key) { return ie.tapCount; }
        }

        return 0;
    }

    public InputElement GetRaw ( string _key )
    {
        foreach (InputElement ie in gameinputs)
        {
            if (ie.name == _key) { return ie; }
        }

        return null;
    }

    IEnumerator DisableCoroutine(InputElement ie )
    {
        yield return new WaitForEndOfFrame();
        ie.boolValue = false;
        ie.vectorValue = Vector2.zero;
    }

    string RecoverInputName ( string _input )
    {
        string name = "";

        for (int i = 0 ; i < _input.Length ; i++)
        {
            if (_input[i] == '/')
            {
                i++;
                while (_input[i] != '[')
                {
                    name += _input[i];
                    i++;
                }
                break;
            }
        }

        return name;
    }

    #endregion

    public IEnumerator TapCoroutine (InputElement ie, float tapInterval = 0)
    {
        tapInterval = tapInterval == 0 ? profile.tapInterval : tapInterval;
        ie.taps.Add( true );//print( ie.name );
        yield return new WaitForSeconds( tapInterval );
        ie.taps.RemoveAt( 0 );
    }

    public struct InputStruct
    {
        public bool boolValue;
        public Vector2 vectorValue;
        public int tapCount;

        public InputStruct(bool b, Vector2 v, int t ) { boolValue = b; vectorValue = v; tapCount = t; }

        public static  InputStruct Null { get { return new InputStruct( false , Vector2.zero , 0 ); } }

        public static implicit operator InputStruct (InputElement ie )
        {
            return new InputStruct( ie.boolValue , ie.vectorValue , ie.tapCount );
        }

        public static implicit operator bool ( InputStruct i )
        {
            return i.boolValue;
        }

        public static implicit operator Vector2 ( InputStruct i )
        {
            return i.vectorValue;
        }

        public static implicit operator int ( InputStruct i )
        {
            return i.tapCount;
        }
    }

    [System.Serializable]
    public class InputElement
    {
        public string name;

        public List<string> names = new List<string>();

        public bool boolValue;

        public Vector2 vectorValue;

        [SerializeField]
        int _tapCount;

        public int tapCount { get {_tapCount = taps.Count; return _tapCount; } set { _tapCount = value; } }
        [HideInInspector]
        public List<bool> taps = new List<bool>();

        public void Set(bool b,InputManager caster = null) { boolValue = b; if (caster != null) { caster.StartCoroutine( caster.TapCoroutine( this ) ); } }
        public void Set( Vector2 v ) { vectorValue = v; }

        public InputElement(string _name,bool _bool ) { name = _name; boolValue = _bool; }
        public InputElement(bool _bool ) {  boolValue = _bool; }
        public InputElement ( string _name, Vector2 _v ) { name = _name; vectorValue = _v; }
        public InputElement( Vector2 _v ) { vectorValue = _v; }

        public static implicit operator bool (InputElement ie ) { return ie.boolValue; }
        public static implicit operator Vector2 (InputElement ie ) { return ie.vectorValue; }

        public static implicit operator InputElement ( KeyValuePair<string , bool> kp ) { return new InputElement( kp.Key ,kp.Value); }
        public static implicit operator InputElement ( KeyValuePair<string , Vector2> kp ) { return new InputElement( kp.Key ,kp.Value); }
        
        public static implicit operator InputElement ( bool b ) { return new InputElement( b ); }
        public static implicit operator InputElement ( Vector2 v ) { return new InputElement( v ); }

        public static InputElement GetInList(ref List<InputElement> _list, string _name )
        {
            foreach(InputElement ie in _list)
            {
                if(ie.name == _name) { return ie; }
            }

            return null;
        }
    }

    public static InputManager instance;
}