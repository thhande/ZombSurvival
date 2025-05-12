using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MireFixedJoystick : Joystick
{
    public static MireFixedJoystick instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
