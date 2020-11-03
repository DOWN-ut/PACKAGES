using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Profile : MonoBehaviour
{
    public string layout = "qwerty";
    [Space(10)]
    public string forward;
    public string backward;
    public string rightward;
    public string leftward;
    [Space(5)]
    public string jump;
    public string sneak;
    [Space(5)]
    public string shoot;
    public string aim;
    [Space(5)]
    public string ability1;
    public string ability2;
    public string ability3;
    public string ability4;
    [Space(5)]
    public string melee;
    [Space(5)]
    public string next;
    public string previous;
    [Space(5)]
    public string pin;
    public string inventory;
    public string map;
    [Space(5)]
    public Vector2 lookSensibility = new Vector2(0.1f,2);
    public string look = "mouse";

    public Vector2 Look(Vector2 _look )
    {
        return _look.normalized * Mathf.Pow( _look.magnitude , lookSensibility.y ) * lookSensibility.x;
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
        ChangeLayout("qwerty" );
    }

    public void ChangeLayout(string _layout )
    {
        layout = _layout;

        foreach (System.Reflection.FieldInfo field in typeof( Input_Profile ).GetFields())
        {
            if (field.FieldType  == typeof( string ))
            {
                field.SetValue( this , Calculations.QwertyAzerty( (string)field.GetValue( this ) , layout == "azerty" ) );
            }
        }
    }
}
