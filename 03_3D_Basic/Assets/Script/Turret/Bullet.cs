using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    public float speed = 6.0f;
    float killtime = 5.0f;

    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(this.gameObject, killtime);
    }

    private void FixedUpdate()
    {
        rigid.MovePosition(transform.position + speed * Time.fixedDeltaTime * transform.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDead deadtarget = collision.gameObject.GetComponent<IDead>();
            if (deadtarget != null)
            {
                deadtarget.Die();   // 죽이는 함수 호출
            }
        }
    }
}
