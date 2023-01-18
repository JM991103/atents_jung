using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Network : TestBase
{
    protected override void Test1(InputAction.CallbackContext _)
    {
        NetPlayer player = GameManager.Inst.Player;
        Animator anim = player.GetComponent<Animator>();
        anim.SetTrigger("Test");
    }

    protected override void Test2(InputAction.CallbackContext _)
    {
        NetPlayer player = GameManager.Inst.Player;
        NetPlayerDecoration deco = player.GetComponent<NetPlayerDecoration>();
        Color color = Random.ColorHSV(0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f);
        deco.SetPlayerColorServerRpc(color);
    }

    protected override void Test3(InputAction.CallbackContext _)
    {
        
    }
}
