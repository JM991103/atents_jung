using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy_s : MonoBehaviour
{
    float speed = 3.0f;
    Vector3 dis = new Vector3(-10, 0, 0);
    GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        explosion = transform.GetChild(0).gameObject;
        //explosion.SetActive(false); //활성화 상태를 끄기(비활성화)
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.left, Space.Self);
        //if (transform.position.x < dis.x)
        //{
        //    Destroy(gameObject);
        //}
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            //GameObject obj = Instantiate(explosion,transform.position, Quaternion.identity);
            //Destroy(obj, 0.42f);
            explosion.SetActive(true);  //총알에 맞았을 때 익스폴로젼을 활성화 시키고
            explosion.transform.parent = null;  //익스플로젼은 부모(Enemy) 연결을 제거한다.

            Destroy(this.gameObject);   //Enemy를 파괴한다.
        }
    }
}