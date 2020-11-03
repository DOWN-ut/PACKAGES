using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu( fileName = "Color_Palette" , menuName = "Miscelaneous/Color Palette" , order = 10 )]
public class ColorPalette : ScriptableObject
{
    public string name;

    public List<Couleur> colors = new List<Couleur>();

    [HideInInspector]
    public int variation;
    [HideInInspector]
    public int order;
}