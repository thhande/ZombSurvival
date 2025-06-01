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

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(10);
        ReturnToPool();
    }
    private void OnEnable()
    {
        SelfDestroy();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
