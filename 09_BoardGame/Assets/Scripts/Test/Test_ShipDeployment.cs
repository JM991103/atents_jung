using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_ShipDeployment : TestBase
{
    Board board;
    Ship ship;

    private void Start()
    {
        board = FindObjectOfType<Board>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        inputActions.Test.TestClick.performed += OnClick;
    }

    protected override void OnDisable()
    {
        inputActions.Test.TestClick.performed -= OnClick;
        base.OnDisable();
    }

    private void OnClick(InputAction.CallbackContext _)
    {
        Vector2 screen = Mouse.current.position.ReadValue();
        Vector3 world = Camera.main.ScreenToWorldPoint(screen);
        Vector2Int grid = board.WorldToGrid(world);
        ////Debug.Log($"클릭 : {grid.x}, {grid.y}");
        //Vector3 Gtow = board.GridToWorld(grid);
        ////Debug.Log($"클릭 : {Gtow.x}, {Gtow.y}");

        //ship = ShipManager.Inst.MakeShip(ShipType.Carrier, this.transform);
        //ship.gameObject.SetActive(true);
        //ship.transform.position = Gtow;        
        //Debug.Log( board.IsValidPosition(world));
        Debug.Log(Board.IsValidPosition(grid));
    }
}
