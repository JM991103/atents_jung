using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item Data", menuName = "Scriptable Object/Item Data - Weapon", order = 5)]
public class ItemData_Weapon : ItemData_EquipItem
{
    [Header("무기 데이터")]
    public float attackPower = 30;

    // new를 사용하면 새로 갈아치우고 이걸 사용함
    public new EquipPartType EquipPart => EquipPartType.Weapon;
}
