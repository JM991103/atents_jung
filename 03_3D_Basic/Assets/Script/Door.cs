using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 문 클래스
/// </summary>
public class Door : MonoBehaviour, IUseavleObject
{
    /// <summary>
    /// 애니메이터 컴포넌트(문 열고 닫는 애니메이션 처리용)
    /// </summary>
    protected Animator anim;

    bool playerIn = false;
    bool ondoor = true;

    /// <summary>
    /// Awake 필요한 컴포넌트 찾기
    /// </summary>
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("문 열림");
            playerIn = true;

            
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("문 닫힘");
            playerIn = false;

            
        }

    }

    public void Use()
    {
        if (playerIn)
        {
            anim.SetBool("IsOpen", ondoor);
            ondoor = !ondoor;
        }
    }

   
}
