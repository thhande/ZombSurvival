using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageReceiver : DamageReceiver
{
    public EnemyDamageReceiver()
    {
        health = 10;
        maxHealth = 10;
    }
}
