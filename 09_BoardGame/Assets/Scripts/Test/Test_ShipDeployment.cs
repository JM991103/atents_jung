using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_ShipDeployment : TestBase
{
    Board board;

    private void Start()
    {
        board = FindObjectOfType<Board>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        inputActions.Test.TestClick.performed += OnClick;
    }

    protected override void OnDisable()
    {
        inputActions.Test.TestClick.performed -= OnClick;
        base.OnDisable();
    }

    private void OnClick(InputAction.CallbackContext _)
    {
        
    }
}
