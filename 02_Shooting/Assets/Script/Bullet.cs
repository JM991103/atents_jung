using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 13.0f;
    public GameObject hitEffect;

    private void Start()
    {
        
    }

    private void Update()
    {
        //transform.Translate(speed * Time.deltaTime * new Vector3(1, 0));
        transform.Translate(speed * Time.deltaTime * Vector3.right, Space.Self);
        //Space.World   //씬 기준
        //space.self    //자기 기준
    }
    //충돌한 대상이 컬라이더일 때 실행
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($"collision : {collision.gameObject.name}");

        if (collision.gameObject.CompareTag("Enemy"))
        {
            hitEffect.transform.parent = null;
            hitEffect.transform.position = collision.contacts[0].point;
            //collision.contacts[0].normal : 법선 벡터 (노멀 벡터)
            //노멀 벡터 : 특정 평면에 수직인 벡터
            //노멀 벡터는 반사를 계산하기 위해 반드시 필요. => 반사를 가지고 그림자를 계산한다. 물리적인 반사도 계산한다.
            //노멀 벡터를 구하기 위해 벡터의 외적을 사용한다.
            hitEffect.gameObject.SetActive(true);
            Destroy(this.gameObject);
        //Destroy(this.gameObject);
        }
        //매우 좋지 못한 코드
        //1.CompareTag는 숫자와 숫자를 비교하지만 ==은 문자열 비교다.
        //2. 필요없는 가비지가 생긴다.
        //if (collision.gameObject.tag == "Enemy")
        //{
        //}
    }

    //충돌한 대상이 트리거일 때 실행
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        Debug.Log($"trigger : {collision.gameObject.name}");
//    }
}
