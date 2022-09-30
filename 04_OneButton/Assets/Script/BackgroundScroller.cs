using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollingspeed = 5.0f;
    public float widht = 7.2f;  // 하늘은 7.2, 바닥은 8.4

    Transform[] bgSlots;
    float edgPoint;

    private void Awake()
    {
        //bgSlots를 배경의 트랜스폼으로 채우기
        bgSlots = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            bgSlots[i] = transform.GetChild(i);
        }
    }

    private void Start()
    {
        edgPoint = transform.position.x - widht * 2.0f;
    }

    private void Update()
    {
        
        foreach (var slot in bgSlots)
        {
            slot.Translate(scrollingspeed * Time.deltaTime * -transform.right);
            if (slot.position.x < edgPoint)
            {
                slot.Translate(widht * bgSlots.Length * transform.right);
            }
        }
    }

}
