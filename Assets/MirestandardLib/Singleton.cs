using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MMono where T : MonoBehaviour
{
    public static T Instance;
    private void InitialInstance()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this as T;
    }

    protected override void Awake()
    {
        InitialInstance();
        base.Awake();

    }
}
