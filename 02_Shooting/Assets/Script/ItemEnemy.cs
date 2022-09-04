using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEnemy : MonoBehaviour
{
    public GameObject ItemEn;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Instantiate(ItemEn, transform.position, Quaternion.Euler(0,0,90.0f));
        }
    }

}
