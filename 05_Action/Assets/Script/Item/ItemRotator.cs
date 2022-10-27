using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotator : MonoBehaviour
{
    public float rotateSpeed = 180.0f;       // 오브젝트의 회전 속도
    public float minHeight = 0.5f;         // 오브젝트의 가장 낮은 높이
    public float maxHeight = 1.5f;         // 오브젝트의 가장 높은 높이

    float timeElapsed = 0.0f;
    float halfDiff;
    Vector3 newPosition;

    private void Start()
    {
        newPosition = transform.position;
        newPosition.y = minHeight;
        transform.position = newPosition;

        timeElapsed = 0.0f;
        halfDiff = 0.5f * (maxHeight - minHeight);
    }

    public void Update()
    {
        timeElapsed += Time.deltaTime;
        newPosition.y = (minHeight + 1 - Mathf.Cos(timeElapsed) * halfDiff);

        transform.position = new Vector3 (transform.position.x, newPosition.y, transform.position.z);

        //transform.Rotate(Vector3.up,rotateSpeed * Time.deltaTime);
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

}
