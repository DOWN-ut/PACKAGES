using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [Header("Parameters")]

    [SerializeField] private Type type = Type.Drive;
    [SerializeField] private bool isBack;
    [Space(5)]
    [SerializeField] private float torque = 1000;
    [Space(5)]
    [SerializeField] public float autoBrakesRatio = 0.1f;
    [SerializeField] private float breaks = 3000;
    [Space(5)]
    [SerializeField] private float steerSpeed = 5;
    [SerializeField] private float maxAngle = 30; public float maxSteerAngle { get { return maxAngle; } }
    [SerializeField]  [Range(0,100 )] private float steerResetSpeed = 50; float unsteerFactor { get { return 1 + ( 100f / steerResetSpeed ); } }

    //Values

    public bool canAccelerate { get { return type == Type.Both || type == Type.Drive; } }
    public bool canSteer { get { return type == Type.Both || type == Type.Steer; } }

    //[Header("Ingame")]

    bool accelerated,braked,steered;
    public float currentTorque { get { return wheelCollider.motorTorque; } }
    public float currentBreaks { get { return wheelCollider.brakeTorque; } }
    public float currentSteer { get { return wheelCollider.steerAngle; } }
    public float rpm { get { return wheelCollider.rpm; } }

    [Header("References")]

    public Transform visual;
    public WheelCollider wheelCollider;
    private Vector3 wheelPosition;
    private Quaternion wheelRotation;

    /// <summary>
    /// Make this wheel accelerate
    /// </summary>
    /// <param name="torqueMultiplicator"> multiplicator applied to the base torque of this wheel </param>
    /// <param name="accelerationMultiplicator"> mutliplicator applied to the base acceleration of this wheel </param>
    /// <returns></returns>
    public void Accelerate(float torqueMultiplicator = 1)
    {
        if (!canAccelerate) { return; }

        SetTorque( torqueMultiplicator * torque ) ;

        accelerated = true;
    }

    public void Steer(float angle, float multiplicator = 1 )
    {
        if (!canSteer) { return; }

        float a = Mathf.MoveTowards(currentSteer,angle * (isBack ? -1 : 1 ), multiplicator * Time.deltaTime * steerSpeed);
        SetSteer( a );
        
        steered = true;
    }

    public void Break ( float breakMultiplicator = 1)
    {
        SetBreaks( breakMultiplicator * breaks );

        braked = true; 
    }

    public void SetTorque(float value) { wheelCollider.motorTorque = value;  }
    public void SetBreaks(float value) { wheelCollider.brakeTorque = value;  }
    public void SetSteer(float value) { wheelCollider.steerAngle = value > maxAngle ? maxAngle : value < -maxAngle ? -maxAngle : value;  }

    void PassiveBehaviour ()
    {
        if (!accelerated) { 
            SetTorque( 0);

            if (!braked) { SetBreaks( autoBrakesRatio * breaks ); } 
            else { braked = false; }

        } else { SetBreaks( 0 ); accelerated = false; }

        if (!steered) { SetSteer( currentSteer / unsteerFactor ); } 
        else { steered = false; }  
    }

    private void FixedUpdate ()
    {
        PassiveBehaviour();
        SetVisual();
        GetInfos();
    }

    void SetVisual ()
    {
        visual.position = wheelPosition;
        visual.rotation = wheelRotation;
    }

    void GetInfos ()
    {
        wheelCollider.GetWorldPose( out wheelPosition , out wheelRotation );
    }

    public enum Type
    {
        Drive,
        Steer,
        Both
    }
}
