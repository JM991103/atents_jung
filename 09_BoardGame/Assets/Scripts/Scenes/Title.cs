using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.AnyKey.Enable();
        inputActions.AnyKey.AnyKey.performed += OnPressAnyKey;
    }

    private void OnDisable()
    {
        inputActions.AnyKey.AnyKey.performed -= OnPressAnyKey;
        inputActions.AnyKey.Disable();
    }

    private void OnPressAnyKey(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(1);
    }
}
