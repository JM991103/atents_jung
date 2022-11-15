using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponItem Data", menuName = "Scriptable Object/Item Data - weapon", order = 5)]
public class ItemData_Weapon : ItemData, IEquipItem
{
    [Header("아이템 기본 데이터")]
    public float AttackPower = 30;

    public EquipPartType EquipPart => EquipPartType.weapon;

    public void EqupItem(GameObject target)
    {
        
    }

    public void ToggleEquipItem(GameObject target)
    {
        
    }

    public void UnEquipItem(GameObject target)
    {
        
    }
}
