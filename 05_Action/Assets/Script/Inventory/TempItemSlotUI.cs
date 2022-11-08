using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using TreeEditor;
using Unity.VisualScripting;
using System;
using UnityEngine.InputSystem;
using UnityEngine.Device;

public class TempItemSlotUI : ItemSlotUI
{
    private void Update()
    {
        transform.position = Mouse.current.position.ReadValue();    // 매 프레임마다 마우스 위치로 이동
    }

    /// <summary>
    /// TempItemSlotUI를 여는 함수
    /// </summary>
    public void Open()
    {
        if (!ItemSlot.IsEmpty)
        {
            transform.position = Mouse.current.position.ReadValue();
            gameObject.SetActive(true);
        }

    }

    /// <summary>
    /// TempItemSlotUI를 닫는 함수
    /// </summary>
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
