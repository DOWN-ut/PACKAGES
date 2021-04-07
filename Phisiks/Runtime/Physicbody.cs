using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

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

    [Space(3)]

    public int velocityRecorderCount = 2;

    #endregion

    #region Ingame

    [Space(10)]
    [Header("Ingame")]

    private Vector3 _gravityDirection;
    private float _gravityForce;
    private bool localGravity = false;
    public bool useGravity { get { return usingGravity; } }

    public toggler usingGravity = new toggler(toggler.Type.AND,true);

    private PhysicMaterial physicMaterial;
    public Vector3 gravity { 
        get { return useGravity ? 
                localGravity ? (_gravityDirection.normalized * _gravityForce) : 
                physicManager.GravityAt(transform.position,debugGFieldLocalP) : 
                Vector3.zero; } 
        set { _gravityForce = value.magnitude; _gravityDirection = value.normalized; } }

    public Vector3 velocity { get { return rigidbody.velocity; } set { rigidbody.velocity = value; } } public float velocityMagnitude { get { return velocity.magnitude; } }
    public Vector3[] velocities;
    public Vector3 acceleration { get { return velocities.Length >= 2 ? velocities[0] - velocities[1] : Vector3.zero; } } public float accelerationMagnitude { get { return acceleration.magnitude; } }

    public float kineticEnergy { get { return velocityMagnitude * mass; } }

    #endregion

        #region References

        [Header( "References" )]
    [HideInInspector]
    public PhysicManager physicManager;
    public Rigidbody rigidbody { get { return GetComponent<Rigidbody>(); } }
    [HideInInspector]
    public Collider collider { get { return GetComponent<Collider>(); } }
    [Header("Debug")]
    public bool debugGFieldLocalP = false;

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
        GetInfos();
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
    void GetInfos ()
    {
        for(int i = velocities.Length - 1 ; i > 0; i--)
        {
            velocities[i] = velocities[i - 1];
        }
        velocities[0] = velocity;
    }

    #endregion

    #region Setup
    void Setup ()
    {     
        if (physicManager == null) { physicManager = FindObjectOfType<PhysicManager>(); }

        rigidbody.useGravity = false;
        physicMaterial = new PhysicMaterial();
        mass = _mass;

        velocities = new Vector3[velocityRecorderCount];
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

#if UNITY_EDITOR

[CustomEditor( typeof( Physicbody ) )]
[CanEditMultipleObjects]
public class PhysicbodyEditor : Editor
{
    GUIStyle titleStyle;
    float titleHeight = 50; int titleFontSize = 25;

    GUIStyle enumStyle;
    float enumHeight = 20; int enumFontSize = 25;

    GUIStyle arrowStyle;
    int arrowFontSize = 40;

    GUIStyle infoStyle;
    int infoFontSize = 11;    
    GUIStyle infoTitleStyle;
    int infoTitleFontSize = 15;

    bool displayInfos;

    void Styles ()
    {
        titleStyle = GUI.skin.label; titleStyle.alignment = TextAnchor.MiddleCenter; titleStyle.fontStyle = FontStyle.Bold; titleStyle.fontSize = titleFontSize;
        arrowStyle = GUI.skin.label; arrowStyle.alignment = TextAnchor.MiddleCenter; arrowStyle.fontStyle = FontStyle.Bold; arrowStyle.fontSize = arrowFontSize;
        enumStyle = GUI.skin.label; enumStyle.alignment = TextAnchor.MiddleCenter; enumStyle.fontStyle = FontStyle.Normal; enumStyle.fontSize = enumFontSize;

        infoStyle = GUI.skin.label; infoStyle.alignment = TextAnchor.MiddleCenter; infoStyle.fontStyle = FontStyle.Bold; infoStyle.fontSize = infoFontSize;
        infoTitleStyle = GUI.skin.box; infoTitleStyle.alignment = TextAnchor.MiddleCenter; infoTitleStyle.fontStyle = FontStyle.Bold; infoTitleStyle.fontSize = infoTitleFontSize;
    }

    public override void OnInspectorGUI ()
    {
        float width = EditorGUIUtility.currentViewWidth;
        Physicbody physicbody = target as Physicbody;

        Styles();

        serializedObject.Update();

        if(GUILayout.Button(displayInfos ? "Fold" : "Unfold" )) { displayInfos = !displayInfos; }

        if (displayInfos)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField( "Acceleration : " ,infoTitleStyle,GUILayout.Height(20),GUILayout.Width(width * .4f));
            EditorGUILayout.LabelField( physicbody.accelerationMagnitude.ToString("F1"), infoStyle , GUILayout.Height( 20 ) , GUILayout.Width( width * .1f ) );
            EditorGUILayout.LabelField( physicbody.acceleration.ToString("F1"), infoStyle , GUILayout.Height( 20 ) , GUILayout.Width( width * .5f ) );
            EditorGUILayout.EndHorizontal();
        }

        GUING.Line( 1 , 5 );

        DrawDefaultInspector();

        serializedObject.ApplyModifiedProperties();
    }
}

#endif