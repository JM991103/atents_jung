using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : MonoBehaviour
{
    Tab actionRank;
    Tab timeRank;
    ToggleButton toggleButton;

    private void Awake()
    {
        actionRank = transform.GetChild(0).GetComponent<Tab>();
        timeRank = transform.GetChild(1).GetComponent<Tab>();
        toggleButton = transform.GetChild(2).GetComponent<ToggleButton>();
    }

    private void Start()
    {
        GameManager gameManager = GameManager.Inst;
        gameManager.onGameClear += Open;
        gameManager.onGameOver += Open;
        gameManager.onGameReset += Close;
        Close();

        
    }

    void Open()
    {        
        this.gameObject.SetActive(true);
        toggleButton.SetToggleState(true);
        actionRank.ChildPanelOpen();
    }

    void Close()
    {
        this.gameObject.SetActive(false);
        toggleButton.SetToggleState(false);
        actionRank.ChildPanelClose();
    }

}