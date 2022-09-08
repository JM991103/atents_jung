using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy를 상속받은 파워업 아이템 드랍용 적 비행기
/// </summary>
public class ItemEnemy : Enemy_s
{
    public GameObject ItemEn;
    Action<int> onDead;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //
        if (collision.gameObject.CompareTag("Bullet"))
        {
            onDead?.Invoke(score);
            Instantiate(ItemEn, transform.position, Quaternion.Euler(0,0,90.0f));
        }
    }

}
