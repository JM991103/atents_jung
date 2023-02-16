using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishDeploymentButton : MonoBehaviour
{
    UserPlayer player;
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void Start()
    {
        player = GameManager.Inst.UserPlayer;
        foreach (var ship in player.Ships)
        {
            ship.onDeploy += OnshipDeployed;
        }
        button.interactable = false;
    }

    private void OnshipDeployed(bool isDeployed)
    {
        if (isDeployed && player.IsAllDeployed)
        {
            OnComplete();
        }
        else
        {
            OnNotComplete();
        }
    }

    private void OnClick()
    {
        Debug.Log("클릭");
        GameManager.Inst.SaveShipDeployment(player);
        SceneManager.LoadScene(2);
    }

    private void OnComplete()
    {
        button.interactable = true;
    }

    private void OnNotComplete()
    {
        button.interactable = false;
    }
}
