//using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Text;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;

public class Player : MonoBehaviour
{
    //public delegate void DelegateName();    //이런 종류의 델리게이트가 있다. (리턴이 없고 파라메터(매개변수)도 없는 함수를 저장하는 델리게이트)
    //public DelegateName del;        //DelegateName 타입으로 del이라는 이름의 델리게이트를 만듬
    //Action del2;              //리턴타입이 void 파라메터(매개변수)도 없는 델리게이트 del2를 만듬
    //Action<int> del3;         //리턴타입이 void, 파라메터(매개변수)는 int 하나만 델리게이트 del3을 만듬
    //Func<int, float> del4;    //리턴타입이 int고 파라메터(매개변수)는 float 하나인 델리게이트 del4를 만듬
    //Action은 무조건 리턴타입이 void이고 Func는 <> 첫번째 가 리턴타입이 됨

    //public 변수(필드)----------------------------------------------------------------------------------------
    [Header("플레이어 스텟")]
    /// <summary>
    /// player의 이동 속도(초당 이동 속도)
    /// </summary>
    public float speed = 1.0f;     

    /// <summary>
    /// 총알 발사 시간 간격
    /// </summary>
    public float fireInterval = 0.3f;

    [Header("게임 기본 설정")]
    /// <summary>
    /// 초기 생명 개수
    /// </summary>
    public int initaialLife = 3;

    /// <summary>
    /// 피격 시 무적 시간
    /// </summary>
    const float InvincibleTime = 1.0f;

    [Header("각종 프리팹")]
    /// <summary>
    /// 총알용 프리팹
    /// </summary>
    public GameObject BulletPrefab;

    /// <summary>
    /// 비행기 폭팔 이펙트용 프리팹
    /// </summary>
    public GameObject explosionPrefab;



    /// <summary>
    /// 현재 생명 수
    /// </summary>
    public int life;

    /// <summary>
    /// 무적 상태인지 표시용(true면 무적상태, false 일반상태)
    /// </summary>
    private bool invincibleMode = false;

    /// <summary>
    /// 무적 상태에 들어간 시간(의 30배)
    /// </summary>
    private float timeElapsed = 0.0f;

    /// <summary>
    /// 입력된 이동 방향
    /// </summary>
    private  Vector3 dir;                    // 이동 방향(입력에 따라 변경됨)

    /// <summary>
    /// 이동
    /// </summary>
    private  float boost = 1.0f;

    private SpriteRenderer spriteRenderer;
    private PlayerInputAction inputActions;
    private  Collider2D bodyCollider;
    private  bool isDead = false; 
    // Awake -> OnEnable -> start : 대체적으로 이 순서

   
      

    //bool isFiring = false;
    
    
    //float firetimeCount = 0.0f;

    private  Transform firePositionRoot;   //트랜스폼을 여러개 가지는 배열
    private  GameObject flash;
    private float fireAngle = 30.0f;
    private  int power = 0;
    private  Vector3[] firearry = new Vector3[3];
    private  IEnumerator firea;
    private  Rigidbody2D rigid;
    private Animator anim;


    /// <summary>
    /// 이 스크립트가 들어있는 게임 오브젝트가 생성된 직후에 호출
    /// </summary>
    private void Awake()
    {
        inputActions = new PlayerInputAction();
        rigid = GetComponent<Rigidbody2D>();    //한번만 찾고 저장해서 계속 쓰기 (메모리 더 쓰고 성능 아끼기)
        anim = GetComponent<Animator>();
        bodyCollider = GetComponent<Collider2D>();  // CapsulCollider2D가 collider2D의 자식이라서 가능

        firePositionRoot = transform.GetChild(0);
        flash = transform.GetChild(1).gameObject;
        flash.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        //firearry[0] = new Vector3(0, 0, 0);
        //firearry[1] = new Vector3(0, 0, 30);
        //firearry[2] = new Vector3(0, 0, -30);


        firea = fire();
        life = initaialLife;
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
        
        inputActions.player.Disable(); //오브젝트가 사라질 때 더이상 입력을 받지 않도록 비활성화
    }

