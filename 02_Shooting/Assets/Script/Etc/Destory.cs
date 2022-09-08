using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destory : MonoBehaviour
{
    Vector2 Max = new Vector2(10, 6);
    Vector2 Min = new Vector2(-10, -6);
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > Max.x || transform.position.x < Min.x || transform.position.y > Max.y || transform.position.y < Min.y)
        {
            Destroy(gameObject);
        }
    }
}
