using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 180.0f;
    public float jumpPower = 3.0f;
    float moveDir = 0.0f;
    float rotateDir = 0.0f;

    PlayerInputActions inputActions;            // PlayerInputActions타입이고 inputAction 이름을 가진 변수를 선언
    Rigidbody rigid;
    
    bool isJumping = false;

    Vector3 dir;

    private void Awake()
    {
        inputActions = new PlayerInputActions();    // 인스턴스 생성. 실제 메모리를 할당 받고 사용할 수 있도록 만드는 것
        rigid = GetComponent<Rigidbody>();

        GroundChecker ground = GetComponentInChildren<GroundChecker>();
        ground.onGrounded += OnGround;
    }

    private void FixedUpdate()
    {
        move();
        Rotate();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();                   // Player 액션맵에 들어 있는 액션들을 처리 하겠다.
        inputActions.Player.Move.performed += onMove;   //
        inputActions.Player.Move.canceled += onMove;
        inputActions.Player.Jump.performed += onJump;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= onMove;
        inputActions.Player.Move.canceled -= onMove;
        inputActions.Player.Jump.performed -= onJump;
        inputActions.Player.Disable();                  //Player 액션맵에 있는 액션들을 처리하지 않겠다.
    }

    private void onMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();     //입력된 값을 읽어오기

        moveDir = input.y;      //w : +1, s : -1 전진인지 후진인지 결정
        rotateDir = input.x;    //a : -1, b : +1 좌회전인지 우회전인지 결정
    }

    private void onJump(InputAction.CallbackContext _)
    {
        if(!isJumping)
        { 
            //플레이어의 위쪽 방향(up)으로 jumpPower만큼 즉시 힘을 추가한다.(질량 있음)
            rigid.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            isJumping = true;
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
        rigid.MoveRotation(rigid.rotation * Quaternion.AngleAxis(rotateDir * rotateSpeed * Time.fixedDeltaTime, transform.up));

        //Quaternion.Euler(0, rotateDir * rotateSpeed * Time.fixedDeltaTime, 0) x,z 축은 회전 없고 y축 기준으로 회전
        //Quaternion.AngleAxis(rotateDir * rotateSpeed * Time.fixedDeltaTime, transform.up); 플레이어의 Y축 기준으로 회전
    }

    void OnGround()
    {
        isJumping = false;
    }
}
