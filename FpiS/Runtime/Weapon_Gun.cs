using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Gun : MonoBehaviour
{
    [Header("---      Values     ---")]

    [SerializeField] private Gun_Profile[] profiles;
    [SerializeField] private int _profileIndex;
    public Gun_Profile profile { get { return ( profiles == null ? null : ( profiles.Length <= _profileIndex ? profiles[profiles.Length - 1] : profiles[_profileIndex] ) ); } }
    public int profileIndex { get { return _profileIndex; } set { profileIndex = value < 0 ? 0 : value >= profiles.Length ? profiles.Length - 1 : value; } }

    public float fireDelay { get { return profile.fireDelay; } }
    public float shotDelay { get { return profile.shotDelay; } }
    public int shotsCount { get { return profile.shotCount; } }
}
