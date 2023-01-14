using UnityEngine.InputSystem;
using TMPro;
using System;

public class Test_Logger : TestBase
{
    public TMP_InputField inputField;

    private void Start()
    {
        inputField.onSubmit.AddListener(OninputFinish); // 엔터가 눌러졌을때만 실행
        //inputField.onEndEdit.AddListener(OninputFinish); // 인풋필드에서 포커스가 옮겨질 때만 실행(다른곳 클릭, 엔터치기 등)
    }

    private void OninputFinish(string text)
    {
        GameManager.Inst.Logger.Log(text);
        inputField.text = "";

        //inputField.Select();              // 켜져있던것은 끄고 꺼져있던 것은 켜기
        inputField.ActivateInputField();    // 인풋필드 활성화 시키기
    }

    protected override void Test1(InputAction.CallbackContext _)
    {
        //GameManager.Inst.Logger.Log("123123\n");
    }    
}
