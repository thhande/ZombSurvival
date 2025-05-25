using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackRange : MonoBehaviour, IData
{
    [SerializeField] private List<EnemyDamageReceiver> enemiesInRange = new List<EnemyDamageReceiver>();
    [SerializeField] private Transform playerTransform;



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
    public EnemyDamageReceiver FindClosestEnemy()
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

    private void OnValidate()
    {
        Start();
    }

    private void Start()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        if (playerTransform == null) playerTransform = transform.parent;
    }
}
