using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;


    [SerializeField] private Vector2 movementInput;




    //UI Input
    private bool uiMeleeAttack = false;
    private bool uiRangedAttack = false;
    private bool uiSwitchWeaponOne = false;
    private bool uiSwitchWeaponTwo = false;
    private bool uiChangeWeaponOne = false;
    private bool uiChangeWeaponTwo = false;


    private void Update()
    {
        UpdateMovementInput();
    }


    private void UpdateMovementInput()
    {
        Joystick joystick = UIInputManager.instance.joystick;
        if (joystick.Horizontal != 0 && joystick.Vertical != 0)
        {
            movementInput = new Vector2(joystick.Horizontal, joystick.Vertical);
            if (movementInput.magnitude > 1) movementInput.Normalize();

        }

        else
        {
            movementInput = Vector2.zero;
            if (LeftInput()) movementInput.x -= 1;
            if (RightInput()) movementInput.x += 1;
            if (UpInput()) movementInput.y += 1;
            if (DownInput()) movementInput.y -= 1;
        }

    }

    public Vector2 GetMovementVector()
    {
        return movementInput.normalized;
    }

    public bool GetMeleeAttackInput()
    {
        if (uiMeleeAttack)
        {
            uiMeleeAttack = false;
            return true;
        }
        return Input.GetKeyDown(KeyCode.Space);
    }

    public bool GetRangedAttackInput()
    {
        if (uiRangedAttack)
        {
            uiRangedAttack = false;
            return true;
        }
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
        if (uiSwitchWeaponOne)
        {
            uiSwitchWeaponOne = false;
            return true;
        }
        return Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1);
    }
    public bool SwitchToWeaponSlotTwo()
    {
        if (uiSwitchWeaponTwo)
        {
            uiSwitchWeaponTwo = false;
            return true;
        }
        return Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2);
    }
    public bool ChangeWeaponInSlotOne()
    {
        if (uiChangeWeaponOne)
        {
            uiChangeWeaponOne = false;
            return true;
        }
        return Input.GetKeyDown(KeyCode.Q);
    }
    public bool ChangeWeaponInSlotTwo()
    {
        if (uiChangeWeaponTwo)
        {
            uiChangeWeaponTwo = false;
            return true;
        }
        return Input.GetKeyDown(KeyCode.E);
    }

    public void SetMovementInput(Vector2 Dir)
    {
        movementInput = Dir.normalized;
    }


    // UI Input
    public void UIMeleeAttackInput()
    {
        uiMeleeAttack = true;
    }

    public void UIRangedAttackInput()
    {
        uiRangedAttack = true;
    }
    public void UISwitchToWeaponSlotOne()
    {
        uiSwitchWeaponOne = true;
    }
    public void UISwitchToWeaponSlotTwo()
    {
        uiSwitchWeaponTwo = true;
    }
    public void UIChangeWeaponInSlotOne()
    {
        uiChangeWeaponOne = true;
    }
    public void UIChangeWeaponInSlotTwo()
    {
        uiChangeWeaponTwo = true;
    }



    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

    }

}
