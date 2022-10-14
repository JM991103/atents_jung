using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 걷는 이동 속도
    /// </summary>
    public float walkSpeed = 3.0f;

    /// <summary>
    /// 달리는 이동 속도
    /// </summary>
    public float RunSpeed = 5.0f;

    /// <summary>
    /// 회전 속도
    /// </summary>
    public float turnSpeed = 10.0f;

    /// <summary>
    /// 현재 이동 속도
    /// </summary>
    float currentSpeed = 10.0f;

    /// <summary>
    /// 이동 상태를 나타내는 enum
    /// </summary>
    enum MoveMode 
    {
        Walk = 0,
        Run
    }

    /// <summary>
    /// 현재 이동 상태
    /// </summary>
    MoveMode moveMode = MoveMode.Walk;

    /// <summary>
    /// 입력으로 지정된 바라보는 방향
    /// </summary>
    Vector3 inputDir = Vector3.zero;

    /// <summary>
    /// 최종 회전 목표
    /// </summary>
    Quaternion targetRotation = Quaternion.identity;

    /// <summary>
    /// 인풋 액션 인스턴스
    /// </summary>
    PlayerInputAction inputAction;
    
    /// <summary>
    /// 애니메이터 컴포넌트 캐싱용
    /// </summary>
    Animator anim;

    private void Awake()
    {
        // 컴포넌트 만들어졌을 때 인풋 액션 인스턴스 생성
        inputAction = new PlayerInputAction();

        // 컴포넌트 찾아오기
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // inputDir 방향으로 초당 moveSpeed의 속도로 이동. 월드 스페이스 기준으로 이동
        transform.Translate(currentSpeed * Time.deltaTime * inputDir, Space.World);

        // transform.rotation에서 targetRotation으로 초당 1/turnSpeed씩 보간
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
        // 인풋 액션에서 액션맵 활성화
        inputAction.PlayerInput.Enable();
        // 액션과 함수 연결
        inputAction.PlayerInput.Move.performed += OnMove;
        inputAction.PlayerInput.Move.canceled += OnMove;
        inputAction.PlayerInput.Shift.performed += OnMovemodChange;
        inputAction.PlayerInput.Shift.canceled += OnMovemodChange;
    }

    private void OnDisable()
    {
        // 액션과 함수 연결 해제
        inputAction.PlayerInput.Shift.performed -= OnMovemodChange;
        inputAction.PlayerInput.Shift.canceled -= OnMovemodChange;
        inputAction.PlayerInput.Move.performed -= OnMove;
        inputAction.PlayerInput.Move.canceled -= OnMove;
        // 액션맵 비활성화
        inputAction.PlayerInput.Disable();
    }

    /// <summary>
    /// Player액션맵의 Move액션이 performed되거나 canceled될 때 실행
    /// </summary>
    /// <param name="context"></param>
    private void OnMove(InputAction.CallbackContext context)
    {
        // WASD 입력을 받아옴(+X : D, -X : A, +Y : W, -Y : S)
        Vector2 input = context.ReadValue<Vector2>();

        inputDir.x = input.x;   // 입력받은 것을 3D XZ평면상의 방향으로 변경
        inputDir.y = 0.0f;
        inputDir.z = input.y;

        if (!context.canceled)
        {
            // 입력이 들어왔을 때만 실행되는 코드


            Quaternion cameraYRotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);  // 카메라의 Y축 회전만 분리
            // 카메라의 Y축 회전을 inputDir에 곱한다. => inputDir과 카메라가 XZ평면상에서 바라보는 방향을 일치시킴
            inputDir = cameraYRotation * inputDir;

            targetRotation = Quaternion.LookRotation(inputDir); // inputDir 방향으로 바라보는 회전 만들기

            if (moveMode == MoveMode.Walk)
            {
                anim.SetFloat("Speed", 0.3f);   // Walk모드면 걷는 애니메이션
            }
            else
            {
                anim.SetFloat("Speed", 1.0f);   // Run모드면 달리는 애니메이션
            }
        }
        else
        {
            anim.SetFloat("Speed", 0.0f);       // 입력이 안들어 왔으면 대기 애니메이션
        }
    }

    /// <summary>
    /// 쉬프트 키를 눌렀을 때 실행
    /// </summary>
    /// <param name="_"></param>
    private void OnMovemodChange(InputAction.CallbackContext _)
    {
        if (moveMode == MoveMode.Walk)
        {
            // walk 모드면 RunMode로 전환
            moveMode = MoveMode.Run;
            currentSpeed = RunSpeed;        // 이동 속도도 달리는 속도로 변경
            if (inputDir != Vector3.zero)
            {
                anim.SetFloat("Speed", 1.0f);   // 움직이는 중일 때만 재생하는 애니메이션도 변경
            }
        }
        else
        {
            // Run모드면 Walk모드로 전환
            moveMode = MoveMode.Walk;
            currentSpeed = walkSpeed;       // 이동 속도를 걷는 속도로 변경
            if (inputDir != Vector3.zero)
            {
                anim.SetFloat("Speed", 0.3f);   // 움직이는 중일 때만  재생하는 애니메이션도 변경
            }
        }
    }
}
