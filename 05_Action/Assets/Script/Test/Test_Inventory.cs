using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Inventory : TestBase
{
    Inventory inven;

    private void Start()
    {
        inven = new Inventory();
    }

    protected override void Test1(InputAction.CallbackContext _)
    {
        inven.PrintInventory();
    }

    protected override void Test2(InputAction.CallbackContext _)
    {
        inven.AddItem(ItemIDCode.Ruby);
        inven.AddItem(ItemIDCode.Emerald);
        inven.AddItem(ItemIDCode.Sapphire);

        inven.AddItem(ItemIDCode.Emerald);
        inven.AddItem(ItemIDCode.Ruby);

        inven.PrintInventory();
    }

    protected override void Test3(InputAction.CallbackContext _)
    {
        //inven.ClearItem(1);
        //inven.ClearItem(3);
        //inven.ClearItem(15);
        inven.MoveItem(0, 9);
        inven.PrintInventory();
        inven.MoveItem(9, 15);
        inven.PrintInventory();

        inven.MoveItem(1, 2);
        inven.PrintInventory();

        inven.MoveItem(5, 6);
        inven.PrintInventory();
        inven.MoveItem(5, 1);
        inven.PrintInventory();

        inven.AddItem(ItemIDCode.Sapphire, 0);
        inven.MoveItem(0, 1);
    }
    protected override void Test4(InputAction.CallbackContext _)
    {
        //inven.RemoveItem(0);
        //inven.RemoveItem(1, 3);

        inven.AddItem(ItemIDCode.Ruby);
        inven.AddItem(ItemIDCode.Ruby);
        inven.AddItem(ItemIDCode.Ruby);
        inven.AddItem(ItemIDCode.Ruby);
        inven.AddItem(ItemIDCode.Emerald);
        inven.AddItem(ItemIDCode.Emerald);
        inven.AddItem(ItemIDCode.Sapphire);

        inven.PrintInventory();

        inven.AddItem(ItemIDCode.Ruby, 5);
        inven.AddItem(ItemIDCode.Ruby, 5);

        inven.PrintInventory();

        inven.MoveItem(0, 5);
        inven.PrintInventory();
    }
    protected override void Test5(InputAction.CallbackContext _)
    {
        inven.AddItem(ItemIDCode.Ruby, 9);
        inven.AddItem(ItemIDCode.Emerald, 8);
        inven.AddItem(ItemIDCode.Sapphire, 20);
    }
}
