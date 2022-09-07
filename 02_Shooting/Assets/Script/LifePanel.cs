using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifePanel : MonoBehaviour
{
    TextMeshProUGUI lifeText;
    private void Awake()
    {
        lifeText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        //GameObject.Find();                    // 이름으로 찾기
        //GameObject.FindGameObjectsWithTag();  // 태그로 찾기
        //GameObject.FindObjectOfType<>();      // 타입으로 찾기

        Player player = GameObject.FindObjectOfType<Player>();  // 타입으로 player 찾고
        player.onLifeChange += refresh;                         // 델리게이트에 함수 등록
    }

    private void refresh(int life)
    {
        lifeText.text = life.ToString();    // 입력받은 Life 값으로 화면 갱신
    }
}
