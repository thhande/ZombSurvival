using UnityEngine;



[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/WeaponData")]
public class WeaponProfile : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private BulletController bulletPrefab;
    [SerializeField] private Sprite weaponSprite;
    [SerializeField] private Sprite bulletSprite;
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int meleeDamage;
    [SerializeField] private int shootDamage;


    public string WeaponName => weaponName;
    public int MeleeDamage => meleeDamage;
    public BulletController BulletPrefab => bulletPrefab;
    public Sprite WeaponSprite => weaponSprite;
    public Sprite BulletSprite => bulletSprite;
    public WeaponType WeaponType => weaponType;
    public float AttackSpeed => attackSpeed;
    public int ShootDamage => shootDamage;

}
