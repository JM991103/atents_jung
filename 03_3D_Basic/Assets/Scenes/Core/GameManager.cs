using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public Action onGameStart;
    Timer timer;
    Player player;
    ResulPanel resultPanel;

    bool isGameStart = false;

    public Player Player { get => player; }
    public bool IsGameStart 
    { 
        get => isGameStart;
        private set
        {
            isGameStart = value;
            if (isGameStart)
            {
                onGameStart?.Invoke();
            }
        }
    }

    protected override void Initialize()
    {
        isGameStart = false;

        timer = FindObjectOfType<Timer>();
        player = FindObjectOfType<Player>();
        resultPanel = FindObjectOfType<ResulPanel>();
        resultPanel?.gameObject.SetActive(false);   //resultPanel이 null이 아니면 실행
    }

    public void GameStart()
    {
        if (!IsGameStart)
        {
            IsGameStart = true;
        }
    }

    public void ShowResultPanel()
    {
        if (resultPanel != null)
        {
            resultPanel.ClearTime = timer.ResultTime;
            resultPanel?.gameObject.SetActive(true);
        }
    }
}
