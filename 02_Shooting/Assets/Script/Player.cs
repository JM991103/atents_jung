//using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //public delegate void DelegateName();    //이런 종류의 델리게이트가 있다. (리턴이 없고 파라메터(매개변수)도 없는 함수를 저장하는 델리게이트)
    //public DelegateName del;        //DelegateName 타입으로 del이라는 이름의 델리게이트를 만듬
    //Action del2;              //리턴타입이 void 파라메터(매개변수)도 없는 델리게이트 del2를 만듬
    //Action<int> del3;         //리턴타입이 void, 파라메터(매개변수)는 int 하나만 델리게이트 del3을 만듬
    //Func<int, float> del4;    //리턴타입이 int고 파라메터(매개변수)는 float 하나인 델리게이트 del4를 만듬
    //Action은 무조건 리턴타입이 void이고 Func는 <> 첫번째 가 리턴타입이 됨
    


    PlayerInputAction inputActions;
    // Awake -> OnEnable -> start : 대체적으로 이 순서

    public GameObject Bullet;
    public float speed = 1.0f;      // player의 이동 속도(초당 이동 속도)
    Vector3 dir;                    // 이동 방향(입력에 따라 변경됨)
    float boost = 1.0f;

    //bool isFiring = false;
    public float fireInterval = 0.3f;
    //float firetimeCount = 0.0f;

    Transform[] firePosition;   //트랜스폼을 여러개 가지는 배열
    Vector3[] firearry = new Vector3[3];
 

    IEnumerator firea;

    Rigidbody2D rigid;
    Animator anim;



    /// <summary>
    /// 이 스크립트가 들어있는 게임 오브젝트가 생성된 직후에 호출
    /// </summary>
    private void Awake()
    {
        inputActions = new PlayerInputAction();
        rigid = GetComponent<Rigidbody2D>();    //한번만 찾고 저장해서 계속 쓰기 (메모리 더 쓰고 성능 아끼기)
        anim = GetComponent<Animator>();

        firePosition = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            firePosition[i] = transform.GetChild(i);
        }

        firearry[0] = new Vector3(0, 0, 0);
        firearry[1] = new Vector3(0, 0, 30);
        firearry[2] = new Vector3(0, 0, -30);

        firea = fire();
    }

    /// <summary>
    /// 이 스크립트가 들어있는 게임 오브젝트가 활성화 되었을 때 호출
    /// </summary>
    private void OnEnable()
    {
        inputActions.player.Enable(); //오브젝트가 생성되면 입력을 받도록 활성화
        inputActions.player.Move.performed += OnMove;   //performed 일 때 OnMove 함수 실행하도록 연결
        inputActions.player.Move.canceled += OnMove;    //canceled 일 때 OnMove 함수 실행하도록 연결
        inputActions.player.Fire.performed += OnFireStart;
        inputActions.player.Fire.canceled += OnFireStop;
        inputActions.player.Booster.performed += OnBooster;
        inputActions.player.Booster.canceled += OffBooster;
    }

    /// <summary>
    /// 이 스크립트가 들어있는 게임 오브젝트가 비활성화 되었을 때 호출
    /// </summary>
    private void OnDisable()
    {
        inputActions.player.Booster.performed -= OffBooster;
        inputActions.player.Booster.canceled -= OnBooster;
        inputActions.player.Fire.canceled -= OnFireStop;
        inputActions.player.Fire.performed -= OnFireStart;
        inputActions.player.Move.performed -= OnMove; //연결해 놓은 함수 해제(안전을 위해)
        inputActions.player.Move.canceled -= OnMove;  
        inputActions.player.Disable(); //오브젝트가 사라질 때 더이상 입력을 받지 않도록 비활성화
    }

    /// <summary>
    /// 시작할 때, 첫번째 Update 함수가 실행되기 직전에 호출
    /// </summary>
    private void Start()
    {
        
    }

    /// <summary>
    /// 매 프레임마다 호출
    /// </summary>
    private void Update()
    {
        //transform.position += speed * dir * Time.deltaTime;
        //transform.Translate(speed * dir * Time.deltaTime);

    }

    /// <summary>
    /// 일정 시간 간격(물리 업데이트 시간 간격)으로 호출
    /// </summary>
    private void FixedUpdate()
    {
        //transform.Translate(speed * dir * Time.deltaTime);
        //이 스크립트 파일이 들어있는 게임 오브젝트에서 Rigidbody2D 컴포넌트를 찾아 리턴. (없으면 null)
        // 그런데 Rigidbody는 무거운 함수 => (Update나 FixedUpdate처럼 주기적 또는 자주 호출되는 함수 안에서는 안쓰는 것이 좋다.)
        //Rigidbody2D rigid = GetComponent<Rigidbody2D>();    

        //rigid.AddForce(speed * Time.fixedDeltaTime * dir); //관성이 있는 움직임을 할 때 유용함
        rigid.MovePosition(transform.position + (boost * speed) * dir * Time.fixedDeltaTime); //관성이 없는 움직임을 처리할 때 유요함

        //firetimeCount += Time.fixedDeltaTime;
        //if(isFiring && firetimeCount > fireInterval)
        {
         //   Instantiate(Bullet, transform.position, Quaternion.identity);
         //      firetimeCount = 0.0f;
        }
    }

    ////(collider 와 trigger 다른 점은 통과 되는지 안되는지)
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("OnCollisionEnter2D");    //collider와 부딪쳤을 때 실행 (리지드바디가 있는, 두존재 다 collider가 있어야함)
    //}
    ////private void OnCollisionStay2D(Collision2D collision)
    ////{
    ////    Debug.Log("OnCollisionStay2D");     // collider와 계속 접촉하면서 움직일 때(매 프레임마다 호출)
    ////}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    Debug.Log("OnCollisionExit2D");     // collider와 접촉이 떨어지는 순간 실행
    //}
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("OnTriggerEnter2D");      // trigger에 들어갔을때 실행 
    //}
    ////private void OnTriggerStay2D(Collider2D collision)
    ////{
    ////    Debug.Log("OnTriggerStay2D");       // trigger와 계속 겹쳐있으면서 움직일 때 (매 프레임마다 호출)
    ////}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log("OnTriggerExit2D");       // trigger에서 나갔을 때 실행
    //}

    private void OnMove(InputAction.CallbackContext context)
    {
        //Exception : 예외 상황(무엇을 해야 할지 지정이 안되어있는 예외 일때(코드가 짜여지지 않은 상황))
        //throw new NotImplementedException();    //NotImplementedException(구현되지 않았음) 을 실행해라. => 코드 구현을 알려주기 위해 강제로 죽이는 코드

        //Debug.Log("이동 입력");
        dir = context.ReadValue<Vector2>();

        //dir.y > 0     //w를 눌렀다.
        //dir.y == 0    //w,s중 아무것도 안눌렀다.
        //dir.y < 0     //s를 눌렀다
        anim.SetFloat("InputY", dir.y);
    }

    public void OnFireStart(InputAction.CallbackContext _)
    {
        ////Debug.Log("발사");
        //float value = UnityEngine.Random.Range(0.0f, 10.0f);   //value는 0.0 ~ 10.0사이의 랜덤값이 들어간다.
        ////UnityEngine를 사용하지 않으면 Using System;을 지워야함
        //Instantiate(Bullet, transform.position, Quaternion.identity);

        // isFiring = true;
        StartCoroutine(firea); //코루틴 시작
    }

    private void OnFireStop(InputAction.CallbackContext _)
    {
        //  isFiring = false;
        //StopCoroutine();   //모든 코루틴이 다 멈춤
        //StopCoroutine(fire());
        StopCoroutine(firea);
    }
    IEnumerator fire()  //코루틴 선언
    {
        //yield return null;  //다음 프레임에 이어서 시작해라
        //yield return new WaitForSeconds(1.0f);  // 1초후에 이어서 시작해라
        while (true)
        {
            for (int i = 0; i < firePosition.Length; i++)
            {
                GameObject obj = Instantiate(Bullet, firePosition[i].position, Quaternion.identity);
                obj.transform.Rotate(firearry[i]);
                
            }
            yield return new WaitForSeconds(fireInterval);
        }
    }

    private void OnBooster(InputAction.CallbackContext context)
    {
        boost *= 2.0f;
    }

    private void OffBooster(InputAction.CallbackContext context)
    {
        boost = 1.0f;
    }

}
