using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    public float speed = 6.0f;
    float killtime = 3.0f;

    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rigid.velocity = transform.forward * speed;
        
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
        Destroy(this.gameObject, killtime);
    }
}
