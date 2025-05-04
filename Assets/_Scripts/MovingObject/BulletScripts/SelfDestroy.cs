using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] private float selfDestroyTime = 7f;
    private void Awake()
    {
        Invoke(nameof(DoSelfDestroy), selfDestroyTime);
    }


    private void DoSelfDestroy()
    {
        Destroy(gameObject);
    }
}
