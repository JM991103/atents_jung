using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Asteroid : MonoBehaviour
{
    public int score = 50;
    public float rotateSpeed = 360.0f;  //회전 속도
    public float moveSpeed = 1.5f;      //이동 속도

    public float minmoveSpeed = 2.0f;
    public float maxmoveSpeed = 4.0f;
    public float minrotateSpeed = 30.0f;
    public float maxrotateSpeed = 360.0f;

    float lifetime;
    public float minLifetime = 4.0f;
    public float maxLifetime = 6.0f;

    public GameObject small;
    [Range(1,16)]
    public int splitCount = 3;

    public Vector3 direction = Vector3.left;    //운석이 이동할 방향
    float X = -12.0f;
    float maxY = 6.0f;
    float minY = -6.0f;

    public int hitPoint = 3;

    GameObject explosion;
    SpriteRenderer sprite;

    Action<int> onDead;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();

    }

    // Start is called before the first frame update
    void Start()
    {
        explosion = transform.GetChild(0).gameObject;

        //if (Random.Range(0.0f, 1.0f) < 0.5f)
        //    sprite.flipX = true;
        //if (Random.Range(0.0f, 1.0f) < 0.5f)
        //    sprite.flipY = true;

        //int rand = Random.Range(0, 100) % 2;
        //sprite.flipX = (rand == 0);
        int rand = UnityEngine.Random.Range(0, 4);      //rand에다가 0(0b_00),1(0b_01),2(0b_10),3(0b_11)중 하나의 숫자를 랜덤으로 준다.

        //rand & 0b_01 : rand의 제일 오른쪽 비트가 0인지 1인지 확인하는 작업
        //((rand & 0b_01) != 0) : rand의 제일 오른쪽 비트가 1이면 true, 0이면 false
        sprite.flipX = ((rand & 0b_01) != 0);

        //rand & 0b_10 : rand의 제일 오른쪽에서 두번째 비트가 0인지 1인지 확인하는 작업
        //((rand & 0b_10) != 0) ; rand의 제일 오른쪽에서 두번째 비트가 1이면 true, 0이면 false
        sprite.flipY = ((rand & 0b_10) != 0);

        //moveSpeed = Random.Range(2.0f, 4.0f);
        //rotateSpeed = Random.Range(30.0f, 360.0f);

        moveSpeed = UnityEngine.Random.Range(minmoveSpeed, maxmoveSpeed);
        //속도 = 현재값 - 최소값 / 최대값 - 최소값
        //회전 = 속도 * (회전최대값 - 회전쇠소값) + 최소값
        float ratio = (moveSpeed - minmoveSpeed) / (maxmoveSpeed - minmoveSpeed);
        //rotateSpeed = ratio * (maxrotateSpeed - minmoveSpeed) + maxrotateSpeed;
        rotateSpeed = Mathf.Lerp(minrotateSpeed, maxrotateSpeed, ratio);
        //Debug.Log($"calc : {rotateSpeed}");
        //Debug.Log($"calc : {Mathf.Lerp(minrotateSpeed, maxrotateSpeed, ratio)}");
        //Debug.Log($"calc : {Mathf.Lerp(minrotateSpeed, maxrotateSpeed, ratio)}");

        lifetime = UnityEngine.Random.Range(minLifetime, maxLifetime);

        explosion = transform.GetChild(0).gameObject;

        StartCoroutine(selfCrush());

        Player player = FindObjectOfType<Player>();
        onDead += player.AddScore;
    }

    IEnumerator selfCrush()
    {
        yield return new WaitForSeconds(lifetime);

        crush();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation *= Quaternion.Euler(new(0, 0, 90));      // 계속 90도씩 회전
        //transform.rotation *= Quaternion.Euler(new(0, 0, rotateSpped * Time.deltaTime));        // 1초에 360도씩 회전
        transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.forward);   // forward 축을 기주능로 1초에 rotateSpeed도씩 회전

        transform.Translate(Time.deltaTime * moveSpeed * direction, Space.World);

        if (transform.position.x < X || transform.position.y > maxY || transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + direction * 1.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hitPoint--;
            if (hitPoint <= 0)
            {
                onDead?.Invoke(score);
                explosion.SetActive(true);
                explosion.transform.parent = null;
                Destroy(this.gameObject);

                crush();
            }
        }
    }

    void crush()
    {
        explosion.SetActive(true);
        explosion.transform.parent = null;

        if (UnityEngine.Random.Range(0.0f,1.0f)<0.05f)
        {
            //5%확률에 당첨되었다.
            splitCount = 20;
        }
        else
        {
            //95% 확률에 당첨되었다.
            splitCount = UnityEngine.Random.Range(3, 6);    //1/3확률로 3~5가 나온다.
        }


        float angleGap = 360.0f / (float)splitCount;    // 작은 운석들의 진행 방향의 사이각
        float r = UnityEngine.Random.Range(0.0f, 360.0f);   //첫 운석 방향 변화용
        for (int i = 0; i < splitCount; i++)
        {
            Instantiate(small, transform.position, Quaternion.Euler(0, 0, (angleGap * i) + r));
        }

        Destroy(this.gameObject);
    }
}