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
}
