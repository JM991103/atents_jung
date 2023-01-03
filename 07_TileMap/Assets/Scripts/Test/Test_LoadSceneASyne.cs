using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Test_LoadSceneASyne : TestBase
{
    protected override void Test1(InputAction.CallbackContext _)
    {
        //SceneManager.LoadScene(1, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }
}
