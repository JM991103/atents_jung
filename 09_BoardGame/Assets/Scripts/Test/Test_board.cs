using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_board : TestBase
{
    Board board;

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
        Debug.Log(world);
        Vector2Int grid = board.WorldToGrid(world);
        Debug.Log($"클릭 : {grid.x}, {grid.y}");

        Vector3 Gtow = board.GridToWorld(grid);
        Debug.Log($"클릭 : {Gtow.x}, {Gtow.y}");
    }

    protected override void Test1(InputAction.CallbackContext _)
    {
        if(Board.GridToIndex(1, 1) == 11)
        {
            Debug.Log("GridToIndex : 정상");
        }
        else
        {
            Debug.LogError("GridToIndex : 비정상");
        }
        
        if(Board.IndexToGrid(11) == new Vector2Int(1,1)
           && Board.IndexToGrid(21) == new Vector2Int(1, 2)
           && Board.IndexToGrid(90) == new Vector2Int(0, 9))
        {
            Debug.Log("IndexToGrid : 정상");
        }
        else
        {
            Debug.LogError("IndexToGrid : 비정상");
        }

        if (board.WorldToGrid(board.transform.position + new Vector3(3.5f, 0, -2.1f)) == new Vector2Int(3, 2)
            && board.WorldToGrid(board.transform.position + new Vector3(1.5f, 0, -7.1f)) == new Vector2Int(1, 7)
            && board.WorldToGrid(board.transform.position + new Vector3(5.9f, 0, -3.1f)) == new Vector2Int(5, 3))
        {
            Debug.Log("WorldToGrid : 정상");
        }
        else
        {
            Debug.LogError("WorldToGrid : 비정상");
        }

        if (board.GridToWorld(0, 0) == new Vector3(board.transform.position.x + 0.5f, board.transform.position.y, board.transform.position.z - 0.5f)
            && board.GridToWorld(1, 1) == new Vector3(board.transform.position.x + 0.5f + 1, board.transform.position.y, board.transform.position.z - 0.5f - 1)
            && board.GridToWorld(5, 3) == new Vector3(board.transform.position.x + 0.5f + 5, board.transform.position.y, board.transform.position.z - 0.5f - 3))
        {
            Debug.Log("GridToWorld : 정상");
        }
        else
        {
            Debug.LogError("GridToWorld : 비정상");
        } 
    }

}
