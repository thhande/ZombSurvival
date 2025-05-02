using UnityEngine;




[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/WeaponData")]
public class WeaponProfile : ScriptableObject
{
    public string weaponName;
    public int meleeDamage;
    public GameObject bulletPrefab;
    public Sprite weaponSprite;
    public WeaponType weaponType;

}
