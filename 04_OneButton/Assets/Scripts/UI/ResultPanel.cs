using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    public Sprite[] medalSprits;

    ImageNumber score;
    ImageNumber bestScore;
    Image newmarkImage;
    Image medalImage;

    private void Awake()
    {
        score = transform.GetChild(0).GetComponent<ImageNumber>();
        bestScore = transform.GetChild(1).GetComponent<ImageNumber>();
        newmarkImage = transform.GetChild(2).GetComponent<Image>();
        medalImage = transform.GetChild(3).GetComponent<Image>();
    }

    private void Start()
    {
        newmarkImage.color = Color.clear;       // 시작할 때 newMark 안보이게 만들기
        GameManager.Inst.onMark += OnMark;      // newMark가 나올 타이밍에 호출 함수 연결
    }

    private void OnDisable()
    {
        // OnDestory에서 하면 게임 종료 시 GameManager는 이미 삭제되었는데
        // OnDestory에서 GameManager.Inst를 접근하여 새롭게 GameManager를 만드는 일을 방지
        // 그것을 방지하기 위해 OnDisable에서 실행

        // GameManager가 삭제되기 전에 연결 해제
        // 이 패널이 닫힐 때(게임이 끝날 때) 델리게이트에 연결된 함수 해제
        GameManager temp = GameManager.Inst;
        if (temp != null)
        {
            temp.onRankRefresh -= OnMark;
        }
    }

    public void RefreshScore()
    {
        int playerScore = GameManager.Inst.Score;
        score.Number = playerScore;                         // 현재 점수 설정
        bestScore.Number = GameManager.Inst.BestScore;      // 최고 점수 설정 (새가 죽을 때 최고점수는 자동으로 갱신된다.)

        // 100점 이상이면 브론즈 메달
        // 200점 이상이면 실버 메달
        // 300점 이상이면 골드 메달
        // 400점 이상이면 플래티넘 메달
        if (playerScore >= 400)
        {
            medalImage.sprite = medalSprits[0];
            medalImage.color = Color.white;
        }
        else if(playerScore >= 300)
        {
            medalImage.sprite = medalSprits[1];
            medalImage.color = Color.white;
        }
        else if (playerScore >= 200)
        {
            medalImage.sprite = medalSprits[2];
            medalImage.color = Color.white;
        }
        else if(playerScore >= 100)
        {
            medalImage.sprite = medalSprits[3];
            medalImage.color = Color.white;
        }
        else
        {
            medalImage.color = Color.clear;
        }        
    }

    void OnMark()
    {
        newmarkImage.color = Color.white;   //newMark의 알파값을 1로 만들어서 newMark 보이게 만들기
    }
}
