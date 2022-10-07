using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.IO;
using System.IO;
using System;

public class GameManager : Singleton<GameManager>
{
    ImageNumber scoreUI;

    Bird player;
    PipeRotator pipeRotator;

    public Action onMark;       // 최고 점수 갱신 했을 때 
    public Action onRankRefresh;
    public Action onRankUpdate;

    int score = 0;
    int bestScore = 0;

    const int RankCount = 5;
    int[] highScores = new int[RankCount];              // 0번째가 1등. 4번째가 꼴등
    string[] highScorerName = new string[RankCount];   

    // public Bird Player {get => player;} 아래와 같은 코드
    public Bird Player => player;
    
    public int Score
    {
        get => score;
        set
        {
            score = value;
            scoreUI.Number = score;
        }
    }

    public int BestScore => highScores[0];
    public int[] HighScores => highScores;
    public string[] HighScorer => highScorerName;

    protected override void Initialize()
    {
        player = FindObjectOfType<Bird>();
        player.onDead += RankUpdate;            // 새가 죽을 때 랭크 갱신

        pipeRotator = FindObjectOfType<PipeRotator>();
        pipeRotator?.AddPipeSoredDelegate(AddScore);

        scoreUI = GameObject.FindGameObjectWithTag("Score").GetComponent<ImageNumber>();

        
        LoadGameData();
    }

    void AddScore(int point)
    {
        Score += point;
    }

    /// <summary>
    /// 최고 점수와 득점자 이름 추가
    /// </summary>
    void SaveGameData()
    {
        // Serializable로 되어 있는 클래스 만들기
        SaveData saveData = new();      // 해당 클래스의 인스턴스 만들기
        saveData.highScores = highScores;       // 인스턴스에 데이터 기록
        saveData.highScoreNames = highScorerName;
        

        string json = JsonUtility.ToJson(saveData); // 해당 클래스를 json형식의 문자열로 변경

        string path = $"{Application.dataPath}/Save/";  // 파일을 저장할 폴더를 지정, 해당 경로에 Save 파일을 추가로 만든 위치를 path에 저장
        if (!Directory.Exists(path))    // 해당 폴더가 없으면
        {
            Directory.CreateDirectory(path);    // 해당 폴더를 새로 만든다.
        }

        string fullPath = $"{path}Save.json";   // 폴더이름과 파일이름을 합쳐서
        File.WriteAllText(fullPath, json);      // 파일에 json형식의 문자열로 변경한 내용을 저장

        Debug.Log("세이브 완료");
    }

    /// <summary>
    /// 최고 점수와 득점자 이름 불러오기
    /// </summary>
    void LoadGameData()
    {
        string path = $"{Application.dataPath}/save/";      // 경로 확인용
        string fullPath = $"{path}save.json";               // 전체 경로 확인용

        if (Directory.Exists(path) && File.Exists(fullPath))  //해당 폴더가 있고 파일도 있으면
        {
            string json = File.ReadAllText(fullPath);        // Json형식의 데이터 읽기
            SaveData loadData = JsonUtility.FromJson<SaveData>(json);
            Debug.Log($"Load : {loadData.highScoreNames}, {loadData.highScores}");
            highScores = loadData.highScores;               // 읽어온 데이터로 최고점수 기록 변경
            highScorerName = loadData.highScoreNames;       // 이름들도 가져오기
        }
        else
        {
            highScores = new int[] { 0, 0, 0, 0, 0 };
            highScorerName = new string[] { "임시 이름1", "임시 이름2", "임시 이름3", "임시 이름4", "임시 이름5" };
        }
    }

    public void RankUpdate()
    {
        // 뉴마크 표시할지 안할지 결정
        if (BestScore < Score)
        {
            onMark?.Invoke();       // 점수가 갱신되면 델리게이트에 연결된 함수들 실행
        }
        for (int i = 0; i < RankCount; i++)
        {
            if (highScores[i] < Score)      // 한 단계씩 비교해서 Score가 더 크면
            {
                for(int j = RankCount - 1; j > i; j--)  // 그 아래 단계는 하나씩 뒤로 밀고
                {
                    highScores[j] = highScores[j - 1];
                    highScorerName[j] = highScorerName[j - 1];
                }
                highScores[i] = score;      // 새 Score 넣기
                highScorerName[i] = $"이름{ System.DateTime.Now.ToString("HH:mm:ss")}";



                SaveGameData();             // 갱신한 점수로 저장
                break;
            }
        }
        onRankRefresh?.Invoke();
        //highScores[0] ~ highScores[4];
        //highScorerName;
    }

    public void TestSetScore(int newScore)
    {
        Score = newScore;
    }

}
