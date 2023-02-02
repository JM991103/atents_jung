using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class Board : MonoBehaviour
{
    /// <summary>
    /// 보드의 가로 세로 길이(항상 정사각형)
    /// </summary>
    const int BoardSize = 10;

    /// <summary>
    /// 보드의 배 배치 정보. 2차원 대신 1차원으로 저장
    /// </summary>
    ShipType[] shipType;

    // static 함수들 --------------------------------------------------------


    /// <summary>
    /// 배열의 인덱스 값을 그리드 좌표로 변환해주는 static 함수
    /// </summary>
    /// <param name="index">r계산할 인덱스 값</param>
    /// <returns>반환된 그리드 좌표</returns>
    static public Vector2Int IndexToGrid(int index)
    {
        return new Vector2Int(index % BoardSize, index / BoardSize);
    }

    /// <summary>
    /// 그리드 좌표를 배열의 인덱스 값으로 변환해주는 static 함수
    /// </summary>
    /// <param name="grid">계산할 그리드 좌표</param>
    /// <returns>변환된 인덱스 값</returns>
    public static int GridToIndex(Vector2Int grid)
    {
        return grid.x + grid.y * BoardSize;
    }

    /// <summary>
    /// 그리드 좌표를 배열의 인덱스 값으로 변환해주는 static 함수
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>변환된 인덱스 값</returns>
    public static int GridToIndex(int x, int y)
    {
        return x + y * BoardSize;
    }

    public static bool IsValidPosition(Vector2Int gridPos)
    {
        return gridPos.x > -1 && gridPos.x < BoardSize && gridPos.y > -1 && gridPos.y < BoardSize;
    }


    // 일반 함수들 --------------------------------------------------------

    /// <summary>
    /// 그리드 좌표를 월드 좌표로 변환해주는 함수
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Vector3 GridToWorld(int x, int y)
    {
        return transform.position + new Vector3(x + 0.5f, 0, -(y + 0.5f));
    }

    /// <summary>
    /// 그리드 좌표를 월드 좌표로 변환해주는 함수
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    public Vector3 GridToWorld(Vector2Int grid)
    {
        return GridToWorld(grid.x, grid.y);
    }

    /// <summary>
    /// 월드 좌표를 그리드 좌표로 변환해주는 함수
    /// </summary>
    /// <param name="worldPos"></param>
    /// <returns></returns>
    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        worldPos.y = 0;

        Vector3 diff = worldPos - transform.position;

        return new Vector2Int(Mathf.FloorToInt(diff.x), Mathf.FloorToInt(-diff.z));
    }

    /// <summary>
    /// 인덱스 값을 월드좌표로 변환해주는 함수
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Vector2Int IndexToWorld(int index)
    {        
        return Vector2Int.zero;
    }

    /// <summary>
    /// 월드 좌표가 보드 안쪽인지 확인하는 함수
    /// </summary>
    /// <param name="worldPos">체크할 월드 좌표</param>
    /// <returns>보드 안쪽이면 true, 아니면 false</returns>
    public bool IsValidPosition(Vector3 worldPos)
    {
        Vector3 diff = worldPos - transform.position;
        return diff.x >= 0.0f && diff.x <= BoardSize && diff.z < 0.0f && diff.z > -BoardSize;
    }

    /// <summary>
    /// 함선 배치 함수
    /// </summary>
    /// <param name="ship">배치할 함선</param>
    /// <param name="pos">배치할 월드 좌표</param>
    /// <returns>성공하면 true, 아니면 false</returns>
    public bool ShipDeplyment(Ship ship, Vector3 pos)
    {        
        return false;
    }

    /// <summary>
    /// 함선 배치 취소 함수
    /// </summary>
    /// <param name="ship">배치를 취소할 배</param>
    public void UndoShipDeplyment(Ship ship)
    {

    }
}
