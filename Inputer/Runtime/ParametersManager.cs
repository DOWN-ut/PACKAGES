using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametersManager : MonoBehaviour
{
    [Header("Controls HUD")]

    public RectTransform controlsZone;

    public RectTransform controlsParent;

    public RectTransform mainZone;

    [Header("Ingame")]

    public bool opened;

    public bool controlsWindow {
        get {
            return controlsZone.gameObject.activeSelf;
        }
        set {
            if (value) { mainZone.gameObject.SetActive( false ); }
            controlsZone.gameObject.SetActive( value );
        } }

    bool echapPressed;

    [Header("References")]

    public RectTransform mainButton;

    public InputManager inputManager;

    private void Awake ()
    {
        DontDestroyOnLoad( gameObject );

        inputManager = FindObjectOfType<InputManager>();

        Toggle( false );
    }

    private void Update ()
    {
        Actions();
    }

    void Actions ()
    {
        if (inputManager.rawInputs[InputManager.KEY.ECHAP]) { if (!echapPressed) { EchapPressed(); echapPressed = true; } }
        else { echapPressed = false; }
    }

    public void ControlClicked ()
    {
        controlsWindow = true ;
    }

    public void OpenClicked ()
    {
        EchapPressed(true);
    }

    void EchapPressed (bool isOpenButton = false)
    {
        if (!opened || isOpenButton)
        {
            Toggle( true );
            return;
        }

        Retour();
    }

    void Retour ()
    {
        if( controlsWindow) { controlsWindow = false; return; }

        Toggle( false );
    }

    void Toggle(bool open )
    {
        if (!opened)
        {
            mainZone.gameObject.SetActive( true );
        }

        opened = open; transform.GetChild( 0 ).gameObject.SetActive( open );
    }
}
