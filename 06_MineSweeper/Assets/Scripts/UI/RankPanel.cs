using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : MonoBehaviour
{
    Tab[] tabs;
    Tab selectedTab;
    ToggleButton toggle;

    private void Awake()
    {
        tabs = GetComponentsInChildren<Tab>();

        foreach (var tab in tabs)
        {
            tab.onTabSelected += (newSelectedTab) =>
            {
                if (newSelectedTab != selectedTab)   // 서로 다른 탭일 때만 변경
                {
                    selectedTab.IsSelected = false;
                    selectedTab = newSelectedTab;
                    selectedTab.IsSelected = true;
                }
            };
        }

        toggle = GetComponentInChildren<ToggleButton>();
        toggle.onToggleChange += (isOn) =>
        {
            if (isOn && selectedTab != null)
            {
                selectedTab.ChildPanelOpen();
            }
            else
            {
                foreach (var tab in tabs)
                {
                    tab.ChildPanelClose();
                }
            }
        };
    }

    private void Start()
    {
        GameManager gameManager = GameManager.Inst;
        gameManager.onGameClear += Open;
        gameManager.onGameOver += Open;
        gameManager.onGameReset += Close;

        selectedTab = tabs[0];
        selectedTab.IsSelected = true;

        Close();
    }

    void Open()
    {
        this.gameObject.SetActive(true);
    }

    void Close()
    {
        this.gameObject.SetActive(false);
        
    }

}