using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float rotateSpeed = 360.0f;  //회전 속도
    public float moveSpeed = 1.5f;      //이동 속도

    public float minmoveSpeed = 2.0f;
    public float maxmoveSpeed = 4.0f;
    public float minrotateSpeed = 30.0f;
    public float maxrotateSpeed = 360.0f;

    public Vector3 direction = Vector3.left;    //운석이 이동할 방향
    float X = -12.0f;
    float maxY = 6.0f;
    float minY = -6.0f;

    public int hitPoint = 3;

    GameObject explosion;
    SpriteRenderer sprite;

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
        int rand = Random.Range(0, 4);      //rand에다가 0(0b_00),1(0b_01),2(0b_10),3(0b_11)중 하나의 숫자를 랜덤으로 준다.

        //rand & 0b_01 : rand의 제일 오른쪽 비트가 0인지 1인지 확인하는 작업
        //((rand & 0b_01) != 0) : rand의 제일 오른쪽 비트가 1이면 true, 0이면 false
        sprite.flipX = ((rand & 0b_01) != 0);

        //rand & 0b_10 : rand의 제일 오른쪽에서 두번째 비트가 0인지 1인지 확인하는 작업
        //((rand & 0b_10) != 0) ; rand의 제일 오른쪽에서 두번째 비트가 1이면 true, 0이면 false
        sprite.flipY = ((rand & 0b_10) != 0);

        //moveSpeed = Random.Range(2.0f, 4.0f);
        //rotateSpeed = Random.Range(30.0f, 360.0f);

        moveSpeed = Random.Range(minmoveSpeed, maxmoveSpeed);
        float ratio = (moveSpeed - minmoveSpeed) / (maxmoveSpeed - minmoveSpeed);
        rotateSpeed = ratio * (maxrotateSpeed - minmoveSpeed) + maxrotateSpeed;

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
            explosion.SetActive(true);
            explosion.transform.parent = null;

            Destroy(this.gameObject);
            }
        }
    }
}