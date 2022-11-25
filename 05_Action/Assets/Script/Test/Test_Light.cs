using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Light : TestBase
{
    public Light targetLight;

    TrailRenderer trail;

    protected override void Awake()
    {
        base.Awake();
        trail = GetComponent<TrailRenderer>();
    }  
    

    protected override void Test1(InputAction.CallbackContext _)
    {
        trail.startWidth = 1.0f;
        trail.endWidth = 0.1f;
    }

    protected override void Test2(InputAction.CallbackContext _)
    {
        targetLight.color = Color.green;
    }

    protected override void Test3(InputAction.CallbackContext _)
    {
        targetLight.color = Color.blue;
    }

    protected override void Test4(InputAction.CallbackContext _)
    {
        targetLight.intensity = 5.0f;
    }
    protected override void Test5(InputAction.CallbackContext _)
    {
        targetLight.intensity = 1.0f;
    }
}
