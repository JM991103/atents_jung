using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy_s : MonoBehaviour
{
    public int score = 10;
    float speed = 3.0f;
    Vector3 dis = new Vector3(-10, 0, 0);
    GameObject explosion;
    Action<int> onDead;

    float spawnY;       // 생성 되었을 때의 기준 높이
    float timeElapsed;  // 게임 시작부터 얼마나 시간이 지났나를 기록해 놓는 변수

    public float amplitude = 1.0f;    // 사인으로 변경되는 위아래 차이. 원래 sin은 -1 ~ +1인데 그것을 변경하는 변수
    public float frequencyTime = 1.0f; // 사인 그래프가 한번 도는데 걸리는 시간(원래는 2pi)

    // Start is called before the first frame update
    void Start()
    {
        explosion = transform.GetChild(0).gameObject;
        spawnY = transform.position.y;
        timeElapsed = 0.0f;
        //explosion.SetActive(false); //활성화 상태를 끄기(비활성화)

        Player player = FindObjectOfType<Player>();
        onDead += player.AddScore;
    }

    // Update is called once per frame
    void Update()
    {
        //Time.daltaTime : 이전 프레임에서 현재 프레임까지의 시간
        timeElapsed += Time.deltaTime * frequencyTime;
        float newY = spawnY + Mathf.Sin(timeElapsed) * amplitude;
        // Mathf.Sin의 결과는 0에서 시작해서 +1까지 증가하다가 -1까지 감소, 다시 +1까지 증가 (반복)
        float newX = transform.position.x - speed * Time.deltaTime;

        transform.position = new Vector3(newX, newY, 0.0f);

        //transform.Translate(Time.deltaTime * speed * Vector3.left, Space.Self);
        //if (transform.position.x < dis.x)
        //{
        //    Destroy(gameObject);
        //}
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            onDead?.Invoke(score);
            //GameObject obj = Instantiate(explosion,transform.position, Quaternion.identity);
            //Destroy(obj, 0.42f);
            explosion.SetActive(true);  //총알에 맞았을 때 익스폴로젼을 활성화 시키고
            explosion.transform.parent = null;  //익스플로젼은 부모(Enemy) 연결을 제거한다.

            Destroy(this.gameObject);   //Enemy를 파괴한다.
        }
    }
}