using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// 수직 또는 수평으로만 움직일 것. 대각선은 밀리는 현상 발생

public class Platform : MonoBehaviour
{
    public Transform destiantion;
    public float moveSpeed = 1.0f;
    public Action<Vector3> onMove;

    protected bool isMoveing = false;

    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMoveing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMoveing = false;
        }
    }

    private void FixedUpdate()
    {
        if (isMoveing)
        {
            // 이번 FixedUpdate때 움직일 벡터 구하기
            Vector3 moveDelta = moveSpeed * Time.fixedDeltaTime * (destiantion.position - rigid.position).normalized;

            // 새로운 위치 구하기
            Vector3 newPos = rigid.position + moveDelta;

            // 새로운 위치가 도착지점에 거의 근접하면
            if ((destiantion.position - newPos).sqrMagnitude < 0.001f)
            {
                // 도착했다고 처리
                isMoveing = false;
                newPos = destiantion.position;
                moveDelta = Vector3.zero;
            }

            // 위치 최종 결정
            rigid.MovePosition(newPos);

            // 델리게이트에 연결된 함수
            onMove?.Invoke(moveDelta);
        }
    }

    //Transform target;


    //public float speed = 3.0f;      // 엘레베이터의 속도
    //private bool moveStart = false;

    //private Vector3 dir;        // 방향
    //private Vector3 golaTrans;      // 도착지점 위치

    //private void Awake()
    //{
    //    target = transform.GetChild(0);

    //}

    //private void Start()
    //{
    //    dir = target.transform.position - transform.position;   //  방향 구하기
    //    dir = dir.normalized;    //dir를 정규화   
    //    golaTrans = target.transform.position;  // 시작과 동시에 golaTrans의 값을 target의 위치로 만듬

    //    target.transform.parent = null;         // 자식 지우기
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))  //플레이어가 들어가면
    //    {
    //        if (transform.position.y < target.transform.position.y)     // transform.position.y가 target.transform.position.y 작으면
    //        {
    //            moveStart = true;       // moveStart를 true로 바꾸기
    //        }
    //    }
    //}

    //private void Update()
    //{
    //    if (moveStart)  // moveStart가 true면
    //    {
    //        transform.Translate(speed * Time.deltaTime * dir); // dir의 방향으로 이동

    //        if(transform.position.y >= golaTrans.y)     // 자신의 y위치가 golaTrans y위치보다 크면
    //        {
    //            moveStart = false;      // moveStart를 false로 바꾸기
    //        }

    //    }
    //}

}
