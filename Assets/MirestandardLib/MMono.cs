using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//auto loadcomponent before enter playtest
public class MMono : MonoBehaviour
{
    protected virtual void LoadComponents()
    {

    }

    protected virtual void Awake()
    {
        LoadComponents();
    }

    protected virtual void OnValidate()
    {
#if UNITY_EDITOR
        LoadComponents();
#endif
    }



}
