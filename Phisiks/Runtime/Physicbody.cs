using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Physicbody : MonoBehaviour
{
    #region ----- Header

    #region Properties
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

    #endregion

    #region Ingame

    [Header("Ingame")]

    private Vector3 _gravityDirection;
    private float _gravityForce;
    private bool localGravity = false;
    public bool useGravity = true;

    private PhysicMaterial physicMaterial;
    public Vector3 gravity { 
        get { return useGravity ? 
                localGravity ? (_gravityDirection.normalized * _gravityForce) : 
                physicManager.GravityAt(transform.position) : 
                Vector3.zero; } 
        set { _gravityForce = value.magnitude; _gravityDirection = value.normalized; } }

    public Vector3 velocity { get { return rigidbody.velocity; } set { rigidbody.velocity = value; } }

    #endregion

    #region References

    [Header( "References" )]
    [HideInInspector]
    public PhysicManager physicManager;
    public Rigidbody rigidbody { get { return GetComponent<Rigidbody>(); } }
    [HideInInspector]
    public Collider collider { get { return GetComponent<Collider>(); } }

    #endregion

    #endregion

    #region ----- Ingame

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

        rigidbody.useGravity = false;
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

    #endregion

    #region ---- Static

    public static Physicbody PhysicbodyOf(GameObject g )
    {
        if(g == null) { return null; }
        else if(g.GetComponent<Physicbody>() != null) { return g.GetComponent<Physicbody>(); }
        else if(g.transform.parent != null) { return PhysicbodyOf( g.transform.parent.gameObject ); }
        else { return null; }
    }

    public static Vector3 VelocityOf(GameObject g )
    {
        if(g == null) { return Vector3.zero; }

        Physicbody p = PhysicbodyOf(g);

        return p == null ? Vector3.zero : VelocityOf( p );
    }

    public static Vector3 VelocityOf(Physicbody p )
    {
        if (p == null) { return Vector3.zero; }

        return p.velocity;
    }

    #endregion
}
