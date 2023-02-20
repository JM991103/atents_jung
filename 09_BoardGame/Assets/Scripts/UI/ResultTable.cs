using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultTable : MonoBehaviour
{
    TextMeshProUGUI victory;

    private void Awake()
    {
        victory = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void SetVictory()
    {
        victory.text = "승리!";
    }

    public void SetDefeat()
    {
        victory.text = "패배...";
    }
}
