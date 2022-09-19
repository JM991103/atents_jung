using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platfoem : MonoBehaviour
{
    public Transform destination;
    public float moveSpeed = 3.0f;

    Rigidbody rigid;
    bool isMoving = false;

    public Action<Vector3> onMove;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();

    }

    //private void Update()
    //{
    //    if (playerIn)
    //    {
    //        transform.Translate(moveSpeed * Time.deltaTime * dir);

    //        if (transform.position.y > Target.transform.position.y)
    //        {
    //            playerIn = false;
    //        }
    //    }
    //}

    private void FixedUpdate()
    {
        if (isMoving)
        {
            // 이번 fixedUpdate때 움직일 벡터 구하기
            Vector3 moveDelta = moveSpeed * Time.deltaTime * (destination.position - rigid.position).normalized;

            // 새로운 위치 구하기
            Vector3 newPos = rigid.position + moveDelta;

            // 새로운 위치가 도착지점에 거의 근접하면
            if((destination.position - newPos).sqrMagnitude < 0.001f)
            {
                // 도착했다고 처리
                isMoving = false;
                newPos = destination.position;
                moveDelta = Vector3.zero;
            }
             // 위치 최종 결정
            rigid.MovePosition(newPos);
            onMove?.Invoke(moveDelta);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("발판을 밟았다.");
            isMoving = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isMoving = false;
        }
    }

    
}
