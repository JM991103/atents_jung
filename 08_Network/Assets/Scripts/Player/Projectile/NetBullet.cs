using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetBullet : NetworkBehaviour
{
    public float speed = 10.0f;
    int reflectCount = 2;
    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public override void OnNetworkSpawn()
    {
        rigid.velocity = transform.forward * speed;
        StartCoroutine(SelfDespawn());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!NetworkObject.IsSpawned)   // 이미 디스폰 된다고 되어있는 상황이면 
        {
            return;                     // 이후의 코드는 실행안함.
        }
        
        GameManager.Inst.Logger.Log($"총알 : {collision.gameObject.name} 명중");

        if (collision.gameObject.CompareTag("Player"))
        {
            NetworkObject.Despawn();        // 플레이어에 맞으면 즉시 디스폰

            NetPlayer player = collision.gameObject.GetComponent<NetPlayer>();
            player.OnDie();
        }
        else if (reflectCount > 0)
        {
            // 튕길 횟수가 남아 있으면튕기기
            transform.forward = Vector3.Reflect(transform.forward, collision.GetContact(0).normal); // 반사 방향 구하기
            rigid.angularVelocity = Vector3.zero;       // 회전 운동량 제거
            rigid.velocity = transform.forward * 10.0f; // 앞쪽 방향으로 나가게 만들기

            reflectCount--;                             // 튕길 횟수 감소시키기
        }
        else
        {
            NetworkObject.Despawn();    // 튕길 횟수가 없으면 디스폰
        }
    }

    IEnumerator SelfDespawn()
    {
        yield return new WaitForSeconds(5.0f);
        NetworkObject.Despawn();
    }
}
