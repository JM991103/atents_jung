using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Bullet : MonoBehaviour
{
    float speed = 5.0f;

    private void Update()
    {
        //transform.Translate(speed * Time.deltaTime * new Vector3(1, 0));
        transform.Translate(speed * Time.deltaTime * Vector3.right, Space.Self);
        //Space.World   //씬 기준
        //space.self    //자기 기준
    }

    private void Start()
    {
        Destroy(gameObject, 5f);
    }
}
