using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]  // 직렬화 가능하다. 데이터가 한줄로 묶여 있다. (한 덩어리)
public class SaveData
{
    public int[] highScores;            // 최고 득점들 . 0번째가 1등 4번째가 꼴등(5등)
    public string[] highScoreNames;     // 최고 득점자 이름. 순서는 위와 같음
}
