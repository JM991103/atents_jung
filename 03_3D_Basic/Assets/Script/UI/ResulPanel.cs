using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResulPanel : MonoBehaviour
{
    TextMeshProUGUI clearText;
    Button button;
    Goal2 goal;

    float clearTime = 0.0f;
    public float ClearTime 
    {
        get => clearTime;
        set
        { 
            clearTime = value;
            clearText.text = $"클리어 하는데 {clearTime:f2}초 걸렸습니다.";
        }
    }
    private void Awake()
    {
        clearText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(ButtonTest2);

    }

    private void Start()
    {
        goal = FindObjectOfType<Goal2>();
        button.onClick.AddListener(goal.GoNextStage);
    }


    private void ButtonTest2()
    {
        SceneManager.LoadScene("Run");
    }

    public void buttonTest()
    {
        Debug.Log("버튼 클릭");
    }

}
