using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnPrinter : MonoBehaviour
{
    TextMeshProUGUI turnText;

    private void Awake()
    {
        turnText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        TurnManager turnManager = TurnManager.Inst;
        turnManager.onTurnStart += OnTurnChange;
        turnText.text = $"1 턴";
    }

    private void OnTurnChange(int number)
    {
        turnText.text = $"{number} 턴";
    }
}
