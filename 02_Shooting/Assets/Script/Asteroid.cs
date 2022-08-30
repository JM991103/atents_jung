using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float rotateSpeed = 360.0f;
    public float moveSpeed = 3.0f;
    public Vector3 direction = Vector3.left;
    float X = -12.0f;
    float maxY = 6.0f;
    float minY = -6.0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation *= Quaternion.Euler(new(0, 0, 90));      // 계속 90도씩 회전
        //transform.rotation *= Quaternion.Euler(new(0, 0, rotateSpped * Time.deltaTime));        // 1초에 360도씩 회전
        transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.forward);   // forward 축을 기주능로 1초에 rotateSpeed도씩 회전

        transform.Translate(Time.deltaTime * moveSpeed * Vector3.left, Space.World);

        if (transform.position.x < X || transform.position.y > maxY || transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position);
        Gizmos.DrawLine(new(X - 1, minY, 1), new(X - 1, maxY, 1));
    }
}