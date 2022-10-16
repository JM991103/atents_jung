using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public WayPoint wayPoint;
    public float moveSpeed = 5.0f;

    Rigidbody rigid;
    Animator anim;

    Transform target;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        SetTarget(wayPoint.currentWayPoint);
    }

    private void FixedUpdate()
    {
        transform.LookAt(target);

        Vector3 moveDelta = moveSpeed * Time.fixedDeltaTime * transform.forward;
        Vector3 newpos = rigid.position + moveDelta;
        rigid.MovePosition(newpos);

        if ((target.position - newpos).sqrMagnitude < 0.0025f)
        {
            SetTarget(wayPoint.MoveToNextWaypoint());
        }
    }

    void SetTarget(Transform target)
    {
        this.target = target;       // 목적지 정하고
        transform.LookAt(target);   // 그쪽을 바라보게 만들기
    }

}
