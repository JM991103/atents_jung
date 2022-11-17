using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Minimap : MonoBehaviour
{
    void Start()
    {
        ItemData_EquipItem item = GameManager.Inst.ItemData[ItemIDCode.SilverSword] as ItemData_EquipItem;
        GameManager.Inst.Player.EquipItem(EquipPartType.Weapon, item);
    }
}
