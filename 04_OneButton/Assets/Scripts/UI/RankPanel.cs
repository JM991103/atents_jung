using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RankPanel : MonoBehaviour
{
    RankLine[] rankLines;

    TMP_InputField inputField;
    int rank;
    public bool InputNumberCompleted = false;
    public int Rank => rank;

    CanvasGroup canvasGroup;

    private void Awake()
    {
        rankLines = GetComponentsInChildren<RankLine>();
        inputField = GetComponentInChildren<TMP_InputField>();
        inputField.gameObject.SetActive(false);
        inputField.onEndEdit.AddListener(OnNameInputEnd);
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        GameManager.Inst.onRankRefresh += RankDataRefresh;
        GameManager.Inst.onRankUpdate += EnableNameInput;   // 이름을 입력 받을 수 있게 하기
        RankDataRefresh();
        Close();
    }

    private void OnDisable()
    {
        GameManager temp = GameManager.Inst;
        if (temp != null)
        {
            temp.onRankRefresh -= RankDataRefresh;
            temp.onRankUpdate -= EnableNameInput;
        }
    }

    public void Open()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void Close()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void RankDataRefresh()
    {

        for (int i = 0; i < rankLines.Length; i++)
        {
            rankLines[i].SetData(GameManager.Inst.HighScores[i], GameManager.Inst.HighScorer[i]);
        }
    }

    private void EnableNameInput(int index)
    {
        InputNumberCompleted = false;
        Open();
        rank = index;
        inputField.transform.position = new Vector3(inputField.transform.position.x, 
            rankLines[rank].transform.position.y,
            inputField.transform.position.z);
        inputField.gameObject.SetActive(true);
    }

    void OnNameInputEnd(string text)
    {
        GameManager temp = GameManager.Inst;
        if(temp != null)
        {
            temp.SetHighScoreName(rank, text);
        }
        inputField.gameObject.SetActive(false);
        InputNumberCompleted = true;
    }
}
