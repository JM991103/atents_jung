using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    ResultPanel resultPanel;
    Button nextButton;
    Button RankButton;
    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        resultPanel = GetComponentInChildren<ResultPanel>();
        nextButton = transform.GetChild(2).GetComponent<Button>();
        RankButton = transform.GetChild(3).GetComponent<Button>();

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   // 현재 열린 씬을 새로 열기
    }

    void OnClick_Rank()
    {

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
