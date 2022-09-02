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
        
    }

    private void Update()
    {
        cooldown += Time.deltaTime;
        if (cooldown >= 1.0f)
        {
        Move = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0).normalized;

        cooldown = 0.0f;
        }
        transform.Translate(Move * speed * Time.deltaTime);
    }


}

