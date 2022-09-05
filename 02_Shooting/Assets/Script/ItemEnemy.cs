using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy를 상속받은 파워업 아이템 드랍용 적 비행기
/// </summary>
public class ItemEnemy : MonoBehaviour
{
    public GameObject ItemEn;   // Item

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Instantiate(ItemEn, transform.position, Quaternion.Euler(0,0,90.0f));
        }
    }

}
