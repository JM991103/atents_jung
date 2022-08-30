using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class AsterroidSpawner : EnemySpawner
{ 
    private Transform destination;

    private void Awake()
    {
        // 오브젝트가 생성된 직후 실행 => 이 오브젝트 안에 있는 것들을 초기화 할 때 사용
        //      이 오브젝트안에 있는 모든 컴포넌트가 생성이 완료되었다.
        //      그리고 이 오브젝트의 자식 오브젝트들도 모두 생성이 완료되었다.
        //transform.Find("DestinationArea");      // "DestinationArea" 라는 이름을 가진 자식 찾기
        //destination = transform.GetChild(0);    // 첫번째 자식 찾기

    }
    //private void Start()
    //{
    //    // 첫번째 업데이트 실행 직전 호출 
    //    // 나와 다른 오브젝트를 가져와야 할 때 사용
    //}

    protected override IEnumerator EnemySpawn()
    {
        while (true)
        {
            GameObject obj = Instantiate(spawnPrefab, transform.position, Quaternion.identity);   // 생성하고 부모를 이 오브젝트로 설정
            obj.transform.Translate(0, Random.Range(minY, maxY), 0);        // 스폰 생성 범위 안에서 랜덤으로 높이 정하기

            Vector3 destPosition = destination.position + new Vector3(0.0f, Random.Range(minY, maxY), 0.0f);    //목적지 위치 결정

            Asteroid asteroid = obj.GetComponent<Asteroid>();
            if (asteroid != null)
            {
                // 운석이 destPosition로 가는 방향벡터를 구하고
                // destPosition을 방향벡터로 만들어 준다.
                asteroid.direction = (destPosition - asteroid.transform.position).normalized; // 문제가 있다.
            }

            yield return new WaitForSeconds(SpawnTime);     // SpawnTime 만큼 대기
        }
    }



    protected override void OnDrawGizmos()         // 개발용 정보를 항상 그리는 함수
    {
        //Gizmos.color = new Color(1, 0, 0);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new(1, Mathf.Abs(minY) + Mathf.Abs(maxY) + 2, 1));
                
        if (destination != null)
        {
            destination = transform.GetChild(0);
        }
        Gizmos.DrawWireCube(destination.position, new(1, Mathf.Abs(minY) + Mathf.Abs(maxY) + 2, 1));
    }

    //private void OnDrawGizmosSelected()     // 개발자 영역에서만 보이는 영역
    //{

    //}

}