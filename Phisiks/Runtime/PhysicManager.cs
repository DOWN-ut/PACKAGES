using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicManager : MonoBehaviour
{
    [Header("Values")]

    [SerializeField]
    private Vector3 _gravityDirection = -Vector3.up;
    [SerializeField]
    private float _gravityForce = 10;
    public Vector3 gravity { get { return _gravityDirection.normalized * _gravityForce; } set { _gravityDirection = value.normalized; _gravityForce = value.magnitude; } }

    [Header("Ingame")]

    private List<GravityField> gravityFields;

    private void Awake ()
    {
        Setup();
    }

    void Setup ()
    {
        gravityFields = new List<GravityField>( FindObjectsOfType<GravityField>() );
    }

    public Vector3 GravityAt ( Vector3 position, bool debugLocalP = false)
    {
        Vector3 g = Vector3.zero;

        foreach (GravityField gf in gravityFields)
        {
            if (!gf.gameObject.activeSelf) { continue; }
            g += gf.GravityAt( position, debugLocalP );
        }

        return g;
    }
}
