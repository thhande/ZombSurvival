using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpawnObject : SpawnObject
{
    public BuffDrop buffDrop;

    private void OnValidate()
    {
#if UNITY_EDITOR
        buffDrop = GetComponent<BuffDrop>();
#endif
    }



    private void Awake()
    {
        buffDrop = GetComponent<BuffDrop>();
    }
}
