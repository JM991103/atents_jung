using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeTimeGague : MonoBehaviour
{
    Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        Player player = GameManager.Inst.Player;
        player.onLifeTimeChange += OnLifeTimeChange;

        slider.value = 1.0f;
    }

    private void OnLifeTimeChange(float time, float maxTime)
    {
        slider.value = time / maxTime;      // 플레이어의 수명이 변경되면 슬라이더 값 변경
    }
}
