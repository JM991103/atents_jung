using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SceneMonsterManager : MonoBehaviour
{
    GridMap gridMap;
    Tilemap background;
    Tilemap obstacle;

    Spawner[] spawners;

    public GridMap GridMap => gridMap;

    private void Awake()
    {
        Transform grid = transform.parent;
        Transform child = grid.GetChild(0);
        background = child.GetComponent<Tilemap>();         // 타일맵 가져오기
        child = grid.GetChild(1);
        obstacle = child.GetComponent<Tilemap>();

        gridMap = new GridMap(background, obstacle);        // 그리드 맵 만들기

        spawners = GetComponentsInChildren<Spawner>();      // 자식으로 있는 스포너 가져오기
    }

    /// <summary>
    /// 스포너의 스폰 영역 중에서 벽이 아닌 노드들만 찾아서 돌려주는 함수
    /// </summary>
    /// <param name="spawner">계산할 스포너</param>
    /// <returns>스포너의 스폰 영역에 있는 벽이 아닌 노드들</returns>
    public List<Node> CalcSpawnArea(Spawner spawner)
    {
        List<Node> nodes = new List<Node>();

        Vector2Int min = gridMap.WorldToGrid(spawner.transform.position);                  // 그리드 좌표의 최소 값 계산
        Vector2Int max = gridMap.WorldToGrid(spawner.transform.position + (Vector3)spawner.size);  // 그리드 좌표의 최대 값 계산
        for (int y = min.y; y < max.y; y++)
        {
            for (int x = min.x; x < max.x; x++)
            {
                if (gridMap.IsSpawnable(x, y))                      // 스폰 가능한 위치면
                {
                    nodes.Add(gridMap.GetNode(x, y));                          // 기록해 놓기
                }
            }
        }
        return nodes;
    }
}
