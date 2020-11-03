using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct VectorX
{
    public float[] values;

    public int dimension { get { return values.Length; } }
    public float x { get { return values[0]; } }
    public float y { get { return values[1]; } }
    public float z { get { return values[2]; } }
    public float w { get { return values[3]; } }
    public float a { get { return values[4]; } }
    public float b { get { return values[5]; } }
    public float c { get { return values[6]; } }
    public float d { get { return values[7]; } }
    public float e { get { return values[8]; } }
    public float f { get { return values[9]; } }
    public float g { get { return values[10]; } }
    public float h { get { return values[11]; } }
    public float i { get { return values[12]; } }
    public float j { get { return values[13]; } }
    public float k { get { return values[14]; } }
    public float l { get { return values[15]; } }
    public float m { get { return values[16]; } }
    public float n { get { return values[17]; } }
    public float o { get { return values[18]; } }
    public float p { get { return values[19]; } }
    public float q { get { return values[20]; } }
    public float r { get { return values[21]; } }
    public float s { get { return values[22]; } }
    public float t { get { return values[23]; } }
    public float u { get { return values[24]; } }
    public float v { get { return values[25]; } }

    public VectorX ( int _dimension )
    {
        values = new float[_dimension];
    }

    public VectorX ( float[] _valuesA = null , params float[] _valuesP )
    {
        values = _valuesA != null ? _valuesA : _valuesP;
    }

    public static VectorX Operation ( char operation , params VectorX[] vectors )
    {
        int dim =0;
        VectorX v = vectors[0]; /* ! */

        if (operation == '^')
        {
            for (int i = 0 ; i < v.values.Length ; i++)
            {
                v.values[i] = Mathf.Pow( v.values[i] , vectors[1].values[0] );
            }
            return v;
        }

        for (int i = 0 ; i < dim ; i++)
        {
            for (int ii = 1 /* ! */; ii < vectors.Length ; i++)
            {
                switch (operation)
                {
                    case '+':
                        v.values[i] += vectors[ii].values[i];
                        break;
                    case '*':
                        v.values[i] *= vectors[ii].values[i];
                        break;
                    case '-':
                        v.values[i] -= vectors[ii].values[i];
                        break;
                    case '/':
                        v.values[i] /= vectors[ii].values[i];
                        break;
                    default:
                        break;
                }
            }
        }

        return v;
    }

    public static int Dimension ( VectorX vector )
    {
        return vector.dimension;
    }
    public static int[] Dimensions ( VectorX[] vectors )
    {
        return System.Array.ConvertAll( vectors , new System.Converter<VectorX , int>( Dimension ) );
    }
    public VectorX ( float _value )
    {
        values = new float[1] { _value };
    }
    public static implicit operator float ( VectorX vector )
    {
        return vector.dimension;
    }

    public static VectorX operator ^ ( VectorX v , float p )
    {
        return Operation( '^' , v , new VectorX( p ) );
    }
    public static VectorX operator + ( VectorX a , VectorX b )
    {
        return Operation( '+' , a , b );
    }
    public static VectorX operator - ( VectorX a , VectorX b )
    {
        return Operation( '-' , a , b );
    }
    public static VectorX operator * ( VectorX a , VectorX b )
    {
        return Operation( '*' , a , b );
    }
    public static VectorX operator / ( VectorX a , VectorX b )
    {
        return Operation( '/' , a , b );
    }
    public static bool operator == ( VectorX a , VectorX b )
    {
        return a.Equals( b );
    }
    public static bool operator != ( VectorX a , VectorX b )
    {
        return !a.Equals( b );
    }
}