    void InputDisable()
    {
        inputActions.player.Booster.performed -= OffBooster;
        inputActions.player.Booster.canceled -= OnBooster;
        inputActions.player.Fire.canceled -= OnFireStop;
        inputActions.player.Fire.performed -= OnFireStart;
        inputActions.player.Move.performed -= OnMove; //연결해 놓은 함수 해제(안전을 위해)
        inputActions.player.Move.canceled -= OnMove;
    }

    /// <summary>
    /// 시작할 때, 첫번째 Update 함수가 실행되기 직전에 호출
    /// </summary>
    private void Start()
    {
        Power = 1;

    }

    /// <summary>
    /// 매 프레임마다 호출
    /// </summary>
    private void Update()
    {
        //transform.position += speed * dir * Time.deltaTime;
        //transform.Translate(speed * dir * Time.deltaTime);

        if (invincibleMode)
        {
            timeElapsed += Time.deltaTime * 30.0f;
            float alpha = (Mathf.Cos(timeElapsed) + 1.0f) * 0.5f;  // cos의 결과를 1~0으로 변경
            spriteRenderer.color = new Color(1, 1, 1, alpha);
        }
    }

    /// <summary>
    /// 일정 시간 간격(물리 업데이트 시간 간격)으로 호출
    /// </summary>
    private void FixedUpdate()
    {
        if (!isDead)
        {
            //transform.Translate(speed * dir * Time.deltaTime);
            //이 스크립트 파일이 들어있는 게임 오브젝트에서 Rigidbody2D 컴포넌트를 찾아 리턴. (없으면 null)
            // 그런데 Rigidbody는 무거운 함수 => (Update나 FixedUpdate처럼 주기적 또는 자주 호출되는 함수 안에서는 안쓰는 것이 좋다.)
            //Rigidbody2D rigid = GetComponent<Rigidbody2D>();    

            //rigid.AddForce(speed * Time.fixedDeltaTime * dir); //관성이 있는 움직임을 할 때 유용함
            rigid.MovePosition(transform.position + (boost * speed) * dir * Time.fixedDeltaTime); //관성이 없는 움직임을 처리할 때 유요함

            //firetimeCount += Time.fixedDeltaTime;
            //if(isFiring && firetimeCount > fireInterval)
            //{
            //   Instantiate(Bullet, transform.position, Quaternion.identity);
            //      firetimeCount = 0.0f;
            //}
        }
        else
        {
            rigid.AddForce(Vector2.left * 0.1f, ForceMode2D.Impulse);   // 죽었을 때 뒤로 돌면서 튕겨 나가기
            rigid.AddTorque(10.0f);
        }
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            // 파워업 아이템을 먹었으면
            Power++;                        //파워 증가 시키고
            Destroy(collision.gameObject);  //파워업 아이템 삭제
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 적이랑 부딪치면 life가 1 감소한다.
            Life--;

            Debug.Log($"플레이어의 Life는 {Life}");

        }
    }
    
    IEnumerator EnterinvnicibleMode()
    {
        bodyCollider.enabled = false;
        invincibleMode = true;
        timeElapsed = 0.0f;

        yield return new WaitForSeconds(InvincibleTime);    // 무적시간 동안 대기

        spriteRenderer.color = Color.white; //원래 색으로 되돌리기
        invincibleMode = false;
        bodyCollider.enabled = true;
    }

    void Dead()
    {
        isDead = true;      
        GetComponent<Collider2D>().enabled = false;     // 더 이상 충돌 안일어나게 만들기
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);  // 폭발 이펙트 생성
        InputDisable();                  // 입력 막고
        rigid.gravityScale = 1.0f;      // 중력으로 떨어지게 만들기
        rigid.freezeRotation = false;    // 회전 막아 놓은것 풀기
        StopCoroutine(firea);
    }
    // 프로퍼티 ------------------------------------------------------------------------------------------------

    int Power
    {
        get => power;
        set
        {
            power = value;  // 들어온 값으로 파워 설정
            if (power > 3)  // 파워가 3을 벗어나면 3을 제한
                power = 3;

            // 기존에 있는 파이어 포지션 제거
            while (firePositionRoot.childCount > 0)
            {
                Transform temp = firePositionRoot.GetChild(0);  // firePositionRoot의 첫번째 자식을 
                temp.parent = null;         // 부모 제거 하고
                Destroy(temp.gameObject);   // 삭제 시키기
            }

            //파워 등급에 맞게 새로 배치
            for (int i = 0; i < power; i++)
            {
                GameObject firePos = new GameObject();  // 빈 오브젝트 생성하기
                firePos.name = $"FirePosition{i}";
                firePos.transform.parent = firePositionRoot;    // firePositionRoot의 자식으로 추가
                firePos.transform.localPosition = Vector3.zero; // 로컬 위치를 (0,0,0)으로 변경 / 아래 줄과 같은 기능
                //firePos.transform.position = firePositionRoot.transform.position;

                // 파워가 1개 일때 : 0도
                // 파워가 2개 일때 : -15, +15도
                // 파워가 3개 일때 : -30도, 0도, +30도
                firePos.transform.rotation = Quaternion.Euler(0, 0, (power - 1) * (fireAngle * 0.5f) + i * -fireAngle);
                firePos.transform.Translate(1.0f, 0.0f, 0.0f);

                //시작 각도 i * (fireAngle * 0.5)
                //계산 식 : (power - 1) * (fireAngle * 0.5) + i * -fireAngle
            }
        }
    }  

    int Life    //프로퍼티
    {
        //get
        //{
        //    return life;
        //}
        get => life;    // 위 4줄과 같은 코드
        set
        {
            // value는 지금 set하는 값
            if (life > value)
            {
                // life가 감소한 상황 (새로운 값 (value)이 옛날 값(life)보다 작다 => 감소했다.)
                StartCoroutine(EnterinvnicibleMode());
            }

            life = value;
            if (life <= 0)  // 비교범위는 가능한 크게 잡는 쪽이 안전한다.
            {
                Dead();     // life가 0보다 작거나 같으면 죽는다.
            }
        }
        // int i = Life;    // i에다가 Life 값을 가져와서 넣어라 => Life의 get이 실행된다. i = Life; 와 같은 실행 결과가 된다.
        // Life = 3;        // Life에 3을 넣어라 => Life의 set이 실행된다. life = 3;과 같은 실행결과
    }

    //함수(메서드) ---------------------------------------------------------------------------------------------

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerEnter2D");      // trigger에 들어갔을때 실행 
        if (collision.CompareTag("PowerUp"))
        {
            Power++;
            Destroy(collision.gameObject);
        }
    }
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
            for (int i = 0; i < firePositionRoot.childCount; i++)
            {
                //bullet이라는 프리팹을 firePosition[i]의 위치에 (0,0,0)회전으로 만들어라
                //GameObject obj = Instantiate(Bullet, firePosition[i].position, Quaternion.identity);

                //bullet이라는 프리팹을 firePosition[i]의 위치에 firePosition[i]회전으로 만들어라
                GameObject obj = Instantiate(BulletPrefab, firePositionRoot.GetChild(i).position, firePositionRoot.GetChild(i).rotation);

                //Instantiate(생성할 프리팹); //프리팹이 0,0,0위치에 0,0,0회전에 1,1,1 스케일로 만들어짐
                //Instantiate(생성할 프리팹, 생성할 위치, 생성될 때의 회전)

                //총알의 외전 값으로 firePosition[i]의 회전값을 그대로 사용한다
                //obj.transform.rotation = firePosition[i].rotation;

                //Vector3 angle = firePosition[i].rotation.eulerAngles;
                //현재 회전 값을 x, y, z축별로 몇도씩 회전 했는지 확인 가능
                //Quaternion.Euler(10, 20, 30); //x축으로 10도, y축으로 20도, z축으로 30도 회전하는 코드
            }
            flash.SetActive(true);
            StartCoroutine(Flashoff());
            yield return new WaitForSeconds(fireInterval);
        }
    }

    IEnumerator Flashoff()
    {
        yield return new WaitForSeconds(0.1f);
        flash.SetActive(false); 
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
