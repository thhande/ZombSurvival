using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IData
{
    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private PlayerBuffs buff;
    [SerializeField] PlayerCore core;

    const string ANIM_SPEED = "Speed";

    Vector2 movementDir;

    private void Awake()
    {
        LoadComponents();
    }

    private void Start()
    {
        LoadComponents();
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
        rb.MovePosition(rb.position + movementDir * (moveSpeed + buff.GetBonus(BuffType.Speed)) * Time.deltaTime);
    }

    private void UpdateSpriteAnimation()
    {

        if (movementDir.x != 0) playerSprite.flipX = movementDir.x <= 0;
        anim.SetFloat(ANIM_SPEED, movementDir.magnitude);

    }


    private void OnValidate()
    {

#if UNITY_EDITOR
        LoadComponents();
#endif
    }

    private void LoadComponents()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (anim == null) anim = GetComponentInChildren<Animator>();
        if (playerSprite == null) playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void Init(PlayerCore Core)
    {
        this.core = Core;
        this.buff = core.BuffMng;
    }




}
