using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponContainer : MonoBehaviour
{
    [SerializeField] public WeaponProfile weaponProfile;
    [SerializeField] protected int bulletCount;
}
