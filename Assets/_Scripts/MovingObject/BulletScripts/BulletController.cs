using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MMono
{
    [SerializeField] private BulletMoving movement;
    [SerializeField] private BulletDamageSender damageSender;
    [SerializeField] private SpriteRenderer bulletSprite;

    public void SetTarget(Vector2 target)
    {
        movement.SetTarget(target);
    }

    public void SetRotation(Vector2 dir)
    {
        movement.SetRotation(dir);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (movement == null) movement = GetComponent<BulletMoving>();
        if (damageSender == null) damageSender = GetComponent<BulletDamageSender>();
        if (bulletSprite == null) bulletSprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetBulletStats(WeaponProfile weapon)
    {
        damageSender.SetDamage(weapon.ShootDamage);
        bulletSprite.sprite = weapon.BulletSprite;
    }
}
