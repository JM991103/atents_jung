using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Object/Item Data", order = 1)]
public class itemData : ScriptableObject
{
    public uint id = 0;                     // 아이템 ID
    public string itemName = "아이템";     // 아이템의 이름
    public GameObject modelprefab;          // 아이템의 외형을 표시할 프리펩
    public uint value;                      // 
}
