using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_LockOn : MonoBehaviour
{
    private void Start()
    {
        ItemData_EquipItem item = GameManager.Inst.ItemData[ItemIDCode.SilverSword] as ItemData_EquipItem;
        //GameManager.Inst.Player.EquipItem(EquipPartType.Weapon, item);
        GameManager.Inst.Player.Test_AddItem(item);
        GameManager.Inst.Player.Test_UseItem(0);
    }
}
