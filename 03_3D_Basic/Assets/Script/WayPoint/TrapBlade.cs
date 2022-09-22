using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBlade : TrapBase
{
    public WayPoint waypoint;   // 따라다닐 웨이포인트들이 가지고 있는 클래스
    public float moveSpeed = 1.0f;  // 칼날 이동 속도
    public float spinSpeed = 720.0f;

    Transform target;           //목표로하는 웨이포인트의 트랜스폼
    Transform bladeObj;
    Rigidbody rigid;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        bladeObj = GetComponent<Transform>();
    }

    private void Start()
    {
        SetTarget(waypoint.cuurentWaypoint);    // 첫 웨이포인트 지정
    }

    private void Update()
    {
        bladeObj.Rotate(spinSpeed * Time.deltaTime, 0, 0);
    }

    private void FixedUpdate()
    {
        transform.LookAt(target);       // 항상 목적지를 바라보도록

        // 이번 FixedUpdate때 움직일 벡터 구하기
        Vector3 moveDelta = moveSpeed * Time.fixedDeltaTime * transform.forward;    //이번에 움직일 정도 계산

        Vector3 newPos = rigid.position + moveDelta;    // 새로운 위치 구하기   

        rigid.MovePosition(newPos);     // 새 위치로 이동

        // 새로운 위치가 도착지점에 거의 근접하면
        if ((target.position - newPos).sqrMagnitude < 0.025f)
        {
            target = waypoint.MoveToNextWayPoint(); // 다음 웨이포인트로 목적지 설정
        }

        rigid.MovePosition(rigid.position + moveSpeed * Time.fixedDeltaTime * transform.forward);
    }

    /// <summary>
    /// 다음 목적지 지정하는 함수
    /// </summary>
    /// <param name="target">새 웨이포인트 트랜스폼</param>
    void SetTarget(Transform target)
    {
        this.target = target;        //목적지 정하고
        transform.LookAt(target);    //그쪽을 바라보게 만들기
    }


    private void OnTriggerEnter(Collider other)
    {
        IDead iDead = other.GetComponent<IDead>();
        if (iDead != null)
        {
            iDead.Die();
        }
    }
}
