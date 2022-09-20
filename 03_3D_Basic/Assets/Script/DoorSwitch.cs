using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 연결된 문을 열고 닫는 스위치. IUseavleObject 상속 받음
/// </summary>
public class DoorSwitch : MonoBehaviour, IUseavleObject
{
    /// <summary>
    /// 이 스위치로 열고 닫을 문
    /// </summary>
    public TwoWayDoor targetDoor;

    Animator anim;
    /// <summary>
    /// 스위치 사용 여부, true면 켰다. false면했  껐다.
    /// </summary>
    bool switchOn = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// 이 오브젝트가 사용되면 실행될 암수
    /// </summary>
    public void Use()
    {
        switchOn = !switchOn;   // 스위치 on/off 서로 전환
        anim.SetBool("SwitchOn",switchOn);  // SwitchOn에 맞게 애니메이션 재생
        if (switchOn)
        {
            //스위치를 켰으면 targetDoor를 연다.
            targetDoor.Open();
            
        }
        else
        {
            //스위치를 끄면 targetDoor를 닫는다.
            targetDoor.Close();
        }
    }
}
