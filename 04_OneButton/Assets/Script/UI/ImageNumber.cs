using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


public class ImageNumber : MonoBehaviour
{
    public int expectedLength = 6;
    public GameObject digitPrefab;
    public Sprite[] numberImages = new Sprite[10];

    List<Image> digits; // 0번째가 1자리, 1번째가 10자리
    List<int> remainders;   // 숫자를 자리수별로 저장할 리스트

    public float numberChangeSpeed = 100.0f;       // 숫자 이미지가 변하는 속도
    float currentNumber = 0.0f;     // 현재 보여질 값

    int number = 0;     // 도달할 목표 값
    public int Number
    {
        get => number;
        set => number = value;
        
    }

    private void Awake()
    {

        digits = new List<Image>(transform.childCount);
        for (int i = 0; i < transform.childCount; i++)
        {
            digits.Add(transform.GetChild(i).GetComponent<Image>());
        }
        remainders = new List<int>(expectedLength);     // 자료구조를 만들 떄는 기본 크기를 잡아주는 편이 좋다.
    }


    private void Update()
    {
        if ((int)currentNumber != Number)       // currentNumber가 Number와 같아졌는지 확인. 다를 때만 아래코드 실행
        {
            float dir = (currentNumber < Number) ? 1 : -1;              // currentNumber가 변화하는 방향 구하기(증가 아니면 감소)

            currentNumber += dir * numberChangeSpeed * Time.deltaTime;  // 방향에 따라 초당 numberChangeSpeed만큼 currentNumber 변화

            if (dir > 0)
            {
                // currentNumber의방향이 증가일 때 목표인 Number를 넘친 경우 Number로 설정
                currentNumber = Mathf.Min(currentNumber, Number);       
            }
            else
            {
                // currentNumber의 방향이 감소일 때 목표인 Number 밑으로 내려간 경우 Number로 설정
                currentNumber = Mathf.Max(currentNumber, Number);       
            }

            int tempNum = (int)currentNumber;   // 표시할 숫자 결정(currentNumber에서 소수점 제거한 숫자)
            remainders.Clear();     // 자리수 별로 저장하는 리스트 일단 비우기

            // 자리수별로 숫자 분리하기
            do
            {
                remainders.Add(tempNum % 10);   // 3 -> 2 -> 1
                tempNum /= 10;                  // 12 -> 1 -> 0
            } while (tempNum != 0);                 //123으로 시작했을 때

            // 자리수에 맞게 digits 증가 또는 감소
            int diff = remainders.Count - digits.Count;
            if (diff > 0)
            {
                // digits 증가. 나머지들의 자리수가 더 기니까
                for (int i = 0; i < diff; i++)
                {
                    GameObject obj = Instantiate(digitPrefab, transform);
                    obj.name = $"Digit{Mathf.Pow(10, digits.Count)}";  // 1자리는 1, 10자리는 10, 100자리는 100이 이름에 들어가도록 완성하기
                    digits.Add(obj.GetComponent<Image>());
                }
            }
            else if (diff < 0)
            {
                // digits 감소. 나머지들의 자리수가 더 작으니까
                for (int i = digits.Count + diff; i < digits.Count; i++)
                {
                    digits[i].gameObject.SetActive(false);
                }
            }

            // 자리수별로 숫자 설정
            for (int i = 0; i < remainders.Count; i++)
            {
                digits[i].sprite = numberImages[remainders[i]];
                digits[i].gameObject.SetActive(true);
            }

        }
    }
}
