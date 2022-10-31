using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 1개를 표현할 클래스
/// </summary>
public class Item : MonoBehaviour
{
    // 몬스터가 죽으면 아이템이 나타난다
    public itemData Data;   // 아이템의 정보


    private void Start()
    {

        Instantiate(Data.modelprefab, transform.position, transform.rotation, transform);       // 아이템의 외형 추가

    }
    // 플레이어가 아이템 근처에서 획득 버튼을 누르면 플레이어가 아이템을 습득한다.


}
