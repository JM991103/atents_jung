using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    // ItemSlotUI가 있는 프리펩. 인벤토리 크기 변화에 대비해서 가지고 있기.
    public GameObject slotPrefab;

    Inventory inven;

    /// <summary>
    /// 입력받은 인벤토리에 맞게 각종 초기화 직업을 하는 함수 
    /// </summary>
    /// <param name="playerInven">이 UI로 표시할 인벤토리</param>
    public void InitializeInventory(Inventory playerInven)
    {

        inven = playerInven;

        if (Inventory.Default_Inventory_Size != inven.SlotCount)
        {
            // 기본 사이즈와 다르면 기준 슬롯을 전부 삭제하고 새로 만들기
            Transform slotParent = transform.GetChild(0);
        }
        else
        {
            // 크기가 같으면 슬롯UI들의 초기화 진행
        }
    }
}
