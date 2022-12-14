using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spawnPrefab;
    public GameObject ItemSpwner;
    public float SpawnTime = 5.0f;
    
    protected float minY = -4.0f;
    protected float maxY = 4.0f;

    IEnumerator enemySpawn;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawn = EnemySpawn();
        StartCoroutine(enemySpawn);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual IEnumerator EnemySpawn()
    {
        while (true)    // 무한 반복
        { 
            if (Random.Range(0.0f,1.0f) < 0.1f) 
            {
            GameObject obj1 = Instantiate(ItemSpwner, transform.position, Quaternion.identity); //기본적으로 생성하는 것
            obj1.transform.Translate(0.0f, Random.Range(minY, maxY), 0);        // 10% 이하의 확률로 ItemEnemy 적 생성
            }
            else
            {
            GameObject obj2 = Instantiate(spawnPrefab, transform.position, Quaternion.identity);
            obj2.transform.Translate(0.0f, Random.Range(minY, maxY), 0);
            }
            yield return new WaitForSeconds(SpawnTime);
        }
    }

    protected virtual void OnDrawGizmos()         // 개발용 정보를 항상 그리는 함수
    {
        //Gizmos.color = new Color(1, 0, 0);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new(1, Mathf.Abs(minY) + Mathf.Abs(maxY) + 2, 1));
    }

    //private void OnDrawGizmosSelected()     // 개발자 영역에서만 보이는 영역
    //{

    //}

}