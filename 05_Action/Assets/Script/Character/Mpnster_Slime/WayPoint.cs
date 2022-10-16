using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    Transform[] wayPoint;   // 자식으로 가지고 있는 wayPoint
    int index = 0;          // 현재 향하고 있는 wayPoint의 번호(index)

    public Transform currentWayPoint { get => wayPoint[index]; }    // 현재 향하고 있는 웨이 포인트 

    private void Awake()
    {
        wayPoint = new Transform[transform.childCount];     // 자식의 개수만큼 배열을 확보
        for (int i = 0; i < transform.childCount; i++)
        {
            wayPoint[i] = transform.GetChild(i);            // 모든 자식들을 저장
        }
    }

    public Transform MoveToNextWaypoint()
    {
        index++;                        // 인덱스 증감식
        index %= wayPoint.Length;       // index = index % wayPoint.Length
        return wayPoint[index];         // 인덱스를 증가시킨 후 다음 WayPoint의 Transform을 리턴
    }
}
