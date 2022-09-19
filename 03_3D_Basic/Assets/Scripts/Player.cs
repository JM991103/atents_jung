using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float rotateSpeed = 180.0f;
    public float jumpPower = 3.0f;
    float moveDir = 0.0f;
    float rotateDir = 0.0f;

    Vector3 usePosition = Vector3.zero; // 플레이어가 오브젝트 사용을 확인하는 캡슐의 아래지정
    float useRadius = 0.5f;             // 플레이어가 오브젝트 사용을 확인하는 캡슐의 반지름
    float useHeight = 2.0f;             // 플레이어가 오브젝트 사용을 확인하는 캡슐의 높이

    GroundChecker checker;
    PlayerInputActions inputActions;            // PlayerInputActions타입이고 inputAction 이름을 가진 변수를 선언
    Rigidbody rigid;
    Animator anim;

    bool isJumping = false;


    Vector3 dir;

    private void OnDrawGizmos()
    {
        // 플레이어가 오브젝트를 사용하는 범위 표시
        Gizmos.DrawWireSphere(transform.position + usePosition, useRadius);
        Gizmos.DrawWireSphere(transform.position + usePosition + transform.up * useHeight, useRadius);
    }

    private void Awake()
    {
        inputActions = new PlayerInputActions();    // 인스턴스 생성. 실제 메모리를 할당 받고 사용할 수 있도록 만드는 것
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        checker = GetComponentInChildren<GroundChecker>();
        checker.onGrounded += OnGround;

        usePosition = transform.forward;
    }

    private void FixedUpdate()
    {
        move();
        Rotate();
        if (isJumping)
        {
            if (rigid.velocity.y < 0)
            {
                checker.gameObject.SetActive(true);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platfoem"))
        {
            Platfoem platform = collision.gameObject.GetComponent<Platfoem>();
            platform.onMove += onMovingObject;  // 델리게이트 연결
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platfoem"))
        {
            Platfoem platform = collision.gameObject.GetComponent<Platfoem>();
            platform.onMove -= onMovingObject;  // 델리게이트 해제
        }
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();                   // Player 액션맵에 들어 있는 액션들을 처리 하겠다.
        inputActions.Player.Move.performed += onMove;   //
        inputActions.Player.Move.canceled += onMove;
        inputActions.Player.Jump.performed += onJump;
        inputActions.Player.Use.performed += onUseInput;
    }

    private void OnDisable()
    {
        inputActions.Player.Use.performed += onUseInput;
        inputActions.Player.Jump.performed -= onJump;
        inputActions.Player.Move.canceled -= onMove;
        inputActions.Player.Move.performed -= onMove;
        inputActions.Player.Disable();                  //Player 액션맵에 있는 액션들을 처리하지 않겠다.
    }


    private void onMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();     //입력된 값을 읽어오기

        moveDir = input.y;      //w : +1, s : -1 전진인지 후진인지 결정
        rotateDir = input.x;    //a : -1, b : +1 좌회전인지 우회전인지 결정

        anim.SetBool("isMove", !context.canceled);      // 이동키를 눌렀으면 true, 아니면 false
    }

    private void onJump(InputAction.CallbackContext _)
    {
        if (!isJumping)
        {
            //플레이어의 위쪽 방향(up)으로 jumpPower만큼 즉시 힘을 추가한다.(질량 있음)
            JumpStart();
        }
    }

    private void onUseInput(InputAction.CallbackContext _)
    {
        anim.SetTrigger("Use");         // 아이템 사용 애니메이션 재생
        //OnObjectUse?.Invoke();
        //Physics.OverlapSphere()
        Collider[] colliders = Physics.OverlapCapsule(  //캡슐 모양에 겹치는 컬라이더가 있는지 체크
            transform.position + usePosition,           //캡슐의 아래구의 중심점
            transform.position + usePosition + transform.up * useHeight,    //캡슐의 위쪽구의 중심점
            useRadius,                                  //캡슐의 반지름
            LayerMask.GetMask("UseableObject"));        //체크할 레이어

        if (colliders.Length > 0)                   //캡슐에 겹쳐진 UseableObject 컬라이더가 한개 이상이다.
        {
            IUseableObject useable = colliders[0].GetComponent<IUseableObject>();   // 여러개가 있어도 하나만 처리
            if (useable != null)    //IUseableObject를 가진 오브젝트이면
            {
                useable.Use();      //사용하기
            }
        }
    }

    void move()
    {
        //리지드바디로 이동 설정
        rigid.MovePosition(rigid.position + moveSpeed * Time.fixedDeltaTime * moveDir * transform.forward);
    }

    void Rotate()
    {
        //리지드바디로 회전 설정
        Quaternion rotate = Quaternion.AngleAxis(rotateDir * rotateSpeed * Time.fixedDeltaTime, transform.up);
        rigid.MoveRotation(rigid.rotation * rotate);
        usePosition = rotate * usePosition;
        //Quaternion.Euler(0, rotateDir * rotateSpeed * Time.fixedDeltaTime, 0) x,z 축은 회전 없고 y축 기준으로 회전
        //Quaternion.AngleAxis(rotateDir * rotateSpeed * Time.fixedDeltaTime, transform.up); 플레이어의 Y축 기준으로 회전
    }

    void JumpStart()
    {
        rigid.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        isJumping = true;

        checker.gameObject.SetActive(false);
    }

    void OnGround()
    {
        isJumping = false;
    }

    void onMovingObject(Vector3 delta)
    {
        rigid.velocity = Vector3.zero;              // 원래 플레이어의 벨로시티 제거
        rigid.MovePosition(rigid.position + delta); // 플랫폼이 이동한만큼 이동시키기
    }



}

