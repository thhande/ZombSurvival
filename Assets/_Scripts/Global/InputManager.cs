using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [SerializeField] private Vector2 movementInput;


    void Update()
    {
        UpdateMovementInput();
    }


    private void UpdateMovementInput()
    {
        movementInput = Vector2.zero;
        if (LeftInput()) movementInput.x -= 1;
        if (RightInput()) movementInput.x += 1;
        if (UpInput()) movementInput.y += 1;
        if (DownInput()) movementInput.y -= 1;

    }

    public Vector2 GetMovementVector()
    {
        return movementInput.normalized;
    }

    public bool GetMeleeAttackInput()
    {
        return Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
    }

    public bool GetRangedAttackInput()
    {
        return Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(1);
    }

    private bool LeftInput()
    {
        return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);

    }
    private bool RightInput()
    {
        return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

    }
    private bool UpInput()
    {
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

    }
    private bool DownInput()
    {
        return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

    }
    public bool SwitchToWeaponSlotOne()
    {
        return Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1);
    }
    public bool SwitchToWeaponSlotTwo()
    {
        return Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2);
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
