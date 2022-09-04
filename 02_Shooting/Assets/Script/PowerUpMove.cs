using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMove : MonoBehaviour
{
    float speed = 4.0f;
    float cooldown = 0.0f;
    Vector3 Move;

    private void Start()
    {
        Move = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0).normalized;       

        Destroy(this.gameObject, 10.0f);
    }

    private void Update()
    {
        cooldown += Time.deltaTime;
        if (cooldown >= 1.0f)
        {
            Move = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0).normalized;

            cooldown = 0.0f;
        }
        transform.Translate(speed * Time.deltaTime * Move);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Border"))
        {
            Move.y = -Move.y;
            Move.x = -Move.x;

        }
    }
}

