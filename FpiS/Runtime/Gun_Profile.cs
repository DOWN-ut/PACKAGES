using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "Gun Profile" , menuName = "FpiS/Weapons/Gun Profile" , order = 1 )]
public class Gun_Profile : ScriptableObject
{
    [Header("---     Nominations     ---")]

    public string displayName;
    public writing names = new writing("",0,4);

    [Header("---     Values     ---")]

    [Header("Press the trigger 'firesPerSeconds' times per second, firing 'shotsPerFires' bullets with a rate of 'shotsPerSecond'")]
    [SerializeField] private float firesPerSecond;
    [SerializeField] private int shotsPerFire;
    [SerializeField] private float shotsPerSecond;

    [SerializeField] private float clockRate;
    [SerializeField] private float clockDelay { get { return 1f / clockRate; } }

    //[Header("Properties")]
    public float fireDelay { get { return clockDelay / firesPerSecond; } }
    public float shotDelay { get { return clockDelay / shotsPerSecond; } }
    public int shotCount { get { return shotsPerFire; } }
}
