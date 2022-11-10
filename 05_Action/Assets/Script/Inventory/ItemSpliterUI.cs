using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ItemSpliterUI : MonoBehaviour
{
    /// <summary>
    /// 아이템을 분리할 최소 갯수
    /// </summary>
    const int itemCountMin = 1;

    /// <summary>
    /// 분리할 갯수
    /// </summary>
    uint itemSplitCount = itemCountMin;

    /// <summary>
    /// 아이템을 분리할 슬롯
    /// </summary>
    ItemSlot targetSlot;

    /// <summary>
    /// 분리할 갯수 입력을 위한 인풋 필드
    /// </summary>
    TMP_InputField inputField;

    /// <summary>
    /// 분리할 갯수 입력을 위한 슬라이더
    /// </summary>
    Slider slider;

    /// <summary>
    /// 분리할 아이템 아이콘
    /// </summary>
    Image itemImage;

    /// <summary>
    /// 분리할 갯수 설정 및 확인을 위한 프로퍼티 
    /// </summary>
    uint ItemSplitCount
    {
        get => itemSplitCount;
        set
        {
            if (itemSplitCount != value)    // 분리할 갯수에 변경이 있을 때
            {
                itemSplitCount = value;
                itemSplitCount = (uint)Mathf.Max(1, itemSplitCount);    // 최소값은 1

                if (targetSlot != null)
                {
                    itemSplitCount = (uint)Mathf.Min(itemSplitCount, targetSlot.ItemCount - 1); // 최대값은 슬롯에 들어있는 갯수 -1
                }
                // 결정된 분리 갯수를 인풋필드와 슬라이더로 표현
                inputField.text = itemSplitCount.ToString();
                slider.value = itemSplitCount;
            }
        }
    }

    private void Awake()
    {
        // 각종 초기화

        // 인풋 필드 찾기
        inputField = GetComponentInChildren<TMP_InputField>();                                      
        // 인풋 필드의 값이 변경될 때 변경된 값이 ItemSplitCount에 적용
        inputField.onValueChanged.AddListener((text) => ItemSplitCount = uint.Parse(text));         

        // 슬라이더 컴포넌트 찾기
        slider = GetComponentInChildren<Slider>();
        // 슬라이더의 값이 변경될 때 변경된 값이 ItemSplitCount에 적용
        //slider.onValueChanged.AddListener(ChangeSliderValue);                                         // 일반 함수를 사용하는 방법
        slider.onValueChanged.AddListener((value) => ItemSplitCount = (uint)Mathf.RoundToInt(value));   // 람다 함수를 사용하는 방법

        // 증가 버튼 컴포넌트 찾기
        Button increase = transform.GetChild(1).GetComponent<Button>();
        // 증가 버튼이 눌러질 때 마다 ItemSplitCount 1씩 증가
        increase.onClick.AddListener(() => ItemSplitCount++);
        // 감소 버튼 컴포넌트 추가
        Button decrease = transform.GetChild(2).GetComponent<Button>();
        // 감소 버튼이 눌러질 때 마다 ItemSplitCount 1씩 감소
        decrease.onClick.AddListener(() => ItemSplitCount--);

        Button ok = transform.GetChild(4).GetComponent<Button>();
        Button cancel = transform.GetChild(5).GetComponent<Button>();

        // 아이템 아이콘을 표시할 이미지 컴포넌트 찾기
        itemImage = transform.GetChild(6).GetComponent<Image>();
    }

    //private void ChangeSliderValue(float value)      // AddListenrt에 일반 함수 사용하는 것을 위한 예시
    //{
    //    ItemSplitCount = (uint)Mathf.RoundToInt(value);
    //}

    private void Start()
    {   
        Close();    // 시작할 때 닫고 시작하기
    }

    /// <summary>
    /// 아이템 분리창을 여는 함수
    /// </summary>
    /// <param name="target">아이템을 분리할 슬롯</param>
    public void Open(ItemSlotUI target)
    {
        targetSlot = target.ItemSlot;   // 슬롯 가져오고

        //Debug.Log($"{targetSlot.ItemData.itemName} : {targetSlot.ItemCount}개");
        itemImage.sprite = targetSlot.ItemData.itemIcon;    // 아이콘 설정

        slider.minValue = itemCountMin;                     // 최대 최소값 설정
        slider.maxValue = targetSlot.ItemCount - 1;

        this.gameObject.SetActive(true);                    // 실제로 활성화해서 보여주기

    }

    /// <summary>
    /// 아이템 분리창을 닫는 함수
    /// </summary>
    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
