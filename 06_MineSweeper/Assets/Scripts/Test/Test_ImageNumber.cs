using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_ImageNumber : TestBase
{
    [Range(-99,999)]
    public int TestNumber = 0;

    ImageNumber imageNumber;

    private void Start()
    {
        imageNumber = FindObjectOfType<ImageNumber>();
    }

    private void OnValidate()
    {
        if (imageNumber != null)
        {
            imageNumber.Number = TestNumber;
        }
    }
    protected override void Test1(InputAction.CallbackContext _)
    {
        GameManager.Inst.TestTimer_Play();
    }
    protected override void Test2(InputAction.CallbackContext _)
    {
        GameManager.Inst.TestTimer_Stop();
    }
    protected override void Test3(InputAction.CallbackContext _)
    {
        GameManager.Inst.TestTimer_Reset();
    }
}
