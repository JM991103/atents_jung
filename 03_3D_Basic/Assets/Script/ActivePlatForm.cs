using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlatForm : Platform, IUseavleObject
{
    bool playerIn = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIn = true;
            IUser player = other.GetComponent<IUser>();
            player.onObjectUse += Use;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIn = false;
            IUser player = other.GetComponent<IUser>();
            player.onObjectUse -= Use;
        }
    }

    public void Use()
    {
        if (playerIn)   // 플레이어가 트리거안에 들어온 상태에서 사용해야 움직이기
        {
            isMoveing = true;
        }
    }
}
