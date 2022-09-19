using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlatform : Platfoem
{
    bool playerIn = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIn = true;
            Player player = other.GetComponent<Player>();
            player.OnObjectUse += Used;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIn = false;
            
        }
    }

    void Used()
    {
        if(playerIn)
        {
            isMoving = true;
        }
    }
}
