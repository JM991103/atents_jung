using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridPainter : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject letterPrefab;

    const int gridLineCount = 11;

    private void Awake()
    {
        DrawGridLine();
        DrawGridLetter();
    }

    void DrawGridLine()
    {
        // 세로 선 그리기(세로 선을 옆으로 반복해서 그리기)
        for (int i = 0; i < gridLineCount; i++)
        {
            GameObject line = Instantiate(linePrefab, transform);
            LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, new Vector3(i, 0, 1));
            lineRenderer.SetPosition(1, new Vector3(i, 0, 1 - gridLineCount));
        }

        // 가로 선 그리기(가로선을 아래로 반복해서 그리기)
        for (int i = 0; i < gridLineCount; i++)
        {
            GameObject line = Instantiate(linePrefab, transform);
            LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, new Vector3(-1, 0, -i));
            lineRenderer.SetPosition(1, new Vector3(-1 + gridLineCount, 0, -i));
        }
    }

    void DrawGridLetter()
    {
        // 알파벳을 가로로 쓰기
        for (int i = 1; i < gridLineCount; i++)
        {
            GameObject letter = Instantiate(letterPrefab, transform);
            letter.transform.position = new Vector3(i - 0.5f, 0, 0.5f);
            TextMeshPro text = letter.GetComponent<TextMeshPro>();
            char alphbet = (char)(64 + i);
            text.text = alphbet.ToString();
        }

        // 숫자를 세로로 쓰기
        for (int i = 0; i < gridLineCount; i++)
        {
            GameObject letter = Instantiate(letterPrefab, transform);
            letter.transform.position = new Vector3(-0.5f, 0, 0.5f - i);
            TextMeshPro text = letter.GetComponent<TextMeshPro>();
            text.text = i.ToString();

            if (i > 9)
            {
                text.fontSize = 8;
            }
        }
    }
}
