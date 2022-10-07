using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankPanel : MonoBehaviour
{
    RankLine[] rankLines;

    TMP_InputField inputField;

    private void Awake()
    {
        rankLines = GetComponentsInChildren<RankLine>();
        inputField = GetComponentInChildren<TMP_InputField>();
        inputField.gameObject.SetActive(false);
    }
    private void Start()
    {
        GameManager.Inst.onRankRefresh += RankDataRefresh;
        Game
        RankDataRefresh();
    }

    private void OnDisable()
    {
        GameManager temp = GameManager.Inst;
        if (temp != null)
        {
            temp.onRankRefresh -= RankDataRefresh;

        }
    }

    public void RankDataRefresh()
    {

        for (int i = 0; i < rankLines.Length; i++)
        {
            rankLines[i].SetData(GameManager.Inst.HighScores[i], GameManager.Inst.HighScorer[i]);
        }
    }
}
