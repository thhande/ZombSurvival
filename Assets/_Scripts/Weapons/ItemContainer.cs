using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponContainer : MMono
{
    [SerializeField] public WeaponProfile weaponProfile;
    [SerializeField] public int bulletCount;
}
