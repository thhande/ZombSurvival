using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{

    [SerializeField] private Vector2 movementInput;
    [SerializeField] private Vector2 attackDirInput;


    //UI Input Button
    private bool uiMeleeAttack = false;
    private bool uiRangedAttack = false;
    private bool uiSwitchWeaponOne = false;
    private bool uiSwitchWeaponTwo = false;
    private bool uiChangeWeaponOne = false;
    private bool uiChangeWeaponTwo = false;


    private void Update()
    {
        UpdateMovementInput();
        UpdateAttackInput();
    }


    private void UpdateMovementInput()
    {
        Joystick joystick = UIInputManager.Instance.MovementJoystick;
        if (joystick.Horizontal != 0 && joystick.Vertical != 0)
        {
            movementInput = new Vector2(joystick.Horizontal, joystick.Vertical);
            if (movementInput.magnitude > 1) movementInput.Normalize();
        }
        else
        {
            movementInput = Vector2.zero;
            if (LeftMovementInput()) movementInput.x -= 1;
            if (RightMovementInput()) movementInput.x += 1;
            if (UpMovementInput()) movementInput.y += 1;
            if (DownMovementInput()) movementInput.y -= 1;
        }
    }
    private void UpdateAttackInput()
    {
        Joystick joystick = UIInputManager.Instance.AttackJoystick;
        if (joystick.Horizontal != 0 && joystick.Vertical != 0)
        {
            attackDirInput = new Vector2(joystick.Horizontal, joystick.Vertical);
            if (attackDirInput.magnitude > 1) attackDirInput.Normalize();
        }
        else
        {
            attackDirInput = Vector2.zero;
            if (GetLeftAttackDirInput()) attackDirInput.x -= 1;
            if (GetRightAttackDirInput()) attackDirInput.x += 1;
            if (GetUpAttackDirInput()) attackDirInput.y += 1;
            if (GetDownAttackDirInput()) attackDirInput.y -= 1;
        }
    }

    public Vector2 GetAttackDirVector()
    {
        return attackDirInput;
    }

    private bool GetUpAttackDirInput()
    {
        return Input.GetKey(KeyCode.I);
    }
    private bool GetDownAttackDirInput()
    {
        return Input.GetKey(KeyCode.K);
    }
    private bool GetLeftAttackDirInput()
    {
        return Input.GetKey(KeyCode.J);
    }
    private bool GetRightAttackDirInput()
    {
        return Input.GetKey(KeyCode.L);
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
        return Input.GetKeyDown(KeyCode.Z);
    }

    private bool LeftMovementInput()
    {
        return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);

    }
    private bool RightMovementInput()
    {
        return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

    }
    private bool UpMovementInput()
    {
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

    }
    private bool DownMovementInput()
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




}
