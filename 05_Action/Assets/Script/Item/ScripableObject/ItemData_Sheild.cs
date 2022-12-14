using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shild Item Data", menuName = "Scriptable Object/Item Data - Shild", order = 6)]
public class ItemData_Sheild : ItemData_EquipItem
{
    [Header("방패 데이터")]
    public float defencePower = 15;

    // new를 상속 받은 프로퍼티가 아닌 새로 갈아치우고 이걸 사용함
    public override EquipPartType EquipPart => EquipPartType.Sheild;
}
