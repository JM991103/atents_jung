using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    float timeScore;
    int m = 0;
    int s = 0;

    bool stopScore = false;


    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        Goal gool = FindObjectOfType<Goal>();
        gool.start += Finish;
    }

    private void Update()
    {
        if (stopScore)
        {
            timeScore += Time.deltaTime;
            s = (int)timeScore % 60;
            m = (int)timeScore / 60;

            textMesh.text = $"{m} : {s}";

        }

    }

    void Finish(bool goal)
    {
        stopScore = !goal;
    }

}
