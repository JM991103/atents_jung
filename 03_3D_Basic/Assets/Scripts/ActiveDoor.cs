using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveDoor : MonoBehaviour, IUseableObject
{
    Animator anim;
    bool playerIn = false;
    bool ondoor = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("문이 열려야 한다.");
            playerIn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIn = false;
            //Debug.Log("문이 닫혀야 한다.");
            anim.SetBool("isOpen", false);
        }
    }


    public void Use()
    {
        if (playerIn)
        {
            ondoor = !ondoor;
            anim.SetBool("isOpen", ondoor);
        }
    }
}
