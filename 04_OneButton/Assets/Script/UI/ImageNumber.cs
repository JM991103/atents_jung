using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


public class ImageNumber : MonoBehaviour
{
    public Sprite[] numberImages = new Sprite[10];

    public int inputNumber;

    public float scoreUpSpeed = 100.0f;     // 점수가 올라가는 시간
    float targetScore = 0.0f;               // 목표 값
    float currentScore = 0.0f;              // 현재 값

    Image[] digits;

    private void Awake()
    {
        digits = new Image[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            digits[i] = transform.GetChild(i).GetComponent<Image>();
            
        }
        
    }

    int number;
    public int Number
    {
        get => number;
        set 
        {
            number = value;

            // (123 / 1) % 10 = 3
            // (123 / 10) % 10 = 2
            // (123 / 100) % 10 = 1
            int mod = number % 10;
         
            digits[0].sprite = numberImages[mod];

        }
    }

    private void Update()
    {
        if (currentScore < targetScore)
        {
            currentScore += Time.deltaTime * scoreUpSpeed;

            currentScore = Mathf.Min(currentScore, targetScore);
        }
    }


}
