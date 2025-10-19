using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackRange : MMono, IData<PlayerCore>
{
    [SerializeField] private List<EnemyDamageReceiver> enemiesInRange = new List<EnemyDamageReceiver>();
    [SerializeField] private Transform playerTransform;
    [SerializeField] private PlayerCore core;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyDamageReceiver enemy = collision.GetComponent<EnemyDamageReceiver>();
        if (enemy != null) enemiesInRange.Add(enemy);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemyDamageReceiver leftEnemy = collision.GetComponent<EnemyDamageReceiver>();
        if (leftEnemy != null) enemiesInRange.Remove(leftEnemy);
    }
    private EnemyDamageReceiver FindClosestEnemy()
    {
        EnemyDamageReceiver closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        foreach (EnemyDamageReceiver enemy in enemiesInRange)
        {
            float distance = Vector2.Distance(playerTransform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;

    }
    public Vector2 GetClosestEnemyPos()
    {
        EnemyDamageReceiver closestEnemy = FindClosestEnemy();
        return closestEnemy != null ? closestEnemy.transform.position : Vector2.zero;
    }

    public void UpdateAimSightRotation()
    {
        Vector2 aimDirInput = InputManager.Instance.GetAttackDirVector();
        Vector2 moveDirInput = InputManager.Instance.GetMovementVector();
        if (aimDirInput == Vector2.zero) SetRotation(moveDirInput);
        else SetRotation(aimDirInput);
    }

    private void SetRotation(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void Init(PlayerCore _core)
    {
        core = _core;
        playerTransform = core.transform;
    }

}
