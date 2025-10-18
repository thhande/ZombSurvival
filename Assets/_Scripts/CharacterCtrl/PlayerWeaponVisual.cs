using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer weaponSpriteRenderer;
    [SerializeField] private PlayerWeaponSlot weaponSlot;


    public void UpdateWeaponVisual()
    {
        weaponSpriteRenderer.sprite = weaponSlot.weaponProfile.weaponSprite;
    }
    public void UpdateWeaponAnim()
    {
        Vector2 direction = InputManager.Instance.GetAttackDirVector();
        if (direction == Vector2.zero) direction = InputManager.Instance.GetMovementVector().normalized;
        UpdateWeaponDir(direction);
    }

    private void UpdateWeaponDir(Vector2 direction)
    {
        if (direction.x != 0) weaponSpriteRenderer.flipY = direction.x < 0;
        if (direction == Vector2.zero) return;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void HideWeaponSprite()
    {
        if (weaponSpriteRenderer != null) weaponSpriteRenderer.enabled = false;
    }
    public void ShowWeaponSprite()
    {
        weaponSpriteRenderer.enabled = true;
    }

    public void RemoveSprite()
    {
        weaponSpriteRenderer.sprite = null;
    }

}
