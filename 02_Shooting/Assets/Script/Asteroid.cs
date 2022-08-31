using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float rotateSpeed = 360.0f;  //회전 속도
    public float moveSpeed = 1.5f;      //이동 속도
    public Vector3 direction = Vector3.left;    //운석이 이동할 방향
    float X = -12.0f;
    float maxY = 6.0f;
    float minY = -6.0f;

    public int hitPoint = 3;

    GameObject explosion;


    // Start is called before the first frame update
    void Start()
    {
        explosion = transform.GetChild(0).gameObject;
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