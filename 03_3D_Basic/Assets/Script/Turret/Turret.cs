using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // 1. 플레이어가 일정 변경안에 들어오면 해당방향으로 총구를 돌린다.
    // 2. trunSpeed 아무런 영향을 주지 않는다. (총구가 즉시 회전한다.)

    public GameObject Bullet;

    public float turnSpeed = 2.0f;
    public float sightRadius = 5.0f;
    public float MaxDistance = 5.0f;
    public float fireAngle = 10.0f;

    float currentAngle = 0.0f;
    //float targetAngle = 0.0f;

    bool targetIn = false;
    bool targetOn = false;
    bool isFiring = false;

    Vector3 initialForward;
    Vector3 barrelToPlayerDir;

    RaycastHit hit;
    IEnumerator Bulletfire;

    Transform target = null;
    Transform barrelBody;
    Transform fireTransform;

    private void Awake()
    {
        barrelBody = transform.GetChild(2);
        fireTransform = barrelBody.GetChild(2);

        Bulletfire = BulletFire();
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
        LookTarget();

    }

    bool IsFireAngle()
    {
        //target;
        //transform.position;
        //fireAngle;

        Vector3 dir = target.position - barrelBody.position;
        dir.y = 0.0f;
        return Vector3.Angle(barrelBody.forward, dir) < fireAngle;
    }

    //private void Fire()
    //{
    //    if (targetIn)
    //    {

    //        if (Physics.Raycast(barrelBody.position, barrelBody.forward, out hit, MaxDistance))
    //        {
    //            if (targetOn)
    //            {
    //                StartCoroutine(Bulletfire);
    //                Debug.Log("들어옴");
    //                targetOn = false;
    //            }
    //        }
    //        else
    //        {
    //            if (!targetOn)
    //            {
    //                StopCoroutine(Bulletfire);
    //                Debug.Log("나감");
    //                targetOn = true;
    //            }
    //        }
    //    }
    //}

    void LookTarget()
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


            // 각도를 사용하는 경우(등속도로 회전)
            barrelToPlayerDir = target.position - barrelBody.position;    // 총구에서 플레이어의 위치로 가는 방향 벡터 계산
            barrelToPlayerDir.y = 0;

            // 정방향일 때 0 ~ 180도, 역방향 일때 0~-180
            float betweenAngle = Vector3.SignedAngle(barrelBody.forward, barrelToPlayerDir, barrelBody.up);

            Vector3 resultDir;

            if (Mathf.Abs(betweenAngle) < 1.0f )    // 사이각이 일정 각도 이하인지 체크
            {
                float rotateDirection = 1.0f;   //일단 +방향(시계방향)으로 설정
                if (betweenAngle < 0)
                {
                    rotateDirection = -1.0f;    // betweeAngle이 -면 rotateDirection도 -1로
                }

                //초당 turnSpeed만큼 회전하는데 rotateDirection로 시계방향으로 회전할지 반시계 방향으로 회전할지 결정
                currentAngle += (rotateDirection * turnSpeed * Time.deltaTime);

                resultDir = Quaternion.Euler(0, currentAngle, 0) * initialForward;

            }
            else
            {
                // 사이각이 0에 가까울 경우
                resultDir = barrelToPlayerDir;
            }

            barrelBody.rotation = Quaternion.LookRotation(resultDir);

            if (!isFiring && IsFireAngle())
            {
                FireStart();
            }
            if (isFiring && !IsFireAngle())
            {
                FireStop();
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetIn = true;
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetIn = false;
            target = null;
            FireStop();
        }
    }
    
    IEnumerator BulletFire()
    {
        while (true)
        {
            Fire();

            yield return new WaitForSeconds(1.5f);
        }

    }

    void Fire()
    {
        Instantiate(Bullet, fireTransform.position, fireTransform.rotation);
    }

    //IEnumerator BulletFire()
    //{
    //    while (true)
    //    {
    //        GameObject Obj = Instantiate(Bullet, barrelBody.position, Quaternion.LookRotation(dir));
    //        Obj.transform.Translate(0,0,1.5f);

    //        yield return new WaitForSeconds(1.5f);
    //    }
    //}

    void FireStart()
    {
        StartCoroutine(Bulletfire);
        isFiring = true;
    }

    void FireStop()
    {
        StopCoroutine(Bulletfire);
        isFiring = false;
    }

}
