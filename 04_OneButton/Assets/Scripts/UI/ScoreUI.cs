using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private void Start()
    {
        GameManager.Inst.OnGameStart += OnGameStart;
        gameObject.SetActive(false);
    }

    private void OnGameStart()
    {
        gameObject.SetActive(true);
    }
}
