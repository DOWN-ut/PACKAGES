using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Text.RegularExpressions;

[ExecuteInEditMode]
public class MultiLanguages : MonoBehaviour {

    [Header("Main")]

    [Tooltip("0 = english ; 1 = french")]
    public int language = 0;

    public string[] texts;
    public char returnChar = '¤';

    [Header("Ref")]

    public TextMesh textDisplayer;
    public Text textDisplayer0;

    [Header("Actualize")]

    public bool actualize;

    // Update is called once per frame
    void Update()
    {

        language = PlayerPrefs.GetInt("language");

        if ((actualize || Application.isPlaying) && language < texts.Length)
        {
            if (textDisplayer == null && GetComponent<TextMesh>() != null)
            {
                textDisplayer = GetComponent<TextMesh>();
            }
            if (textDisplayer0 == null && GetComponent<Text>() != null)
            {
                textDisplayer0 = GetComponent<Text>();
            }

            if (textDisplayer != null)
            {
                textDisplayer.text = "";

                string[] text = texts[language].Split(returnChar);

                int i = 0;
                while (i < text.Length)
                {
                    if (i != 0)
                    {
                        textDisplayer.text += '\n' + text[i];
                    }
                    else
                    {
                        textDisplayer.text += text[i];
                    }
                    i++;
                }
            }

            if (textDisplayer0 != null)
            {
                textDisplayer0.text = "";

                string[] text = texts[language].Split(returnChar);

                int i = 0;
                while (i < text.Length)
                {
                    if (i != 0)
                    {
                        textDisplayer0.text += '\n' + text[i];
                    }
                    else
                    {
                        textDisplayer0.text += text[i];
                    }
                    i++;
                }
            }

            actualize = false;
        }
    }
}
