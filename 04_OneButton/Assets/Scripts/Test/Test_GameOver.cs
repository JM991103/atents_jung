using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;    // 파일 저장

public class Test_GameOver : Test_Base
{
    protected override void TestInput1(InputAction.CallbackContext obj)
    {

        //File.WriteAllText(@"e:\Test\test.txt", "Hello");
        SaveData saveData = new();
        saveData.BestScore = 100;
        saveData.name = "TestPlayer";

        string json = JsonUtility.ToJson(saveData); // 문자열로 반환 해준다.

        string path = $"{Application.dataPath}/Save/";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string fullPath = $"{path}Save.json";
        File.WriteAllText(fullPath, json);

        Debug.Log("세이브 완료");
    }

    protected override void TestInput2(InputAction.CallbackContext obj)
    {
        //string test = File.ReadAllText(@"e:\Test\Data.txt");
        //Debug.Log(test);
    }
}
