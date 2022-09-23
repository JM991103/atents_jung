using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // 1. 플레이어가 일정 변경안에 들어오면 해당방향으로 총구를 돌린다.
    // 2. trunSpeed 아무런 영향을 주지 않는다. (총구가 즉시 회전한다.)


    public float turnSpeed = 2.0f;
    public float sightRadius = 5.0f;

    Transform target = null;
    Transform barrelBody;

    float currentAngle = 0.0f;
    float targetAngle = 0.0f;
    Vector3 initialForward;

    private void Awake()
    {
        barrelBody = transform.GetChild(2);
    }

    private void Start()
    {
        initialForward = transform.forward;
        SphereCollider coll = GetComponent<SphereCollider>();
        coll.radius = sightRadius;
    }

    /// <summary>
    /// 인스펙터 창에서 값이 성공적으로 변경되었을 때 호출되는 함수
    /// </summary>
    private void OnValidate() 
    {
        SphereCollider coll = GetComponent<SphereCollider>();
        if (coll != null)
        {
            coll.radius = sightRadius;
        }
    }

    private void Update()
    {
        if (target != null)
        {
            // 보간을 사용한 경우 (감속하며 회전)
            //// 총구를 플레어쪽으로 돌려야 함
            //Vector3 dir = target.position - barrelBody.position;    // 총구를 플레이어쪽으로 돌리기 위해 총구에서 플레이어의 위치로 가는 방향벡터를 계산
            //dir.y = 0;      // 방향 벡터에서 y축의 영향을 제거 => x, z 평면상의 방향만 남음

            ////trunSpeed초에 걸쳐서 0 -> 1로 변경된다. (시작점에서 도착점까지 걸리는 시간이 trunSpeed초)
            //dir = Vector3.Lerp(barrelBody.forward, dir, turnSpeed * Time.deltaTime);    

            //barrelBody.rotation = Quaternion.LookRotation(dir);     // 최종적인 방향을 바라보는 회전을 만들어서 총몸에 적용 

            ////barrelBody.LookAt(target);  // 타켓을 바라봄 (target의 피봇을 바라봄)


            // 각도를 사용하는 경우
            Vector3 dir = target.position - barrelBody.position;    // 총구에서 플레이어의 위치로 가는 방향 벡터 계산
            dir.y = 0;

            targetAngle = Vector3.SignedAngle(initialForward, dir, barrelBody.up);

            if (currentAngle < targetAngle)
            {
                currentAngle += (turnSpeed * Time.deltaTime);
                currentAngle = Mathf.Min(currentAngle, targetAngle);
            }
            else if(currentAngle > targetAngle)
            {
                currentAngle -= (turnSpeed * Time.deltaTime);
                currentAngle = Mathf.Max(currentAngle, targetAngle);
            }

            Vector3 targetDir = Quaternion.Euler(0, currentAngle, 0) * initialForward;
            barrelBody.rotation = Quaternion.LookRotation(targetDir);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = null;
        }
    }
}
