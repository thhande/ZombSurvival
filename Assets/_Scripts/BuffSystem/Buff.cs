using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public BuffData buffData;
    public float duration = 15f;
    public float timer = 0;

    public Buff(BuffData data)
    {
        duration = data.duration;
    }

    public bool IsStillActive()
    {
        timer += Time.deltaTime;
        if (timer >= duration) return false;
        else return true;
    }
}
