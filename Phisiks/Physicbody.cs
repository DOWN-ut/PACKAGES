using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Physicbody : MonoBehaviour
{
    [Header("Properties")]

    [SerializeField]
    private float _mass = 1;
    public float mass { get { return _mass; } set { _mass = value; rigidbody.mass = value; } }

    [SerializeField]
    private float _bouciness = 0.5f;
    public float bouciness { get { return _bouciness; } set { _bouciness = value > 0 ? ( value < 1 ? value : 1 ) : 0; } }
    [SerializeField]
    private float dynamicFriction = 0.3f;
    public float _dynamicFriction { get { return _dynamicFriction; } set { _dynamicFriction = value > 0 ? ( value < 1 ? value : 1 ) : 0; } }
    [SerializeField]
    private float staticFriction = 0.2f;
    public float _staticFriction { get { return _staticFriction; } set { _staticFriction = value > 0 ? ( value < 1 ? value : 1 ) : 0; } }

    public PhysicMaterialCombine frictionCombine = PhysicMaterialCombine.Average;

    [Header("Ingame")]

    private Vector3 _gravityDirection;
    private float _gravityForce;
    private bool localGravity = false;
    public bool useGravity = true;
    public Vector3 gravity { get { return useGravity ? localGravity ? (_gravityDirection.normalized * _gravityForce) : physicManager.gravity : Vector3.zero; } set { _gravityForce = value.magnitude; _gravityDirection = value.normalized; } }

    private PhysicMaterial physicMaterial;

    [Header( "References" )]
    [HideInInspector]
    private PhysicManager physicManager;
    public Rigidbody rigidbody { get { return GetComponent<Rigidbody>(); } }
    [HideInInspector]
    public Collider collider { get { return GetComponent<Collider>(); } }

    #region Main
    private void Awake ()
    {
        Setup();
    }
    private void FixedUpdate ()
    {
        ApplyGravity();
        Material();
    }
    #endregion

    #region Behaviour
    void ApplyGravity ()
    {
        rigidbody.AddForce( gravity , ForceMode.Acceleration );
    }
    void Material ()
    {
        physicMaterial.bounciness = bouciness;
        physicMaterial.dynamicFriction = dynamicFriction;
        physicMaterial.staticFriction = staticFriction;
        physicMaterial.frictionCombine = frictionCombine ;

        collider.material = physicMaterial;
    }
    #endregion

    #region Setup
    void Setup ()
    {     
        if (physicManager == null) { physicManager = FindObjectOfType<PhysicManager>(); }

        physicMaterial = new PhysicMaterial();
        mass = _mass;
    }
#endregion

    #region Methods

    public void SetGravityDirection(Vector3 g )
    {
        SetGravity( g.normalized * _gravityForce );
    }

    public void SetGravityForce(float g )
    {
        SetGravity( _gravityDirection.normalized * g );
    }

    public void SetGravity(Vector3 g )
    {
        gravity = g;
    }

    #endregion
}
