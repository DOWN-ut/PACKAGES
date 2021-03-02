using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


[System.Serializable]
public struct IColor
{
    public Color color;
    public float intensity;

    public int priority;

    public static implicit operator IColor ( Color a )
    {
        return new IColor( a , 1 );
    }
    public static implicit operator Color ( IColor a )
    {
        return a.color;
    }

    public static Color none { get { return new Color( 0 , 0 , 0 , 0 ); } }

    public static IColor Blend ( IColor[] colors )
    {
        int p = 0; IColor r = colors[0];
        foreach (IColor c in colors)
        {
            if (c.priority > p)
            {
                r = c;
                p = c.priority;
            }
        }

        return r;
    }

    public IColor ( Color colo , float intens , int _priority = 0 )
    {
        color = colo;
        intensity = intens;
        priority = _priority;
    }
    public IColor ( float r , float g , float b , float a , float intens , int _priority = 0 )
    {
        color = new Color( r , g , b , a );
        intensity = intens;
        priority = _priority;
    }

    public IColor(float r, float g,float b ) { color = new Color( r , g , b ); intensity = 1; priority = 0; }

    public static IColor Grey { get { return new IColor( 84  / 256f, 84  / 256f, 84  / 256f); } }
    public static IColor GreySilver { get { return new IColor( 192  / 256f, 192  / 256f, 192  / 256f); } }
    public static IColor grey { get { return new IColor( 190  / 256f, 190  / 256f, 190  / 256f); } }
    public static IColor LightGray { get { return new IColor( 211  / 256f, 211  / 256f, 211  / 256f); } }
    public static IColor LightSlateGrey { get { return new IColor( 119  / 256f, 136  / 256f, 153  / 256f); } }
    public static IColor SlateGray { get { return new IColor( 112  / 256f, 128  / 256f, 144  / 256f); } }
    public static IColor SlateGray1 { get { return new IColor( 198  / 256f, 226  / 256f, 255  / 256f); } }
    public static IColor SlateGray2 { get { return new IColor( 185  / 256f, 211  / 256f, 238  / 256f); } }
    public static IColor SlateGray3 { get { return new IColor( 159  / 256f, 182  / 256f, 205  / 256f); } }
    public static IColor SlateGray4 { get { return new IColor( 108  / 256f, 123  / 256f, 139  / 256f); } }
    public static IColor black { get { return new IColor( 0  / 256f, 0  / 256f, 0  / 256f); } }
    public static IColor grey0 { get { return new IColor( 0  / 256f, 0  / 256f, 0  / 256f); } }
    public static IColor grey1 { get { return new IColor( 3  / 256f, 3  / 256f, 3  / 256f); } }
    public static IColor grey2 { get { return new IColor( 5  / 256f, 5  / 256f, 5  / 256f); } }
    public static IColor grey3 { get { return new IColor( 8  / 256f, 8  / 256f, 8  / 256f); } }
    public static IColor grey4 { get { return new IColor( 10  / 256f, 10  / 256f, 10  / 256f); } }
    public static IColor grey5 { get { return new IColor( 13  / 256f, 13  / 256f, 13  / 256f); } }
    public static IColor grey6 { get { return new IColor( 15  / 256f, 15  / 256f, 15  / 256f); } }
    public static IColor grey7 { get { return new IColor( 18  / 256f, 18  / 256f, 18  / 256f); } }
    public static IColor grey8 { get { return new IColor( 20  / 256f, 20  / 256f, 20  / 256f); } }
    public static IColor grey9 { get { return new IColor( 23  / 256f, 23  / 256f, 23  / 256f); } }
    public static IColor grey10 { get { return new IColor( 26  / 256f, 26  / 256f, 26  / 256f); } }
    public static IColor grey11 { get { return new IColor( 28  / 256f, 28  / 256f, 28  / 256f); } }
    public static IColor grey12 { get { return new IColor( 31  / 256f, 31  / 256f, 31  / 256f); } }
    public static IColor grey13 { get { return new IColor( 33  / 256f, 33  / 256f, 33  / 256f); } }
public static IColor grey14 { get { return new IColor( 36  / 256f, 36  / 256f, 36  / 256f); } }
public static IColor grey15 { get { return new IColor( 38  / 256f, 38  / 256f, 38  / 256f); } }
public static IColor grey16 { get { return new IColor( 41  / 256f, 41  / 256f, 41  / 256f); } }
public static IColor grey17 { get { return new IColor( 43  / 256f, 43  / 256f, 43  / 256f); } }
public static IColor grey18 { get { return new IColor( 46  / 256f, 46  / 256f, 46  / 256f); } }
public static IColor grey19 { get { return new IColor( 48  / 256f, 48  / 256f, 48  / 256f); } }
public static IColor grey20 { get { return new IColor( 51  / 256f, 51  / 256f, 51  / 256f); } }
public static IColor grey21 { get { return new IColor( 54  / 256f, 54  / 256f, 54  / 256f); } }
public static IColor grey22 { get { return new IColor( 56  / 256f, 56  / 256f, 56  / 256f); } }
public static IColor grey23 { get { return new IColor( 59  / 256f, 59  / 256f, 59  / 256f); } }
public static IColor grey24 { get { return new IColor( 61  / 256f, 61  / 256f, 61  / 256f); } }
public static IColor grey25 { get { return new IColor( 64  / 256f, 64  / 256f, 64  / 256f); } }
public static IColor grey26 { get { return new IColor( 66  / 256f, 66  / 256f, 66  / 256f); } }
public static IColor grey27 { get { return new IColor( 69  / 256f, 69  / 256f, 69  / 256f); } }
public static IColor grey28 { get { return new IColor( 71  / 256f, 71  / 256f, 71  / 256f); } }
public static IColor grey29 { get { return new IColor( 74  / 256f, 74  / 256f, 74  / 256f); } }
public static IColor grey30 { get { return new IColor( 77  / 256f, 77  / 256f, 77  / 256f); } }
public static IColor grey31 { get { return new IColor( 79  / 256f, 79  / 256f, 79  / 256f); } }
public static IColor grey32 { get { return new IColor( 82  / 256f, 82  / 256f, 82  / 256f); } }
public static IColor grey33 { get { return new IColor( 84  / 256f, 84  / 256f, 84  / 256f); } }
public static IColor grey34 { get { return new IColor( 87  / 256f, 87  / 256f, 87  / 256f); } }
public static IColor grey35 { get { return new IColor( 89  / 256f, 89  / 256f, 89  / 256f); } }
public static IColor grey36 { get { return new IColor( 92  / 256f, 92  / 256f, 92  / 256f); } }
public static IColor grey37 { get { return new IColor( 94  / 256f, 94  / 256f, 94  / 256f); } }
public static IColor grey38 { get { return new IColor( 97  / 256f, 97  / 256f, 97  / 256f); } }
public static IColor grey39 { get { return new IColor( 99  / 256f, 99  / 256f, 99  / 256f); } }
public static IColor grey40 { get { return new IColor( 102  / 256f, 102  / 256f, 102  / 256f); } }
public static IColor grey41DimGrey { get { return new IColor( 105  / 256f, 105  / 256f, 105  / 256f); } }
public static IColor grey42 { get { return new IColor( 107  / 256f, 107  / 256f, 107  / 256f); } }
public static IColor grey43 { get { return new IColor( 110  / 256f, 110  / 256f, 110  / 256f); } }
public static IColor grey44 { get { return new IColor( 112  / 256f, 112  / 256f, 112  / 256f); } }
public static IColor grey45 { get { return new IColor( 115  / 256f, 115  / 256f, 115  / 256f); } }
public static IColor grey46 { get { return new IColor( 117  / 256f, 117  / 256f, 117  / 256f); } }
public static IColor grey47 { get { return new IColor( 120  / 256f, 120  / 256f, 120  / 256f); } }
public static IColor grey48 { get { return new IColor( 122  / 256f, 122  / 256f, 122  / 256f); } }
public static IColor grey49 { get { return new IColor( 125  / 256f, 125  / 256f, 125  / 256f); } }
public static IColor grey50 { get { return new IColor( 127  / 256f, 127  / 256f, 127  / 256f); } }
public static IColor grey51 { get { return new IColor( 130  / 256f, 130  / 256f, 130  / 256f); } }
public static IColor grey52 { get { return new IColor( 133  / 256f, 133  / 256f, 133  / 256f); } }
public static IColor grey53 { get { return new IColor( 135  / 256f, 135  / 256f, 135  / 256f); } }
public static IColor grey54 { get { return new IColor( 138  / 256f, 138  / 256f, 138  / 256f); } }
public static IColor grey55 { get { return new IColor( 140  / 256f, 140  / 256f, 140  / 256f); } }
public static IColor grey56 { get { return new IColor( 143  / 256f, 143  / 256f, 143  / 256f); } }
public static IColor grey57 { get { return new IColor( 145  / 256f, 145  / 256f, 145  / 256f); } }
public static IColor grey58 { get { return new IColor( 148  / 256f, 148  / 256f, 148  / 256f); } }
public static IColor grey59 { get { return new IColor( 150  / 256f, 150  / 256f, 150  / 256f); } }
public static IColor grey60 { get { return new IColor( 153  / 256f, 153  / 256f, 153  / 256f); } }
public static IColor grey61 { get { return new IColor( 156  / 256f, 156  / 256f, 156  / 256f); } }
public static IColor grey62 { get { return new IColor( 158  / 256f, 158  / 256f, 158  / 256f); } }
public static IColor grey63 { get { return new IColor( 161  / 256f, 161  / 256f, 161  / 256f); } }
public static IColor grey64 { get { return new IColor( 163  / 256f, 163  / 256f, 163  / 256f); } }
public static IColor grey65 { get { return new IColor( 166  / 256f, 166  / 256f, 166  / 256f); } }
public static IColor grey66 { get { return new IColor( 168  / 256f, 168  / 256f, 168  / 256f); } }
public static IColor grey67 { get { return new IColor( 171  / 256f, 171  / 256f, 171  / 256f); } }
public static IColor grey68 { get { return new IColor( 173  / 256f, 173  / 256f, 173  / 256f); } }
public static IColor grey69 { get { return new IColor( 176  / 256f, 176  / 256f, 176  / 256f); } }
public static IColor grey70 { get { return new IColor( 179  / 256f, 179  / 256f, 179  / 256f); } }
public static IColor grey71 { get { return new IColor( 181  / 256f, 181  / 256f, 181  / 256f); } }
public static IColor grey72 { get { return new IColor( 184  / 256f, 184  / 256f, 184  / 256f); } }
    public static IColor grey73 { get { return new IColor( 186  / 256f, 186  / 256f, 186  / 256f); } }
public static IColor grey74 { get { return new IColor( 189  / 256f, 189  / 256f, 189  / 256f); } }
public static IColor grey75 { get { return new IColor( 191  / 256f, 191  / 256f, 191  / 256f); } }
public static IColor grey76 { get { return new IColor( 194  / 256f, 194  / 256f, 194  / 256f); } }
public static IColor grey77 { get { return new IColor( 196  / 256f, 196  / 256f, 196  / 256f); } }
public static IColor grey78 { get { return new IColor( 199  / 256f, 199  / 256f, 199  / 256f); } }
public static IColor grey79 { get { return new IColor( 201  / 256f, 201  / 256f, 201  / 256f); } }
public static IColor grey80 { get { return new IColor( 204  / 256f, 204  / 256f, 204  / 256f); } }
public static IColor grey81 { get { return new IColor( 207  / 256f, 207  / 256f, 207  / 256f); } }
public static IColor grey82 { get { return new IColor( 209  / 256f, 209  / 256f, 209  / 256f); } }
public static IColor grey83 { get { return new IColor( 212  / 256f, 212  / 256f, 212  / 256f); } }
public static IColor grey84 { get { return new IColor( 214  / 256f, 214  / 256f, 214  / 256f); } }
public static IColor grey85 { get { return new IColor( 217  / 256f, 217  / 256f, 217  / 256f); } }
public static IColor grey86 { get { return new IColor( 219  / 256f, 219  / 256f, 219  / 256f); } }
public static IColor grey87 { get { return new IColor( 222  / 256f, 222  / 256f, 222  / 256f); } }

