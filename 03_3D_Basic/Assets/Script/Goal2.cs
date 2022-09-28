using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal2 : MonoBehaviour
{
    ParticleSystem[] ps;

    public Action onGoalIn;
    public string nextSceanName;

    private void Awake()
    {
        Transform effect = transform.GetChild(2);
        ps = effect.GetComponentsInChildren<ParticleSystem>();  // 골인할 때 터트릴 파티클 시스템 찾기
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))     // 트리거 안에 플레이어가 들어왔을 때
        {
            PlayGoalInEffect();             // 골인 이펙트 터트리기
            StartCoroutine(Wait1Second());  // 1초 이후에 결과창 열기
            onGoalIn?.Invoke();             // 골인했을 때 실행되는 함수들 실행
        }
    }

    void PlayGoalInEffect()
    {
        //int i = 10;
        //var j = i;
        // var : C#이 알아서 데이터 타입을 설정해 줌
        foreach (var effect in ps)      // 찾아놓은 골인 시스템을 전부 실행하기
        {
            effect.Play();
        }
    }

    IEnumerator Wait1Second()
    {
        yield return new WaitForSeconds(1.0f);
        GameManager.Inst.ShowResultPanel();     // 결과창 열기
    }

    public void GoNextStage()
    {
        SceneManager.LoadScene(nextSceanName);  // 지정된 씬으로 변경
    }
}
