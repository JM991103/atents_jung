using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TestPanel : MonoBehaviour
{
    TMP_InputField inputField;
    Button dieButton;


    private void Awake()
    {
        inputField = GetComponentInChildren<TMP_InputField>();
        dieButton = GetComponentInChildren<Button>();

        inputField.onValueChanged.AddListener(onInputValueChanged);
        dieButton.onClick.AddListener(OnDieButtonClick);
    }

    private void onInputValueChanged(string text)
    {
        int score = 0;
        if(text != "")
        {
            score = int.Parse(text);
        }
        GameManager.Inst.TestSetScore(score);
    }

    private void OnDieButtonClick()
    {
        GameManager.Inst.Player.TestDie();
    }
}
