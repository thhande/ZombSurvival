using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer playerSprite;

    const string ANIM_SPEED = "Speed";

    Vector2 movementDir;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
    }


    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void Update()
    {
        UpdateSpriteAnimation();

    }


    private void MovePlayer()
    {

        movementDir = InputManager.instance.GetMovementVector().normalized;
        rb.MovePosition(rb.position + movementDir * moveSpeed * Time.deltaTime);
    }

    private void UpdateSpriteAnimation()
    {

        if (movementDir.x != 0) playerSprite.flipX = movementDir.x <= 0;
        anim.SetFloat(ANIM_SPEED, movementDir.magnitude);

    }



}
