using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class BattleLogger : MonoBehaviour
{
    /// <summary>
    /// 유저 플레이어 이름 출력할 때 사용할 색상
    /// </summary>
    public Color userColor;

    /// <summary>
    /// 적 플레이어 이름 출력할 때 사용할 색상
    /// </summary>
    public Color enemyColor;

    /// <summary>
    /// 배 이름 출력할 때 사용할 색상
    /// </summary>
    public Color shipColor;

    /// <summary>
    /// 턴 숫자 출력할 때 사용할 색상
    /// </summary>
    public Color turnColor;

    /// <summary>
    /// 글자가 출력될 텍스트 메시 프로
    /// </summary>
    TextMeshProUGUI logText;


    /// <summary>
    /// 자주 사용하는 텍스트 재사용용 상수
    /// </summary>
    const string YOU = "당신";
    const string ENEMY = "적";

    /// <summary>
    /// 최대 표현 가능한 줄 수
    /// </summary>
    const int MaxLineCount = 20;

    /// <summary>
    /// 로거에서 출력할 문자열들을 가지고 있는 리스트
    /// </summary>
    List<string> logLines;

    /// <summary>
    /// 문자열 조합용 스트링빌더
    /// </summary>
    StringBuilder builder;

    private void Awake()
    {
        logText = GetComponentInChildren<TextMeshProUGUI>();
        logLines = new List<string>(MaxLineCount + 5);  // List의 Capacity를 5개 여유있게 확보
        builder = new StringBuilder(logLines.Capacity); // SringBuilder의 크기를 logLines의 Capacity만큼 확보
    }

    private void Start()
    {
        // 턴 시작할 때 턴 번호 출력하기 위해 델리게이트 함수 등록
        TurnManager turnManager = TurnManager.Inst;
        turnManager.onTurnStart += Log_Turn_Start;

        GameManager gameManager = GameManager.Inst;
        foreach (var ship in gameManager.UserPlayer.Ships)
        {
            ship.onHit += (targetShip) => { Log_Attack_Success(false, targetShip); };
            //ship.onSinking = (targetShip) => { Log_Ship_Destroy(false, targetShip); } + ship.onSinking;
        }
        foreach (var ship in gameManager.EnemyPlayer.Ships)
        {
            ship.onHit += (targetShip) => { Log_Attack_Success(true, targetShip); };
            //ship.onSinking = (targetShip) => { Log_Ship_Destroy(true, targetShip); } + ship.onSinking;
        }
        
        


        Clear();
    }

    /// <summary>
    /// 로거에 입력받은 데이터를 출력하는 함수 
    /// </summary>
    /// <param name="text">출력할 문자</param>
    void Log(string text)
    {
        logLines.Add(text);                 // 입력 받은 문자열을 리스트에 추가 
        if (logLines.Count > MaxLineCount)  // 현재 줄 수가 최대 줄 수보다 많아지면
        {
            logLines.RemoveAt(0);           // 첫 번째 줄 삭제 
        }

        builder.Clear();                    // 문자열 조합용 빌더 초기화

        foreach (var line in logLines)
        {
            builder.AppendLine(line);       // 빌더에 문자열 추가
        }

        logText.text = builder.ToString();  // 빌더에서 합친 문자열을 text에 넣기
    }

    /// <summary>
    /// 로거의 내용을 다 지우는 함수
    /// </summary>
    void Clear()
    {
        logLines.Clear();   // 기록된 string 초기화
        logText.text = "";  // 표시 되어있는 Text 지우기
    }

    /// <summary>
    /// 공격이 성공했을 때 상황을 출력하는 함수
    /// </summary>
    /// <param name="isPlayerAttack">true면 플레이어 공격, false면 적의 공격</param>
    /// <param name="ship">공격을 당한 배</param>
    private void Log_Attack_Success(bool isPlayerAttack, Ship ship)
    {
        string attackerColor;   // 공격자 색상
        string attackerName;    // 공격자 이름
        if (isPlayerAttack)
        {
            attackerColor = ColorUtility.ToHtmlStringRGB(userColor);
            attackerName = YOU;
        }
        else
        {
            attackerColor = ColorUtility.ToHtmlStringRGB(enemyColor);
            attackerName = ENEMY;
        }

        string shipColor = ColorUtility.ToHtmlStringRGB(this.shipColor);
        string playerColor;              // 배의 소유주가 UserPlayer면 userColor, EnemyPlayer면 enemyColor로 출력하기
        string playerName;          

        if (ship.Owner is UserPlayer)   // 배의 소유주가 UserPlayer 인지 아닌지 확인
        {   
            playerColor = ColorUtility.ToHtmlStringRGB(userColor);  // 색상 지정
            playerName = YOU;                                       // 이름 지정
        }
        else
        {
            playerColor = ColorUtility.ToHtmlStringRGB(enemyColor);
            playerName = ENEMY;
        }
        Log($"<#{attackerColor}><{attackerName}>의 공격 </color> \t : <#{playerColor}>{playerName}</color>의 <#{shipColor}>{ship.name}</color>에 포탄이 명중했습니다.");
    }

    private void Log_Ship_Destroy()
    {
        
    }

    /// <summary>
    /// 턴을 시작할 때 상황을 알려주는 함수
    /// </summary>
    /// <param name="number">현재 턴 수</param>
    private void Log_Turn_Start(int number)
    {
        string color = ColorUtility.ToHtmlStringRGB(turnColor);
        Log($"<#{color}>{number}</color> 번째 턴이 시작했습니다.");
    }
}
