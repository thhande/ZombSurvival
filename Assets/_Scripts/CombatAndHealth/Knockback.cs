using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float knockbackForce = 10f;

    private void OnValidate()
    {
        LoadComponents();
    }

    private void Start()
    {
        LoadComponents();
    }


    private void LoadComponents()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyKnockback(Vector2 direction)
    {
        Debug.Log("Apply knockback in direction: " + direction);
        rb.velocity = Vector2.zero;
        rb.AddForce(direction.normalized * knockbackForce, ForceMode2D.Impulse);
    }
}
