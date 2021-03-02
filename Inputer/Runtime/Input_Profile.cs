using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Profile : MonoBehaviour
{
    public string layout = "qwerty";
    [Space(10)]
    public List<InputElement> inputs = new List<InputElement>();

    [Space(5)]

    public LookProfile.LookProfileEnum _lookProfile;
    LookProfile lookProfile { get { return new LookProfile( _lookProfile , lookSensibility ); } }
    public float lookSensibility =1f;

    public Vector2 Look ( Vector2 _look )
    {
        return _look.normalized * Mathf.Pow( _look.magnitude + lookProfile.threshold , lookProfile.curve ) * lookProfile.sensivity;
    }

    public void Setup ()
    {
        ChangeLayout( "qwerty" );
    }

    public void StartEdition ()
    {
        ChangeLayout( Application.systemLanguage == SystemLanguage.French ? "azerty" : "qwerty" );
    }

    public void EndEdition ()
    {
        ChangeLayout( "qwerty" );
    }

    public void ChangeLayout ( string _layout )
    {
        if (layout == _layout) { return; }
        layout = _layout;

        foreach (System.Reflection.FieldInfo field in typeof( Input_Profile ).GetFields())
        {
            if (field.FieldType == typeof( string ))
            {
                field.SetValue( this , Calculations.QwertyAzerty( (string)field.GetValue( this ) ) );
            }
        }
    }

    public InputElement GetInput ( string _name )
    {
        int id = -1;

        if(InputElement.Contains(inputs.ToArray(),_name,out id ))
        {
            return inputs[id];
        }
        return null;
    }
    public struct LookProfile
    {
        public float sensivity;
        public float curve;
        public float threshold;

        public static LookProfile _BASE { get { return new LookProfile( 2 , 1 , 1 ); } }
        public static LookProfile _SQUARED { get { return new LookProfile( .1f , 2 , 1 ); } }

        public LookProfile ( float _s , float _c , float _t ) { sensivity = _s; curve = _c; threshold = _t; }

        public LookProfile ( LookProfileEnum profile , float coef = 1 )
        {
            LookProfile p;
            switch (profile)
            {
                default: p = _BASE; break;
                case LookProfileEnum.SQUARED: p = _SQUARED; break;
            }
            sensivity = p.sensivity * coef;
            curve = p.curve;
            threshold = p.threshold;
        }

        public enum LookProfileEnum
        {
            BASE, SQUARED
        }
    }

    [System.Serializable]
    public class InputElement
    {
        public string name;
        public InputManager.KEY key { get { return keys[0]; } }
        public InputManager.KEY[] keys = new InputManager.KEY[1];

        public static bool Contains(InputElement[] ies, string _name, out int id )
        {
            id = 0;
            foreach (InputElement ie in ies)
            {
                if (ie.name == _name) { return true; }
                id++;
            }
            id = -1;
            return false;
        }
        public static bool Contains(InputElement[] ies, string _name )
        {
            int id;
            return Contains( ies , _name , out id );
        }
    }
}
