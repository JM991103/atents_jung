using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platfoem : MonoBehaviour
{
    GameObject Target;

    bool moveStart = false;
    public float speed = 3.0f;

    Vector3 dir;

    private void Awake()
    {
        Target = GameObject.Find("Target");
        dir = Target.transform.position - transform.position;
        dir = dir.normalized;
    }

    private void Update()
    {
        if (moveStart)
        {
            transform.Translate(speed * Time.deltaTime * dir);

            if (transform.position.y > Target.transform.position.y)
            {
                moveStart = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("발판을 밟았다.");
            moveStart = true;
        }
    }

}
