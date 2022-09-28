using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI timetext;

    bool isStart = false;
    float currentTime = 0.0f;


    public float CurrentTime
    {
        get => currentTime;
        set
        {
            currentTime = value;
            timetext.text = $"{currentTime:f2} 초";
        }
    }

    public float ResultTime { get => currentTime; }


    private void Awake()
    {
        timetext = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        
    }

    private void Start()
    {
        Goal2 goal = FindObjectOfType<Goal2>();
        goal.onGoalIn += StopTimer;
        currentTime = 0.0f;

        GameManager.Inst.onGameStart += StartTimer;
        //StartTimer();
    }

    private void Update()
    {
        if (isStart)
        {

            CurrentTime += Time.deltaTime;
        }
         
    }


    void StartTimer()
    {
        isStart = true;
        CurrentTime = 0.0f;
    }    

    void StopTimer()
    {
        isStart = false;
    }
}
