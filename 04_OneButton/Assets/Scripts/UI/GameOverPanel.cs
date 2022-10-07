using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverPanel : MonoBehaviour
{
    ResultPanel resultPanel;
    RankPanel rankPanel;
    UnityEngine.UI.Button nextButton;
    UnityEngine.UI.Button RankButton;
    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        resultPanel = GetComponentInChildren<ResultPanel>();
        rankPanel = GetComponentInChildren<RankPanel>();
        nextButton = transform.GetChild(2).GetComponent<UnityEngine.UI.Button>();
        RankButton = transform.GetChild(3).GetComponent<UnityEngine.UI.Button>();

        nextButton.onClick.AddListener(OnClick_Next);
        RankButton.onClick.AddListener(OnClick_Rank);

    }

    private void Start()
    {
        Close();
        GameManager.Inst.Player.onDead += Open;
    }

    void OnClick_Next()
    {
        if (!rankPanel.InputNumberCompleted)
        {
            GameManager temp = GameManager.Inst;
            if(temp != null)
            {
                temp.SetHighScoreName(rankPanel.Rank, $"이름 없음{UnityEngine.Random.Range(0, 1000)}");
            }
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   // 현재 열린 씬을 새로 열기
    }

    void OnClick_Rank()
    {
        rankPanel.Open();
    }

    public void Open()
    {
        resultPanel.RefreshScore();
        StartCoroutine(OpenDlay());
    }

    void Close()
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    IEnumerator OpenDlay()
    {
        yield return new WaitForSeconds(2);

        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}
