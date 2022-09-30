using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestScore : MonoBehaviour
{
    public ImageNumber imageNumber;
    BirdInput inputaction;

    private void Awake()
    {
        inputaction = new BirdInput();
    }

    private void OnEnable()
    {
        inputaction.Test.Enable();
        inputaction.Test.Test0.performed += TestInput0;
        inputaction.Test.Test1.performed += TestInput1;
        inputaction.Test.Test2.performed += TestInput2;
        inputaction.Test.Test3.performed += TestInput3;
        inputaction.Test.Test4.performed += TestInput4;
        inputaction.Test.Test5.performed += TestInput5;
        inputaction.Test.Test6.performed += TestInput6;
        inputaction.Test.Test7.performed += TestInput7;
        inputaction.Test.Test8.performed += TestInput8;
        inputaction.Test.Test9.performed += TestInput9;
    }

    private void OnDisable()
    {
        inputaction.Test.Test0.performed += TestInput0;
        inputaction.Test.Test1.performed += TestInput1;
        inputaction.Test.Test2.performed += TestInput2;
        inputaction.Test.Test3.performed += TestInput3;
        inputaction.Test.Test4.performed += TestInput4;
        inputaction.Test.Test5.performed += TestInput5;
        inputaction.Test.Test6.performed += TestInput6;
        inputaction.Test.Test7.performed += TestInput7;
        inputaction.Test.Test8.performed += TestInput8;
        inputaction.Test.Test9.performed += TestInput9;
        inputaction.Test.Disable();
    }

    private void TestInput9(InputAction.CallbackContext obj)
    {
        imageNumber.Number = 9;
    }

    private void TestInput8(InputAction.CallbackContext obj)
    {
        imageNumber.Number = 8;
    }

    private void TestInput7(InputAction.CallbackContext obj)
    {
        imageNumber.Number = 7;
    }

    private void TestInput6(InputAction.CallbackContext obj)
    {
        imageNumber.Number = 6;
    }

    private void TestInput5(InputAction.CallbackContext obj)
    {
        imageNumber.Number = 5;
    }

    private void TestInput4(InputAction.CallbackContext obj)
    {
        imageNumber.Number = 4;
    }

    private void TestInput3(InputAction.CallbackContext obj)
    {
        imageNumber.Number = 3;
    }

    private void TestInput2(InputAction.CallbackContext obj)
    {
        imageNumber.Number = 2;
    }

    private void TestInput1(InputAction.CallbackContext obj)
    {
        imageNumber.Number = 1;
    }

    private void TestInput0(InputAction.CallbackContext obj)
    {
        imageNumber.Number = 0;
    }


}
