using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResulPanel : MonoBehaviour
{
    TextMeshProUGUI clearText;      // 결과가 출력될 text
    Button button;                  // 다음 스테이지로 넘어가기 위한 버튼
    Goal2 goal;                     

    float clearTime = 0.0f;         // 클리어 하는데 걸린 시간(timer에서 받아온다.)
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
        button.onClick.AddListener(goal.GoNextStage);       // 버튼에 함수 연결
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