    public static IColor grey88 { get { return new IColor( 224  / 256f, 224  / 256f, 224  / 256f); } }
public static IColor grey89 { get { return new IColor( 227  / 256f, 227  / 256f, 227  / 256f); } }
public static IColor grey90 { get { return new IColor( 229  / 256f, 229  / 256f, 229  / 256f); } }
public static IColor grey91 { get { return new IColor( 232  / 256f, 232  / 256f, 232  / 256f); } }
public static IColor grey92 { get { return new IColor( 235  / 256f, 235  / 256f, 235  / 256f); } }
public static IColor grey93 { get { return new IColor( 237  / 256f, 237  / 256f, 237  / 256f); } }
public static IColor grey94
{ get { return new IColor( 240  / 256f, 240  / 256f, 240  / 256f); } }
public static IColor grey95 { get { return new IColor( 242  / 256f, 242  / 256f, 242  / 256f); } }
public static IColor grey96 { get { return new IColor( 245  / 256f, 245  / 256f, 245  / 256f); } }
public static IColor grey97 { get { return new IColor( 247  / 256f, 247  / 256f, 247  / 256f); } }
public static IColor grey98 { get { return new IColor( 250  / 256f, 250  / 256f, 250  / 256f); } }
public static IColor grey99 { get { return new IColor( 252  / 256f, 252  / 256f, 252  / 256f); } }
public static IColor grey100White { get { return new IColor( 255  / 256f, 255  / 256f, 255  / 256f); } }
public static IColor DarkSlateGrey { get { return new IColor( 47  / 256f, 79  / 256f, 79  / 256f); } }
public static IColor DimGrey { get { return new IColor( 84  / 256f, 84  / 256f, 84  / 256f); } }
public static IColor LightGrey { get { return new IColor( 219  / 256f, 219  / 256f, 112  / 256f); } }
public static IColor VeryLightGrey { get { return new IColor( 205  / 256f, 205  / 256f, 205  / 256f); } }
public static IColor FreeSpeechGrey { get { return new IColor( 99  / 256f, 86  / 256f, 136  / 256f); } }
public static IColor AliceBlue{ get { return new IColor( 240  / 256f, 248  / 256f, 255  / 256f); } }
public static IColor BlueViolet { get { return new IColor( 138  / 256f, 43  / 256f, 226  / 256f); } }
public static IColor CadetBlue { get { return new IColor( 95  / 256f, 159  / 256f, 159  / 256f); } }
public static IColor CadetBlue1 { get { return new IColor( 152  / 256f, 245  / 256f, 255  / 256f); } }
public static IColor CadetBlue2 { get { return new IColor( 142  / 256f, 229  / 256f, 238  / 256f); } }
public static IColor CadetBlue3 { get { return new IColor( 122  / 256f, 197  / 256f, 205  / 256f); } }
public static IColor CadetBlue4 { get { return new IColor( 83  / 256f, 134  / 256f, 139  / 256f); } }
public static IColor CornFlowerBlue { get { return new IColor( 66  / 256f, 66  / 256f, 111  / 256f); } }
public static IColor CornflowerBlue { get { return new IColor( 100  / 256f, 149  / 256f, 237  / 256f); } }
public static IColor DarkSlateBlue { get { return new IColor( 72  / 256f, 61  / 256f, 139  / 256f); } }
public static IColor DarkTurquoise { get { return new IColor( 0  / 256f, 206  / 256f, 209  / 256f); } }
public static IColor DeepSkyBlue { get { return new IColor( 0  / 256f, 191  / 256f, 255  / 256f); } }
public static IColor DeepSkyBlue1 { get { return new IColor( 0  / 256f, 191  / 256f, 255  / 256f); } }
public static IColor DeepSkyBlue2 { get { return new IColor( 0  / 256f, 178  / 256f, 238  / 256f); } }
public static IColor DeepSkyBlue3 { get { return new IColor( 0  / 256f, 154  / 256f, 205  / 256f); } }
public static IColor DeepSkyBlue4 { get { return new IColor( 0  / 256f, 104  / 256f, 139  / 256f); } }
public static IColor DodgerBlue { get { return new IColor( 30  / 256f, 144  / 256f, 255  / 256f); } }
public static IColor DodgerBlue1 { get { return new IColor( 30  / 256f, 144  / 256f, 255  / 256f); } }
public static IColor DodgerBlue2 { get { return new IColor( 28  / 256f, 134  / 256f, 238  / 256f); } }
public static IColor DodgerBlue3 { get { return new IColor( 24  / 256f, 116  / 256f, 205  / 256f); } }
public static IColor DodgerBlue4 { get { return new IColor( 16  / 256f, 78  / 256f, 139  / 256f); } }
public static IColor  LightBlue { get { return new IColor( 173  / 256f, 216  / 256f, 230  / 256f); } }
public static IColor LightBlue1 { get { return new IColor( 191  / 256f, 239  / 256f, 255  / 256f); } }
public static IColor LightBlue2 { get { return new IColor( 178  / 256f, 223  / 256f, 238  / 256f); } }
public static IColor LightBlue3 { get { return new IColor( 154  / 256f, 192  / 256f, 205  / 256f); } }
public static IColor LightBlue4 { get { return new IColor( 104  / 256f, 131  / 256f, 139  / 256f); } }
public static IColor LightCyan { get { return new IColor( 224  / 256f, 255  / 256f, 255  / 256f); } }
public static IColor LightCyan1 { get { return new IColor( 224  / 256f, 255  / 256f, 255  / 256f); } }
public static IColor LightCyan2 { get { return new IColor( 209  / 256f, 238  / 256f, 238  / 256f); } }
public static IColor LightCyan3 { get { return new IColor( 180  / 256f, 205  / 256f, 205  / 256f); } }
public static IColor LightCyan4 { get { return new IColor( 122  / 256f, 139  / 256f, 139  / 256f); } }
public static IColor LightSkyBlue { get { return new IColor( 135  / 256f, 206  / 256f, 250  / 256f); } }
public static IColor LightSkyBlue1 { get { return new IColor( 176  / 256f, 226  / 256f, 255  / 256f); } }
public static IColor LightSkyBlue2 { get { return new IColor( 164  / 256f, 211  / 256f, 238  / 256f); } }
public static IColor LightSkyBlue3 { get { return new IColor( 141  / 256f, 182  / 256f, 205  / 256f); } }
public static IColor LightSkyBlue4 { get { return new IColor( 96  / 256f, 123  / 256f, 139  / 256f); } }
public static IColor LightSlateBlue { get { return new IColor( 132  / 256f, 112  / 256f, 255  / 256f); } }
public static IColor LightSteelBlue { get { return new IColor( 176  / 256f, 196  / 256f, 222  / 256f); } }
public static IColor LightSteelBlue1 { get { return new IColor( 202  / 256f, 225  / 256f, 255  / 256f); } }
public static IColor LightSteelBlue2 { get { return new IColor( 188  / 256f, 210  / 256f, 238  / 256f); } }
public static IColor LightSteelBlue3 { get { return new IColor( 162  / 256f, 181  / 256f, 205  / 256f); } }
public static IColor LightSteelBlue4 { get { return new IColor( 110  / 256f, 123  / 256f, 139  / 256f); } }
public static IColor Aquamarine { get { return new IColor( 112  / 256f, 219  / 256f, 147  / 256f); } }
public static IColor MediumBlue { get { return new IColor( 0  / 256f, 0  / 256f, 205  / 256f); } }
public static IColor MediumSlateBlue { get { return new IColor( 123  / 256f, 104  / 256f, 238  / 256f); } }
public static IColor MediumTurquoise { get { return new IColor( 72  / 256f, 209  / 256f, 204  / 256f); } }
public static IColor MidnightBlue { get { return new IColor( 25  / 256f, 25  / 256f, 112  / 256f); } }
public static IColor NavyBlue { get { return new IColor( 0  / 256f, 0  / 256f, 128  / 256f); } }
public static IColor PaleTurquoise { get { return new IColor( 175  / 256f, 238  / 256f, 238  / 256f); } }
public static IColor PaleTurquoise1 { get { return new IColor( 187  / 256f, 255  / 256f, 255  / 256f); } }
public static IColor PaleTurquoise2 { get { return new IColor( 174  / 256f, 238  / 256f, 238  / 256f); } }
public static IColor PaleTurquoise3 { get { return new IColor( 150  / 256f, 205  / 256f, 205  / 256f); } }
public static IColor PaleTurquoise4 { get { return new IColor( 102  / 256f, 139  / 256f, 139  / 256f); } }
public static IColor PowderBlue { get { return new IColor( 176  / 256f, 224  / 256f, 230  / 256f); } }
public static IColor RoyalBlue { get { return new IColor( 65  / 256f, 105  / 256f, 225  / 256f); } }
public static IColor RoyalBlue1 { get { return new IColor( 72  / 256f, 118  / 256f, 255  / 256f); } }
public static IColor RoyalBlue2 { get { return new IColor( 67  / 256f, 110  / 256f, 238  / 256f); } }
public static IColor RoyalBlue3 { get { return new IColor( 58  / 256f, 95  / 256f, 205  / 256f); } }
public static IColor RoyalBlue4 { get { return new IColor( 39  / 256f, 64  / 256f, 139  / 256f); } }
public static IColor RoyalBlue5 { get { return new IColor( 0  / 256f, 34  / 256f, 102  / 256f); } }
public static IColor SkyBlue { get { return new IColor( 135  / 256f, 206  / 256f, 235  / 256f); } }
public static IColor SkyBlue1 { get { return new IColor( 135  / 256f, 206  / 256f, 255  / 256f); } }
public static IColor SkyBlue2 { get { return new IColor( 126  / 256f, 192  / 256f, 238  / 256f); } }
public static IColor SkyBlue3 { get { return new IColor( 108  / 256f, 166  / 256f, 205  / 256f); } }
public static IColor SkyBlue4 { get { return new IColor( 74  / 256f, 112  / 256f, 139  / 256f); } }
public static IColor SlateBlue { get { return new IColor( 106  / 256f, 90  / 256f, 205  / 256f); } }
public static IColor SlateBlue1 { get { return new IColor( 131  / 256f, 111  / 256f, 255  / 256f); } }
public static IColor SlateBlue2 { get { return new IColor( 122  / 256f, 103  / 256f, 238  / 256f); } }
public static IColor SlateBlue3 { get { return new IColor( 105  / 256f, 89  / 256f, 205  / 256f); } }
public static IColor SlateBlue4 { get { return new IColor( 71  / 256f, 60  / 256f, 139  / 256f); } }
public static IColor SteelBlue { get { return new IColor( 70  / 256f, 130  / 256f, 180  / 256f); } }
public static IColor SteelBlue1 { get { return new IColor( 99  / 256f, 184  / 256f, 255  / 256f); } }
public static IColor SteelBlue2 { get { return new IColor( 92  / 256f, 172  / 256f, 238  / 256f); } }
public static IColor SteelBlue3 { get { return new IColor( 79  / 256f, 148  / 256f, 205  / 256f); } }
public static IColor SteelBlue4 { get { return new IColor( 54  / 256f, 100  / 256f, 139  / 256f); } }
public static IColor aquamarine { get { return new IColor( 127  / 256f, 255  / 256f, 212  / 256f); } }
public static IColor aquamarine1 { get { return new IColor( 127  / 256f, 255  / 256f, 212  / 256f); } }
public static IColor aquamarine2 { get { return new IColor( 118  / 256f, 238  / 256f, 198  / 256f); } }
public static IColor aquamarine3 { get { return new IColor( 102  / 256f, 205  / 256f, 170  / 256f); } }
public static IColor aquamarine4 { get { return new IColor( 69  / 256f, 139  / 256f, 116  / 256f); } }
public static IColor azure { get { return new IColor( 240  / 256f, 255  / 256f, 255  / 256f); } }
public static IColor azure1 { get { return new IColor( 240  / 256f, 255  / 256f, 255  / 256f); } }
public static IColor azure2 { get { return new IColor( 224  / 256f, 238  / 256f, 238  / 256f); } }
public static IColor azure3 { get { return new IColor( 193  / 256f, 205  / 256f, 205  / 256f); } }
public static IColor azure4 { get { return new IColor( 131  / 256f, 139  / 256f, 139  / 256f); } }
public static IColor blue { get { return new IColor( 0  / 256f, 0  / 256f, 255  / 256f); } }
public static IColor blue1 { get { return new IColor( 0  / 256f, 0  / 256f, 255  / 256f); } }
public static IColor blue2 { get { return new IColor( 0  / 256f, 0  / 256f, 238  / 256f); } }
public static IColor blue3 { get { return new IColor( 0  / 256f, 0  / 256f, 205  / 256f); } }
public static IColor blue4 { get { return new IColor( 0  / 256f, 0  / 256f, 139  / 256f); } }
public static IColor aqua { get { return new IColor( 0  / 256f, 255  / 256f, 255  / 256f); } }
public static IColor cyan { get { return new IColor( 0  / 256f, 255  / 256f, 255  / 256f); } }
public static IColor cyan1 { get { return new IColor( 0  / 256f, 255  / 256f, 255  / 256f); } }
public static IColor cyan2 { get { return new IColor( 0  / 256f, 238  / 256f, 238  / 256f); } }
public static IColor cyan3 { get { return new IColor( 0  / 256f, 205  / 256f, 205  / 256f); } }
public static IColor cyan4 { get { return new IColor( 0  / 256f, 139  / 256f, 139  / 256f); } }
public static IColor navy { get { return new IColor( 0  / 256f, 0  / 256f, 128  / 256f); } }
public static IColor teal { get { return new IColor( 0  / 256f, 128  / 256f, 128  / 256f); } }
public static IColor turquoise { get { return new IColor( 64  / 256f, 224  / 256f, 208  / 256f); } }
public static IColor turquoise1 { get { return new IColor( 0  / 256f, 245  / 256f, 255  / 256f); } }
public static IColor turquoise2 { get { return new IColor( 0  / 256f, 229  / 256f, 238  / 256f); } }
public static IColor turquoise3 { get { return new IColor( 0  / 256f, 197  / 256f, 205  / 256f); } }
public static IColor turquoise4 { get { return new IColor( 0  / 256f, 134  / 256f, 139  / 256f); } }
public static IColor DarkSlateGray { get { return new IColor( 47  / 256f, 79  / 256f, 79  / 256f); } }
public static IColor DarkSlateGray1 { get { return new IColor( 151  / 256f, 255  / 256f, 255  / 256f); } }
public static IColor DarkSlateGray2 { get { return new IColor( 141  / 256f, 238  / 256f, 238  / 256f); } }
public static IColor DarkSlateGray3 { get { return new IColor( 121  / 256f, 205  / 256f, 205  / 256f); } }
public static IColor DarkSlateGray4 { get { return new IColor( 82  / 256f, 139  / 256f, 139  / 256f); } }
public static IColor NeonBlue { get { return new IColor( 77  / 256f, 77  / 256f, 255  / 256f); } }
public static IColor NewMidnightBlue { get { return new IColor( 0  / 256f, 0  / 256f, 156  / 256f); } }
public static IColor RichBlue { get { return new IColor( 89  / 256f, 89  / 256f, 171  / 256f); } }
public static IColor SummerSky { get { return new IColor( 56  / 256f, 176  / 256f, 222  / 256f); } }
public static IColor IrisBlue { get { return new IColor( 3  / 256f, 180  / 256f, 200  / 256f); } }
public static IColor FreeSpeechBlue { get { return new IColor( 65  / 256f, 86  / 256f, 197  / 256f); } }
public static IColor Brown { get { return new IColor( 188  / 256f, 143  / 256f, 143  / 256f); } }
public static IColor RosyBrown1 { get { return new IColor( 255  / 256f, 193  / 256f, 193  / 256f); } }
public static IColor RosyBrown2 { get { return new IColor( 238  / 256f, 180  / 256f, 180  / 256f); } }
public static IColor RosyBrown3 { get { return new IColor( 205  / 256f, 155  / 256f, 155  / 256f); } }
public static IColor RosyBrown4 { get { return new IColor( 139  / 256f, 105  / 256f, 105  / 256f); } }
public static IColor SaddleBrown { get { return new IColor( 139  / 256f, 69  / 256f, 19  / 256f); } }
public static IColor SandyBrown { get { return new IColor( 244  / 256f, 164  / 256f, 96  / 256f); } }
public static IColor beige { get { return new IColor( 245  / 256f, 245  / 256f, 220  / 256f); } }
public static IColor brown { get { return new IColor( 165  / 256f, 42  / 256f, 42  / 256f); } }
public static IColor brown1 { get { return new IColor( 255  / 256f, 64  / 256f, 64  / 256f); } }
public static IColor brown2 { get { return new IColor( 238  / 256f, 59  / 256f, 59  / 256f); } }
public static IColor brown3 { get { return new IColor( 205  / 256f, 51  / 256f, 51  / 256f); } }
public static IColor brown4 { get { return new IColor( 139  / 256f, 35  / 256f, 35  / 256f); } }
public static IColor darkbrown { get { return new IColor( 92  / 256f, 64  / 256f, 51  / 256f); } }
public static IColor burlywood { get { return new IColor( 222  / 256f, 184  / 256f, 135  / 256f); } }
public static IColor burlywood1 { get { return new IColor( 255  / 256f, 211  / 256f, 155  / 256f); } }
public static IColor burlywood2 { get { return new IColor( 238  / 256f, 197  / 256f, 145  / 256f); } }
public static IColor burlywood3 { get { return new IColor( 205  / 256f, 170  / 256f, 125  / 256f); } }
public static IColor burlywood4 { get { return new IColor( 139  / 256f, 115  / 256f, 85  / 256f); } }
public static IColor chocolate { get { return new IColor( 210  / 256f, 105  / 256f, 30  / 256f); } }
public static IColor chocolate1 { get { return new IColor( 255  / 256f, 127  / 256f, 36  / 256f); } }
public static IColor chocolate2 { get { return new IColor( 238  / 256f, 118  / 256f, 33  / 256f); } }
public static IColor chocolate3 { get { return new IColor( 205  / 256f, 102  / 256f, 29  / 256f); } }
public static IColor chocolate4 { get { return new IColor( 139  / 256f, 69  / 256f, 19  / 256f); } }
public static IColor peru { get { return new IColor( 205  / 256f, 133  / 256f, 63  / 256f); } }
public static IColor tan { get { return new IColor( 210  / 256f, 180  / 256f, 140  / 256f); } }
public static IColor tan1 { get { return new IColor( 255  / 256f, 165  / 256f, 79  / 256f); } }
public static IColor tan2 { get { return new IColor( 238  / 256f, 154  / 256f, 73  / 256f); } }
public static IColor tan3 { get { return new IColor( 205  / 256f, 133  / 256f, 63  / 256f); } }
public static IColor tan4 { get { return new IColor( 139  / 256f, 90  / 256f, 43  / 256f); } }
public static IColor DarkTan { get { return new IColor( 151  / 256f, 105  / 256f, 79  / 256f); } }
public static IColor DarkWood { get { return new IColor( 133  / 256f, 94  / 256f, 66  / 256f); } }
public static IColor LightWood { get { return new IColor( 133  / 256f, 99  / 256f, 99  / 256f); } }
public static IColor MediumWood { get { return new IColor( 166  / 256f, 128  / 256f, 100  / 256f); } }
public static IColor NewTan { get { return new IColor( 235  / 256f, 199  / 256f, 158  / 256f); } }
public static IColor SemiSweetChocolate{ get { return new IColor( 107  / 256f, 66  / 256f, 38  / 256f); } }
public static IColor Sienna { get { return new IColor( 142  / 256f, 107  / 256f, 35  / 256f); } }
public static IColor Tan { get { return new IColor( 219  / 256f, 147  / 256f, 112  / 256f); } }
public static IColor VeryDarkBrown { get { return new IColor( 92  / 256f, 64  / 256f, 51  / 256f); } }
public static IColor DarkGreen { get { return new IColor( 47  / 256f, 79  / 256f, 47  / 256f); } }
public static IColor darkgreencopper { get { return new IColor( 74  / 256f, 118  / 256f, 110  / 256f); } }
public static IColor DarkKhaki { get { return new IColor( 189  / 256f, 183  / 256f, 107  / 256f); } }
public static IColor DarkOliveGreen { get { return new IColor( 85  / 256f, 107  / 256f, 47  / 256f); } }
public static IColor DarkOliveGreen1 { get { return new IColor( 202  / 256f, 255  / 256f, 112  / 256f); } }
public static IColor DarkOliveGreen2 { get { return new IColor( 188  / 256f, 238  / 256f, 104  / 256f); } }
public static IColor DarkOliveGreen3 { get { return new IColor( 162  / 256f, 205  / 256f, 90  / 256f); } }
public static IColor DarkOliveGreen4 { get { return new IColor( 110  / 256f, 139  / 256f, 61  / 256f); } }
public static IColor olive { get { return new IColor( 128  / 256f, 128  / 256f, 0  / 256f); } }
public static IColor DarkSeaGreen { get { return new IColor( 143  / 256f, 188  / 256f, 143  / 256f); } }
public static IColor DarkSeaGreen1 { get { return new IColor( 193  / 256f, 255  / 256f, 193  / 256f); } }
public static IColor DarkSeaGreen2 { get { return new IColor( 180  / 256f, 238  / 256f, 180  / 256f); } }
public static IColor DarkSeaGreen3 { get { return new IColor( 155  / 256f, 205  / 256f, 155  / 256f); } }
public static IColor DarkSeaGreen4 { get { return new IColor( 105  / 256f, 139  / 256f, 105  / 256f); } }
public static IColor ForestGreen { get { return new IColor( 34  / 256f, 139  / 256f, 34  / 256f); } }
public static IColor GreenYellow { get { return new IColor( 173  / 256f, 255  / 256f, 47  / 256f); } }
public static IColor LawnGreen { get { return new IColor( 124  / 256f, 252  / 256f, 0  / 256f); } }
public static IColor LightSeaGreen { get { return new IColor( 32  / 256f, 178  / 256f, 170  / 256f); } }
public static IColor LimeGreen { get { return new IColor( 50  / 256f, 205  / 256f, 50  / 256f); } }
public static IColor MediumSeaGreen { get { return new IColor( 60  / 256f, 179  / 256f, 113  / 256f); } }
public static IColor MediumSpringGreen { get { return new IColor( 0  / 256f, 250  / 256f, 154  / 256f); } }
public static IColor MintCream { get { return new IColor( 245  / 256f, 255  / 256f, 250  / 256f); } }
public static IColor OliveDrab { get { return new IColor( 107  / 256f, 142  / 256f, 35  / 256f); } }
public static IColor OliveDrab1 { get { return new IColor( 192  / 256f, 255  / 256f, 62  / 256f); } }
public static IColor OliveDrab2 { get { return new IColor( 179  / 256f, 238  / 256f, 58  / 256f); } }
public static IColor OliveDrab3 { get { return new IColor( 154  / 256f, 205  / 256f, 50  / 256f); } }
public static IColor OliveDrab4 { get { return new IColor( 105  / 256f, 139  / 256f, 34  / 256f); } }
public static IColor PaleGreen { get { return new IColor( 152  / 256f, 251  / 256f, 152  / 256f); } }
public static IColor PaleGreen1 { get { return new IColor( 154  / 256f, 255  / 256f, 154  / 256f); } }
public static IColor PaleGreen2 { get { return new IColor( 144  / 256f, 238  / 256f, 144  / 256f); } }
public static IColor PaleGreen3 { get { return new IColor( 124  / 256f, 205  / 256f, 124  / 256f); } }
public static IColor PaleGreen4 { get { return new IColor( 84  / 256f, 139  / 256f, 84  / 256f); } }
public static IColor SeaGreenSeaGreen4 { get { return new IColor( 46  / 256f, 139  / 256f, 87  / 256f); } }
public static IColor SeaGreen1 { get { return new IColor( 84  / 256f, 255  / 256f, 159  / 256f); } }
public static IColor SeaGreen2 { get { return new IColor( 78  / 256f, 238  / 256f, 148  / 256f); } }
public static IColor SeaGreen3 { get { return new IColor( 67  / 256f, 205  / 256f, 128  / 256f); } }
public static IColor SpringGreen { get { return new IColor( 0  / 256f, 255  / 256f, 127  / 256f); } }
public static IColor SpringGreen1 { get { return new IColor( 0  / 256f, 255  / 256f, 127  / 256f); } }
public static IColor SpringGreen2 { get { return new IColor( 0  / 256f, 238  / 256f, 118  / 256f); } }
public static IColor SpringGreen3 { get { return new IColor( 0  / 256f, 205  / 256f, 102  / 256f); } }
public static IColor SpringGreen4 { get { return new IColor( 0  / 256f, 139  / 256f, 69  / 256f); } }
public static IColor YellowGreen { get { return new IColor( 154  / 256f, 205  / 256f, 50  / 256f); } }
public static IColor chartreuse { get { return new IColor( 127  / 256f, 255  / 256f, 0  / 256f); } }
public static IColor chartreuse1 { get { return new IColor( 127  / 256f, 255  / 256f, 0  / 256f); } }
public static IColor chartreuse2 { get { return new IColor( 118  / 256f, 238  / 256f, 0  / 256f); } }
public static IColor chartreuse3 { get { return new IColor( 102  / 256f, 205  / 256f, 0  / 256f); } }
public static IColor chartreuse4 { get { return new IColor( 69  / 256f, 139  / 256f, 0  / 256f); } }
public static IColor green { get { return new IColor( 0  / 256f, 255  / 256f, 0  / 256f); } }
public static IColor lime { get { return new IColor( 0  / 256f, 255  / 256f, 0  / 256f); } }
public static IColor green1 { get { return new IColor( 0  / 256f, 255  / 256f, 0  / 256f); } }
public static IColor green2 { get { return new IColor( 0  / 256f, 238  / 256f, 0  / 256f); } }
public static IColor green3 { get { return new IColor( 0  / 256f, 205  / 256f, 0  / 256f); } }
public static IColor green4 { get { return new IColor( 0  / 256f, 139  / 256f, 0  / 256f); } }
public static IColor khaki { get { return new IColor( 240  / 256f, 230  / 256f, 140  / 256f); } }
public static IColor khaki1 { get { return new IColor( 255  / 256f, 246  / 256f, 143  / 256f); } }
public static IColor khaki2 { get { return new IColor( 238  / 256f, 230  / 256f, 133  / 256f); } }
public static IColor khaki3 { get { return new IColor( 205  / 256f, 198  / 256f, 115  / 256f); } }
public static IColor khaki4 { get { return new IColor( 139  / 256f, 134  / 256f, 78  / 256f); } } 
public static IColor HunterGreen{ get { return new IColor( 142  / 256f, 35  / 256f, 35  / 256f); } }
public static IColor ForestGreenKhaki{ get { return new IColor( 35  / 256f, 142  / 256f, 35  / 256f); } }
public static IColor MediumForestGreen { get { return new IColor( 219  / 256f, 219  / 256f, 112  / 256f); } }
public static IColor SeaGreen { get { return new IColor( 35  / 256f, 142  / 256f, 104  / 256f); } }
public static IColor FreeSpeechGreen { get { return new IColor( 9  / 256f, 249  / 256f, 17  / 256f); } }
public static IColor DarkOrange{ get { return new IColor( 255  / 256f, 140  / 256f, 0  / 256f); } }
public static IColor DarkOrange1 { get { return new IColor( 255  / 256f, 127  / 256f, 0  / 256f); } }
public static IColor DarkOrange2 { get { return new IColor( 238  / 256f, 118  / 256f, 0  / 256f); } }
public static IColor DarkOrange3 { get { return new IColor( 205  / 256f, 102  / 256f, 0  / 256f); } }
public static IColor DarkOrange4 { get { return new IColor( 139  / 256f, 69  / 256f, 0  / 256f); } }
public static IColor DarkSalmon { get { return new IColor( 233  / 256f, 150  / 256f, 122  / 256f); } }
public static IColor LightCoral { get { return new IColor( 240  / 256f, 128  / 256f, 128  / 256f); } }
public static IColor LightSalmon { get { return new IColor( 255  / 256f, 160  / 256f, 122  / 256f); } }
public static IColor LightSalmon1 { get { return new IColor( 255  / 256f, 160  / 256f, 122  / 256f); } }
public static IColor LightSalmon2 { get { return new IColor( 238  / 256f, 149  / 256f, 114  / 256f); } }
public static IColor LightSalmon3 { get { return new IColor( 205  / 256f, 129  / 256f, 98  / 256f); } }
public static IColor LightSalmon4 { get { return new IColor( 139  / 256f, 87  / 256f, 66  / 256f); } }
public static IColor PeachPuff { get { return new IColor( 255  / 256f, 218  / 256f, 185  / 256f); } }
public static IColor PeachPuff1 { get { return new IColor( 255  / 256f, 218  / 256f, 185  / 256f); } }
public static IColor PeachPuff2 { get { return new IColor( 238  / 256f, 203  / 256f, 173  / 256f); } }
public static IColor PeachPuff3 { get { return new IColor( 205  / 256f, 175  / 256f, 149  / 256f); } }
public static IColor PeachPuff4 { get { return new IColor( 139  / 256f, 119  / 256f, 101  / 256f); } }
public static IColor bisque { get { return new IColor( 255  / 256f, 228  / 256f, 196  / 256f); } }
public static IColor bisque1 { get { return new IColor( 255  / 256f, 228  / 256f, 196  / 256f); } }
public static IColor bisque2 { get { return new IColor( 238  / 256f, 213  / 256f, 183  / 256f); } }
public static IColor bisque3 { get { return new IColor( 205  / 256f, 183  / 256f, 158  / 256f); } }
public static IColor bisque4 { get { return new IColor( 139  / 256f, 125  / 256f, 107  / 256f); } }
public static IColor coral { get { return new IColor( 255  / 256f, 127  / 256f, 0  / 256f); } }
public static IColor coral1 { get { return new IColor( 255  / 256f, 114  / 256f, 86  / 256f); } }
public static IColor coral2 { get { return new IColor( 238  / 256f, 106  / 256f, 80  / 256f); } }
public static IColor coral3 { get { return new IColor( 205  / 256f, 91  / 256f, 69  / 256f); } }
public static IColor coral4 { get { return new IColor( 139  / 256f, 62  / 256f, 47  / 256f); } }
public static IColor honeydew { get { return new IColor( 240  / 256f, 255  / 256f, 240  / 256f); } }
public static IColor honeydew1 { get { return new IColor( 240  / 256f, 255  / 256f, 240  / 256f); } }
public static IColor honeydew2 { get { return new IColor( 224  / 256f, 238  / 256f, 224  / 256f); } }
public static IColor honeydew3 { get { return new IColor( 193  / 256f, 205  / 256f, 193  / 256f); } }
public static IColor honeydew4 { get { return new IColor( 131  / 256f, 139  / 256f, 131  / 256f); } }
public static IColor orange { get { return new IColor( 255  / 256f, 165  / 256f, 0  / 256f); } }
public static IColor orange1 { get { return new IColor( 255  / 256f, 165  / 256f, 0  / 256f); } }
public static IColor orange2 { get { return new IColor( 238  / 256f, 154  / 256f, 0  / 256f); } }
public static IColor orange3 { get { return new IColor( 205  / 256f, 133  / 256f, 0  / 256f); } }
public static IColor orange4 { get { return new IColor( 139  / 256f, 90  / 256f, 0  / 256f); } }
public static IColor salmon { get { return new IColor( 250  / 256f, 128  / 256f, 114  / 256f); } }
public static IColor salmon1 { get { return new IColor( 255  / 256f, 140  / 256f, 105  / 256f); } }
public static IColor salmon2 { get { return new IColor( 238  / 256f, 130  / 256f, 98  / 256f); } }
public static IColor salmon3 { get { return new IColor( 205  / 256f, 112  / 256f, 84  / 256f); } }
public static IColor salmon4 { get { return new IColor( 139  / 256f, 76  / 256f, 57  / 256f); } }
public static IColor sienna { get { return new IColor( 160  / 256f, 82  / 256f, 45  / 256f); } }
public static IColor sienna1 { get { return new IColor( 255  / 256f, 130  / 256f, 71  / 256f); } }
public static IColor sienna2 { get { return new IColor( 238  / 256f, 121  / 256f, 66  / 256f); } }
public static IColor sienna3 { get { return new IColor( 205  / 256f, 104  / 256f, 57  / 256f); } }
public static IColor sienna4 { get { return new IColor( 139  / 256f, 71  / 256f, 38  / 256f); } }
public static IColor MandarianOrange { get { return new IColor( 142  / 256f, 35  / 256f, 35  / 256f); } }
public static IColor Orange { get { return new IColor( 255  / 256f, 127  / 256f, 0  / 256f); } }
public static IColor OrangeRed { get { return new IColor( 255  / 256f, 36  / 256f, 0  / 256f); } }
public static IColor DeepPink { get { return new IColor( 255  / 256f, 20  / 256f, 147  / 256f); } }
public static IColor DeepPink1 { get { return new IColor( 255  / 256f, 20  / 256f, 147  / 256f); } }
public static IColor DeepPink2 { get { return new IColor( 238  / 256f, 18  / 256f, 137  / 256f); } }
public static IColor DeepPink3 { get { return new IColor( 205  / 256f, 16  / 256f, 118  / 256f); } }
public static IColor DeepPink4 { get { return new IColor( 139  / 256f, 10  / 256f, 80  / 256f); } }
public static IColor HotPink { get { return new IColor( 255  / 256f, 105  / 256f, 180  / 256f); } }
public static IColor HotPink1 { get { return new IColor( 255  / 256f, 110  / 256f, 180  / 256f); } }
public static IColor HotPink2 { get { return new IColor( 238  / 256f, 106  / 256f, 167  / 256f); } }
public static IColor HotPink3 { get { return new IColor( 205  / 256f, 96  / 256f, 144  / 256f); } }
public static IColor HotPink4 { get { return new IColor( 139  / 256f, 58  / 256f, 98  / 256f); } }
public static IColor IndianRed { get { return new IColor( 205  / 256f, 92  / 256f, 92  / 256f); } }
public static IColor IndianRed1 { get { return new IColor( 255  / 256f, 106  / 256f, 106  / 256f); } }
public static IColor IndianRed2 { get { return new IColor( 238  / 256f, 99  / 256f, 99  / 256f); } }
public static IColor IndianRed3 { get { return new IColor( 205  / 256f, 85  / 256f, 85  / 256f); } }
public static IColor IndianRed4 { get { return new IColor( 139  / 256f, 58  / 256f, 58  / 256f); } }
public static IColor LightPink { get { return new IColor( 255  / 256f, 182  / 256f, 193  / 256f); } }
public static IColor LightPink1 { get { return new IColor( 255  / 256f, 174  / 256f, 185  / 256f); } }
public static IColor LightPink2 { get { return new IColor( 238  / 256f, 162  / 256f, 173  / 256f); } }
public static IColor LightPink3 { get { return new IColor( 205  / 256f, 140  / 256f, 149  / 256f); } }
public static IColor LightPink4 { get { return new IColor( 139  / 256f, 95  / 256f, 101  / 256f); } }
public static IColor MediumVioletRed { get { return new IColor( 199  / 256f, 21  / 256f, 133  / 256f); } }
public static IColor MistyRose { get { return new IColor( 255  / 256f, 228  / 256f, 225  / 256f); } }
public static IColor MistyRose1 { get { return new IColor( 255  / 256f, 228  / 256f, 225  / 256f); } }
public static IColor MistyRose2 { get { return new IColor( 238  / 256f, 213  / 256f, 210  / 256f); } }
public static IColor MistyRose3 { get { return new IColor( 205  / 256f, 183  / 256f, 181  / 256f); } }
public static IColor MistyRose4 { get { return new IColor( 139  / 256f, 125  / 256f, 123  / 256f); } }
public static IColor OrangeRed1 { get { return new IColor( 255  / 256f, 69  / 256f, 0  / 256f); } }
public static IColor OrangeRed2 { get { return new IColor( 238  / 256f, 64  / 256f, 0  / 256f); } }
public static IColor OrangeRed3 { get { return new IColor( 205  / 256f, 55  / 256f, 0  / 256f); } }
public static IColor OrangeRed4 { get { return new IColor( 139  / 256f, 37  / 256f, 0  / 256f); } }
public static IColor PaleVioletRed { get { return new IColor( 219  / 256f, 112  / 256f, 147  / 256f); } }
public static IColor PaleVioletRed1 { get { return new IColor( 255  / 256f, 130  / 256f, 171  / 256f); } }
public static IColor PaleVioletRed2 { get { return new IColor( 238  / 256f, 121  / 256f, 159  / 256f); } }
public static IColor PaleVioletRed3 { get { return new IColor( 205  / 256f, 104  / 256f, 137  / 256f); } }
public static IColor PaleVioletRed4 { get { return new IColor( 139  / 256f, 71  / 256f, 93  / 256f); } }
public static IColor VioletRed { get { return new IColor( 208  / 256f, 32  / 256f, 144  / 256f); } }
public static IColor VioletRed1 { get { return new IColor( 255  / 256f, 62  / 256f, 150  / 256f); } }
public static IColor VioletRed2 { get { return new IColor( 238  / 256f, 58  / 256f, 140  / 256f); } }
public static IColor VioletRed3 { get { return new IColor( 205  / 256f, 50  / 256f, 120  / 256f); } }
public static IColor VioletRed4 { get { return new IColor( 139  / 256f, 34  / 256f, 82  / 256f); } }
public static IColor firebrick { get { return new IColor( 178  / 256f, 34  / 256f, 34  / 256f); } }
public static IColor firebrick1 { get { return new IColor( 255  / 256f, 48  / 256f, 48  / 256f); } }
public static IColor firebrick2 { get { return new IColor( 238  / 256f, 44  / 256f, 44  / 256f); } }
public static IColor firebrick3 { get { return new IColor( 205  / 256f, 38  / 256f, 38  / 256f); } }
public static IColor firebrick4 { get { return new IColor( 139  / 256f, 26  / 256f, 26  / 256f); } }
public static IColor pink { get { return new IColor( 255  / 256f, 192  / 256f, 203  / 256f); } }
public static IColor pink1 { get { return new IColor( 255  / 256f, 181  / 256f, 197  / 256f); } }
public static IColor pink2 { get { return new IColor( 238  / 256f, 169  / 256f, 184  / 256f); } }
public static IColor pink3 { get { return new IColor( 205  / 256f, 145  / 256f, 158  / 256f); } }
public static IColor pink4 { get { return new IColor( 139  / 256f, 99  / 256f, 108  / 256f); } }
public static IColor Flesh { get { return new IColor( 245  / 256f, 204  / 256f, 176  / 256f); } }
public static IColor Feldspar { get { return new IColor( 209  / 256f, 146  / 256f, 117  / 256f); } }
public static IColor red1 { get { return new IColor( 255  / 256f, 0  / 256f, 0  / 256f); } }
public static IColor red2 { get { return new IColor( 238  / 256f, 0  / 256f, 0  / 256f); } }
public static IColor red3 { get { return new IColor( 205  / 256f, 0  / 256f, 0  / 256f); } }
public static IColor red4 { get { return new IColor( 139  / 256f, 0  / 256f, 0  / 256f); } }
public static IColor tomato { get { return new IColor( 255  / 256f, 99  / 256f, 71  / 256f); } }
public static IColor tomato1 { get { return new IColor( 255  / 256f, 99  / 256f, 71  / 256f); } }
public static IColor tomato2 { get { return new IColor( 238  / 256f, 92  / 256f, 66  / 256f); } }
public static IColor tomato3 { get { return new IColor( 205  / 256f, 79  / 256f, 57  / 256f); } }
public static IColor tomato4 { get { return new IColor( 139  / 256f, 54  / 256f, 38  / 256f); } }
public static IColor DustyRose { get { return new IColor( 133  / 256f, 99  / 256f, 99  / 256f); } }
public static IColor Firebrick { get { return new IColor( 142  / 256f, 35  / 256f, 35  / 256f); } }
public static IColor Pink { get { return new IColor( 188  / 256f, 143  / 256f, 143  / 256f); } }
public static IColor Salmon { get { return new IColor( 111  / 256f, 66  / 256f, 66  / 256f); } }
public static IColor Scarlet { get { return new IColor( 140  / 256f, 23  / 256f, 23  / 256f); } }
public static IColor SpicyPink { get { return new IColor( 255  / 256f, 28  / 256f, 174  / 256f); } }
public static IColor FreeSpeechMagenta { get { return new IColor( 227  / 256f, 91  / 256f, 216  / 256f); } }
public static IColor FreeSpeechRed { get { return new IColor( 192  / 256f, 0  / 256f, 0  / 256f); } }
public static IColor DarkOrchid{ get { return new IColor( 153  / 256f, 50  / 256f, 204  / 256f); } }
public static IColor DarkOrchid1 { get { return new IColor( 191  / 256f, 62  / 256f, 255  / 256f); } }
public static IColor DarkOrchid2 { get { return new IColor( 178  / 256f, 58  / 256f, 238  / 256f); } }
public static IColor DarkOrchid3 { get { return new IColor( 154  / 256f, 50  / 256f, 205  / 256f); } }
public static IColor DarkOrchid4 { get { return new IColor( 104  / 256f, 34  / 256f, 139  / 256f); } }
public static IColor DarkViolet { get { return new IColor( 148  / 256f, 0  / 256f, 211  / 256f); } }
public static IColor LavenderBlush { get { return new IColor( 255  / 256f, 240  / 256f, 245  / 256f); } }
public static IColor LavenderBlush1 { get { return new IColor( 255  / 256f, 240  / 256f, 245  / 256f); } }
public static IColor LavenderBlush2 { get { return new IColor( 238  / 256f, 224  / 256f, 229  / 256f); } }
public static IColor LavenderBlush3 { get { return new IColor( 205  / 256f, 193  / 256f, 197  / 256f); } }
public static IColor LavenderBlush4 { get { return new IColor( 139  / 256f, 131  / 256f, 134  / 256f); } }
public static IColor MediumOrchid { get { return new IColor( 186  / 256f, 85  / 256f, 211  / 256f); } }
public static IColor MediumOrchid1 { get { return new IColor( 224  / 256f, 102  / 256f, 255  / 256f); } }
public static IColor MediumOrchid2 { get { return new IColor( 209  / 256f, 95  / 256f, 238  / 256f); } }
public static IColor MediumOrchid3 { get { return new IColor( 180  / 256f, 82  / 256f, 205  / 256f); } }
public static IColor MediumOrchid4 { get { return new IColor( 122  / 256f, 55  / 256f, 139  / 256f); } }
public static IColor MediumPurple { get { return new IColor( 147  / 256f, 112  / 256f, 219  / 256f); } }
public static IColor MediumPurple1 { get { return new IColor( 171  / 256f, 130  / 256f, 255  / 256f); } }
public static IColor MediumPurple2 { get { return new IColor( 159  / 256f, 121  / 256f, 238  / 256f); } }
public static IColor MediumPurple3 { get { return new IColor( 137  / 256f, 104  / 256f, 205  / 256f); } }
public static IColor MediumPurple4 { get { return new IColor( 93  / 256f, 71  / 256f, 139  / 256f); } }
public static IColor lavender { get { return new IColor( 230  / 256f, 230  / 256f, 250  / 256f); } }
public static IColor magenta { get { return new IColor( 255  / 256f, 0  / 256f, 255  / 256f); } }
public static IColor fuchsia { get { return new IColor( 255  / 256f, 0  / 256f, 255  / 256f); } }
public static IColor magenta1 { get { return new IColor( 255  / 256f, 0  / 256f, 255  / 256f); } }
public static IColor magenta2 { get { return new IColor( 238  / 256f, 0  / 256f, 238  / 256f); } }
public static IColor magenta3 { get { return new IColor( 205  / 256f, 0  / 256f, 205  / 256f); } }
public static IColor magenta4 { get { return new IColor( 139  / 256f, 0  / 256f, 139  / 256f); } }
public static IColor maroon { get { return new IColor( 176  / 256f, 48  / 256f, 96  / 256f); } }
public static IColor maroon1 { get { return new IColor( 255  / 256f, 52  / 256f, 179  / 256f); } }
public static IColor maroon2 { get { return new IColor( 238  / 256f, 48  / 256f, 167  / 256f); } }
public static IColor maroon3 { get { return new IColor( 205  / 256f, 41  / 256f, 144  / 256f); } }
public static IColor maroon4 { get { return new IColor( 139  / 256f, 28  / 256f, 98  / 256f); } }
public static IColor orchid { get { return new IColor( 218  / 256f, 112  / 256f, 214  / 256f); } }
public static IColor Orchid { get { return new IColor( 219  / 256f, 112  / 256f, 219  / 256f); } }
public static IColor orchid1 { get { return new IColor( 255  / 256f, 131  / 256f, 250  / 256f); } }
public static IColor orchid2 { get { return new IColor( 238  / 256f, 122  / 256f, 233  / 256f); } }
public static IColor orchid3 { get { return new IColor( 205  / 256f, 105  / 256f, 201  / 256f); } }
public static IColor orchid4 { get { return new IColor( 139  / 256f, 71  / 256f, 137  / 256f); } }
public static IColor plum { get { return new IColor( 221  / 256f, 160  / 256f, 221  / 256f); } }
public static IColor plum1 { get { return new IColor( 255  / 256f, 187  / 256f, 255  / 256f); } }
public static IColor plum2 { get { return new IColor( 238  / 256f, 174  / 256f, 238  / 256f); } }
public static IColor plum3 { get { return new IColor( 205  / 256f, 150  / 256f, 205  / 256f); } }
public static IColor plum4 { get { return new IColor( 139  / 256f, 102  / 256f, 139  / 256f); } }
public static IColor purple { get { return new IColor( 160  / 256f, 32  / 256f, 240  / 256f); } }
public static IColor purple1 { get { return new IColor( 155  / 256f, 48  / 256f, 255  / 256f); } }
public static IColor purple2 { get { return new IColor( 145  / 256f, 44  / 256f, 238  / 256f); } }
public static IColor purple3 { get { return new IColor( 125  / 256f, 38  / 256f, 205  / 256f); } }
public static IColor purple4 { get { return new IColor( 85  / 256f, 26  / 256f, 139  / 256f); } }
public static IColor thistle { get { return new IColor( 216  / 256f, 191  / 256f, 216  / 256f); } }
public static IColor thistle1 { get { return new IColor( 255  / 256f, 225  / 256f, 255  / 256f); } }
public static IColor thistle2 { get { return new IColor( 238  / 256f, 210  / 256f, 238  / 256f); } }
public static IColor thistle3 { get { return new IColor( 205  / 256f, 181  / 256f, 205  / 256f); } }
public static IColor thistle4 { get { return new IColor( 139  / 256f, 123  / 256f, 139  / 256f); } }
public static IColor violet { get { return new IColor( 238  / 256f, 130  / 256f, 238  / 256f); } }
public static IColor violetblue { get { return new IColor( 159  / 256f, 95  / 256f, 159  / 256f); } }
public static IColor DarkPurple { get { return new IColor( 135  / 256f, 31  / 256f, 120  / 256f); } }
public static IColor Maroon { get { return new IColor( 245  / 256f, 204  / 256f, 176  / 256f); } }
public static IColor NeonPink { get { return new IColor( 255  / 256f, 110  / 256f, 199  / 256f); } }
public static IColor Plum { get { return new IColor( 234  / 256f, 173  / 256f, 234  / 256f); } }
public static IColor Thistle { get { return new IColor( 216  / 256f, 191  / 256f, 216  / 256f); } }
public static IColor Turquoise { get { return new IColor( 173  / 256f, 234  / 256f, 234  / 256f); } }
public static IColor Violet { get { return new IColor( 79  / 256f, 47  / 256f, 79  / 256f); } }
public static IColor AntiqueWhite1 { get { return new IColor( 255  / 256f, 239  / 256f, 219  / 256f); } }
public static IColor AntiqueWhite2 { get { return new IColor( 238  / 256f, 223  / 256f, 204  / 256f); } }
public static IColor AntiqueWhite3 { get { return new IColor( 205  / 256f, 192  / 256f, 176  / 256f); } }
public static IColor AntiqueWhite4 { get { return new IColor( 139  / 256f, 131  / 256f, 120  / 256f); } }
public static IColor FloralWhite { get { return new IColor( 255  / 256f, 250  / 256f, 240  / 256f); } }
public static IColor GhostWhite { get { return new IColor( 248  / 256f, 248  / 256f, 255  / 256f); } }
public static IColor NavajoWhite { get { return new IColor( 255  / 256f, 222  / 256f, 173  / 256f); } }
public static IColor NavajoWhite1 { get { return new IColor( 255  / 256f, 222  / 256f, 173  / 256f); } }
public static IColor NavajoWhite2 { get { return new IColor( 238  / 256f, 207  / 256f, 161  / 256f); } }
public static IColor NavajoWhite3 { get { return new IColor( 205  / 256f, 179  / 256f, 139  / 256f); } }
public static IColor NavajoWhite4 { get { return new IColor( 139  / 256f, 121  / 256f, 94  / 256f); } }
public static IColor OldLace { get { return new IColor( 253  / 256f, 245  / 256f, 230  / 256f); } }
public static IColor WhiteSmoke { get { return new IColor( 245  / 256f, 245  / 256f, 245  / 256f); } }
public static IColor gainsboro { get { return new IColor( 220  / 256f, 220  / 256f, 220  / 256f); } }
public static IColor ivory { get { return new IColor( 255  / 256f, 255  / 256f, 240  / 256f); } }
public static IColor ivory1 { get { return new IColor( 255  / 256f, 255  / 256f, 240  / 256f); } }
public static IColor ivory2 { get { return new IColor( 238  / 256f, 238  / 256f, 224  / 256f); } }
public static IColor ivory3 { get { return new IColor( 205  / 256f, 205  / 256f, 193  / 256f); } }
public static IColor ivory4 { get { return new IColor( 139  / 256f, 139  / 256f, 131  / 256f); } }
public static IColor linen { get { return new IColor( 250  / 256f, 240  / 256f, 230  / 256f); } }
public static IColor seashell { get { return new IColor( 255  / 256f, 245  / 256f, 238  / 256f); } }
public static IColor seashell1 { get { return new IColor( 255  / 256f, 245  / 256f, 238  / 256f); } }
public static IColor seashell2 { get { return new IColor( 238  / 256f, 229  / 256f, 222  / 256f); } }
public static IColor seashell3 { get { return new IColor( 205  / 256f, 197  / 256f, 191  / 256f); } }
public static IColor seashell4 { get { return new IColor( 139  / 256f, 134  / 256f, 130  / 256f); } }
public static IColor snow { get { return new IColor( 255  / 256f, 250  / 256f, 250  / 256f); } }
public static IColor snow1 { get { return new IColor( 255  / 256f, 250  / 256f, 250  / 256f); } }
public static IColor snow2 { get { return new IColor( 238  / 256f, 233  / 256f, 233  / 256f); } }
public static IColor snow3 { get { return new IColor( 205  / 256f, 201  / 256f, 201  / 256f); } }
public static IColor snow4 { get { return new IColor( 139  / 256f, 137  / 256f, 137  / 256f); } }
public static IColor wheat { get { return new IColor( 245  / 256f, 222  / 256f, 179  / 256f); } }
public static IColor wheat1 { get { return new IColor( 255  / 256f, 231  / 256f, 186  / 256f); } }
public static IColor wheat2 { get { return new IColor( 238  / 256f, 216  / 256f, 174  / 256f); } }
public static IColor wheat3 { get { return new IColor( 205  / 256f, 186  / 256f, 150  / 256f); } }
public static IColor wheat4 { get { return new IColor( 139  / 256f, 126  / 256f, 102  / 256f); } }
public static IColor white { get { return new IColor( 255  / 256f, 255  / 256f, 255  / 256f); } }
public static IColor Quartz { get { return new IColor( 217  / 256f, 217  / 256f, 243  / 256f); } }
public static IColor Wheat { get { return new IColor( 216  / 256f, 216  / 256f, 191  / 256f); } }
public static IColor BlanchedAlmond { get { return new IColor( 255  / 256f, 235  / 256f, 205  / 256f); } }
public static IColor DarkGoldenrod { get { return new IColor( 184  / 256f, 134  / 256f, 11  / 256f); } }
public static IColor DarkGoldenrod1 { get { return new IColor( 255  / 256f, 185  / 256f, 15  / 256f); } }
public static IColor DarkGoldenrod2 { get { return new IColor( 238  / 256f, 173  / 256f, 14  / 256f); } }
public static IColor DarkGoldenrod3 { get { return new IColor( 205  / 256f, 149  / 256f, 12  / 256f); } }
public static IColor DarkGoldenrod4 { get { return new IColor( 139  / 256f, 101  / 256f, 8  / 256f); } }
public static IColor LemonChiffon { get { return new IColor( 255  / 256f, 250  / 256f, 205  / 256f); } }
public static IColor LemonChiffon1 { get { return new IColor( 255  / 256f, 250  / 256f, 205  / 256f); } }
public static IColor LemonChiffon2 { get { return new IColor( 238  / 256f, 233  / 256f, 191  / 256f); } }
public static IColor LemonChiffon3 { get { return new IColor( 205  / 256f, 201  / 256f, 165  / 256f); } }
public static IColor LemonChiffon4 { get { return new IColor( 139  / 256f, 137  / 256f, 112  / 256f); } }
public static IColor LightGoldenrod { get { return new IColor( 238  / 256f, 221  / 256f, 130  / 256f); } }
public static IColor LightGoldenrod1 { get { return new IColor( 255  / 256f, 236  / 256f, 139  / 256f); } }
public static IColor LightGoldenrod2 { get { return new IColor( 238  / 256f, 220  / 256f, 130  / 256f); } }
public static IColor LightGoldenrod3 { get { return new IColor( 205  / 256f, 190  / 256f, 112  / 256f); } }
public static IColor LightGoldenrod4 { get { return new IColor( 139  / 256f, 129  / 256f, 76  / 256f); } }
public static IColor LightGoldenrodYellow { get { return new IColor( 250  / 256f, 250  / 256f, 210  / 256f); } }
public static IColor LightYellow { get { return new IColor( 255  / 256f, 255  / 256f, 224  / 256f); } }
public static IColor LightYellow1 { get { return new IColor( 255  / 256f, 255  / 256f, 224  / 256f); } }
public static IColor LightYellow2 { get { return new IColor( 238  / 256f, 238  / 256f, 209  / 256f); } }
public static IColor LightYellow3 { get { return new IColor( 205  / 256f, 205  / 256f, 180  / 256f); } }
public static IColor LightYellow4 { get { return new IColor( 139  / 256f, 139  / 256f, 122  / 256f); } }
public static IColor PaleGoldenrod { get { return new IColor( 238  / 256f, 232  / 256f, 170  / 256f); } }
public static IColor PapayaWhip { get { return new IColor( 255  / 256f, 239  / 256f, 213  / 256f); } }
public static IColor cornsilk { get { return new IColor( 255  / 256f, 248  / 256f, 220  / 256f); } }
public static IColor cornsilk1 { get { return new IColor( 255  / 256f, 248  / 256f, 220  / 256f); } }
public static IColor cornsilk2 { get { return new IColor( 238  / 256f, 232  / 256f, 205  / 256f); } }
public static IColor cornsilk3 { get { return new IColor( 205  / 256f, 200  / 256f, 177  / 256f); } }
public static IColor cornsilk4 { get { return new IColor( 139  / 256f, 136  / 256f, 120  / 256f); } }
public static IColor goldenrod { get { return new IColor( 218  / 256f, 165  / 256f, 32  / 256f); } }
public static IColor goldenrod1 { get { return new IColor( 255  / 256f, 193  / 256f, 37  / 256f); } }
public static IColor goldenrod2 { get { return new IColor( 238  / 256f, 180  / 256f, 34  / 256f); } }
public static IColor goldenrod3 { get { return new IColor( 205  / 256f, 155  / 256f, 29  / 256f); } }
public static IColor goldenrod4 { get { return new IColor( 139  / 256f, 105  / 256f, 20  / 256f); } }
public static IColor moccasin { get { return new IColor( 255  / 256f, 228  / 256f, 181  / 256f); } }
public static IColor yellow { get { return new IColor( 255  / 256f, 255  / 256f, 0  / 256f); } }
public static IColor yellow1 { get { return new IColor( 255  / 256f, 255  / 256f, 0  / 256f); } }
public static IColor yellow2 { get { return new IColor( 238  / 256f, 238  / 256f, 0  / 256f); } }
public static IColor yellow3 { get { return new IColor( 205  / 256f, 205  / 256f, 0  / 256f); } }
public static IColor yellow4 { get { return new IColor( 139  / 256f, 139  / 256f, 0  / 256f); } }
public static IColor gold { get { return new IColor( 255  / 256f, 215  / 256f, 0  / 256f); } }
public static IColor gold1 { get { return new IColor( 255  / 256f, 215  / 256f, 0  / 256f); } }
public static IColor gold2 { get { return new IColor( 238  / 256f, 201  / 256f, 0  / 256f); } }
public static IColor gold3 { get { return new IColor( 205  / 256f, 173  / 256f, 0  / 256f); } }
public static IColor gold4 { get { return new IColor( 139  / 256f, 117  / 256f, 0  / 256f); } }
public static IColor Goldenrod { get { return new IColor( 219  / 256f, 219  / 256f, 112  / 256f); } }
public static IColor MediumGoldenrod { get { return new IColor( 234  / 256f, 234  / 256f, 174  / 256f); } }

}