using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageSender : MonoBehaviour
{
    [SerializeField] protected int damage;


    protected void OnTriggerEnter2D(Collider2D collision)
    {
        DealDamage(collision);
    }
    protected virtual void DealDamage(Collider2D collision) { }
}
