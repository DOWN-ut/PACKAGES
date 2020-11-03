using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header( "Parameters" )]

    public bool debug;

    public Input_Profile profile;

    [Header( "Ingame" )]

    public Dictionary<string, InputElement> rawInputs;

    public List<InputElement> gameinputs;

    public Vector2 movement {
        get
        {
            return new Vector2( Get( "forward" ) ? 1 : Get( "backward" ) ? -1 : 0 , Get( "rightward" ) ? -1 : Get( "leftward" ) ? 1 : 0 );
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
        DontDestroyOnLoad( gameObject );

        SetupInputer(); SetupGameInputs();
    }

    #endregion

    #region Setup

    void SetupInputer ()
    {
        inputActions = new INPUT();

        rawInputs = new Dictionary<string , InputElement>();

        inputActions.Keyboard.a.performed += a => Key( "a" ); rawInputs.Add( "a" , false );
        inputActions.Keyboard.b.performed += b => Key( "b" ); rawInputs.Add( "b" , false );
        inputActions.Keyboard.c.performed += c => Key( "c" ); rawInputs.Add( "c" , false );
        inputActions.Keyboard.d.performed += d => Key( "d" ); rawInputs.Add( "d" , false );
        inputActions.Keyboard.e.performed += e => Key( "e" ); rawInputs.Add( "e" , false );
        inputActions.Keyboard.f.performed += f => Key( "f" ); rawInputs.Add( "f" , false );
        inputActions.Keyboard.g.performed += g => Key( "g" ); rawInputs.Add( "g" , false );
        inputActions.Keyboard.h.performed += h => Key( "h" ); rawInputs.Add( "h" , false );
        inputActions.Keyboard.i.performed += i => Key( "i" ); rawInputs.Add( "i" , false );
        inputActions.Keyboard.j.performed += j => Key( "j" ); rawInputs.Add( "j" , false );
        inputActions.Keyboard.k.performed += k => Key( "k" ); rawInputs.Add( "k" , false );
        inputActions.Keyboard.l.performed += l => Key( "l" ); rawInputs.Add( "l" , false );
        inputActions.Keyboard.m.performed += m => Key( "m" ); rawInputs.Add( "m" , false );
        inputActions.Keyboard.n.performed += n => Key( "n" ); rawInputs.Add( "n" , false );
        inputActions.Keyboard.o.performed += o => Key( "o" ); rawInputs.Add( "o" , false );
        inputActions.Keyboard.p.performed += p => Key( "p" ); rawInputs.Add( "p" , false );
        inputActions.Keyboard.q.performed += q => Key( "q" ); rawInputs.Add( "q" , false );
        inputActions.Keyboard.r.performed += r => Key( "r" ); rawInputs.Add( "r" , false );
        inputActions.Keyboard.s.performed += s => Key( "s" ); rawInputs.Add( "s" , false );
        inputActions.Keyboard.t.performed += t => Key( "t" ); rawInputs.Add( "t" , false );
        inputActions.Keyboard.u.performed += u => Key( "u" ); rawInputs.Add( "u" , false );
        inputActions.Keyboard.v.performed += v => Key( "v" ); rawInputs.Add( "v" , false );
        inputActions.Keyboard.w.performed += w => Key( "w" ); rawInputs.Add( "w" , false );
        inputActions.Keyboard.x.performed += x => Key( "x" ); rawInputs.Add( "x" , false );
        inputActions.Keyboard.y.performed += y => Key( "y" ); rawInputs.Add( "y" , false );
        inputActions.Keyboard.z.performed += z => Key( "z" ); rawInputs.Add( "z" , false );

        inputActions.Keyboard._1.performed += _1 => Key( "1" ); rawInputs.Add( "1" , false );
        inputActions.Keyboard._2.performed += _2 => Key( "2" ); rawInputs.Add( "2" , false );
        inputActions.Keyboard._3.performed += _3 => Key( "3" ); rawInputs.Add( "3" , false );
        inputActions.Keyboard._4.performed += _4 => Key( "4" ); rawInputs.Add( "4" , false );
        inputActions.Keyboard._5.performed += _5 => Key( "5" ); rawInputs.Add( "5" , false );
        inputActions.Keyboard._6.performed += _6 => Key( "6" ); rawInputs.Add( "6" , false );
        inputActions.Keyboard._7.performed += _7 => Key( "7" ); rawInputs.Add( "7" , false );
        inputActions.Keyboard._8.performed += _8 => Key( "8" ); rawInputs.Add( "8" , false );
        inputActions.Keyboard._9.performed += _9 => Key( "9" ); rawInputs.Add( "9" , false );
        inputActions.Keyboard._0.performed += _0 => Key( "0" ); rawInputs.Add( "0" , false );

        inputActions.Keyboard.left_ctrl.performed += left_ctrl => Key( "left_ctrl" ); rawInputs.Add( "left_ctrl" , false );
        inputActions.Keyboard.right_ctrl.performed += right_ctrl => Key( "right_ctrl" ); rawInputs.Add( "right_ctrl" , false );
        inputActions.Keyboard.left_shift.performed += left_shift => Key( "left_shift" ); rawInputs.Add( "left_shift" , false );
        inputActions.Keyboard.right_shift.performed += right_shift => Key( "right_shift" ); rawInputs.Add( "right_shift" , false );
        inputActions.Keyboard.tab.performed += tab => Key( "tab" ); rawInputs.Add( "tab" , false );
        inputActions.Keyboard.space.performed += space => Key( "space" ); rawInputs.Add( "space" , false );
        inputActions.Keyboard.enter.performed += enter => Key( "enter" ); rawInputs.Add( "enter" , false );
        inputActions.Keyboard.echap.performed += echap => Key( "echap" ); rawInputs.Add( "echap" , false );


        inputActions.Keyboard.mouse_left.performed += mouse_left => Key( "mouse_left" ); rawInputs.Add( "mouse_left" , false );
        inputActions.Keyboard.mouse_right.performed += mouse_right => Key( "mouse_right" ); rawInputs.Add( "mouse_right" , false );
        inputActions.Keyboard.mouse_middle.performed += mouse_left => Key( "mouse_middle" ); rawInputs.Add( "mouse_middle" , false );

        inputActions.Keyboard.mouse.performed += mouse => Key( "mouse" , mouse.ReadValue<Vector2>(),true ); rawInputs.Add( "mouse" , Vector2.zero );

        inputActions.Keyboard.wheel.performed += wheel => Key("wheel", wheel.ReadValue<Vector2>(),true ); rawInputs.Add( "wheel" , Vector2.zero );
        inputActions.Keyboard.wheel.performed += wheel => Key("wheel_up", wheel.ReadValue<Vector2>().y > 0 , true ); rawInputs.Add( "wheel_up" , Vector2.zero );
        inputActions.Keyboard.wheel.performed += wheel => Key("wheel_down", wheel.ReadValue<Vector2>().y < 0 , true ); rawInputs.Add( "wheel_down" , Vector2.zero );

        inputActions.Enable();
    }

    void SetupGameInputs ()
    {
        gameinputs = new List<InputElement>();

        profile.Setup();

        foreach (System.Reflection.FieldInfo field in typeof( Input_Profile ).GetFields())
        {
            if (field.FieldType != typeof( string )) { continue; }

            string str = (string)field.GetValue(profile);

            if (!rawInputs.ContainsKey( str )) { continue; }

            gameinputs.Add( new KeyValuePair<string , bool>( field.Name , false ) );
        }
    }

    #endregion

    #region Update
    private void Update ()
    {
        UpdateGameInputs();
    }
    void UpdateGameInputs ()
    {
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
        }
    }

    #endregion

    #region Methods

    void Key ( string _name , bool disableCoroutine = false )
    {
        rawInputs[_name].Set(!rawInputs[_name],this);
        //if (debug) { print( "Toggled : " + _name + " : " + rawInputs[_name].boolValue ); }
        if (disableCoroutine) { StartCoroutine( DisableCoroutine( rawInputs[name] ) ); }
    }
    void Key (string name, Vector2 dir, bool disableCoroutine = false )
    {
        rawInputs[name].Set(dir);
        //if (debug) { print( "Set : " + name + " : " + rawInputs[name].vectorValue ); }
        if (disableCoroutine) { StartCoroutine( DisableCoroutine( rawInputs[name] ) ); }
    }
    void Key ( string name , bool b , bool disableCoroutine = false )
    {
        rawInputs[name].Set(b,this);
        //if (debug) { print( "Set : " + name + " : " + rawInputs[name].boolValue ); }
        if (disableCoroutine) { StartCoroutine( DisableCoroutine( rawInputs[name] ) ); }
    }

    public bool Get ( string _key )
    {
        foreach (InputElement ie in gameinputs)
        {
            if (ie.name == _key) { return ie.boolValue; }
        }

        return false;
    }
    public Vector2 GetV(string _key )
    {
        foreach (InputElement ie in gameinputs)
        {
            if (ie.name == _key) { return ie.vectorValue; }
        }

        return Vector2.zero;
    }

    public int GetCount(string _key )
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

    public IEnumerator TapCoroutine (InputElement ie, float tapInterval = 0.1f)
    {
        ie.taps.Add( true );print( ie.name );
        yield return new WaitForSeconds( tapInterval );
        ie.taps.RemoveAt( 0 );
    }

    [System.Serializable]
    public class InputElement
    {
        public string name;

        public string[] names = new string[10];

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
}