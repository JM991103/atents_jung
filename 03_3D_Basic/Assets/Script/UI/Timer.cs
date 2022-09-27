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

    float time = 0.0f;
    bool isStart = false;
    float currentTime = 0.0f;


    float CurrentTime
    {
        get => currentTime;
        set
        {
            currentTime = value;
            timetext.text = $"{CurrentTime: f2} 초";
        }
    }

    private void Awake()
    {
        timetext = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        Goal2 goal = FindObjectOfType<Goal2>();
        goal.onGoalIn += StopTimer;
        StartTimer();
    }

    private void Update()
    {
        CurrentTime += Time.deltaTime;
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
