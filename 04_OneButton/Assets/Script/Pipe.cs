
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    /// <summary>
    /// 랜덤 최소 높이
    /// </summary>
    public float minHeight;

    /// <summary>
    /// 랜덤 최대 높이
    /// </summary>
    public float maxHeight;

    /// <summary>
    /// 리지드바디 컴포넌트
    /// </summary>
    Rigidbody2D rigid;

    /// <summary>
    /// 플레이어가 파이프를 통화하면 실행 될 델리게이트
    /// </summary>
    public System.Action onScored;

    /// <summary>
    /// 랜덤한 위치를 반환하는 프로퍼티
    /// </summary>
    public float RandomHeight{ get => Random.Range(minHeight, maxHeight); }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();    // 리지드바디 찾기
    }

    //public void ResetRandomHeight()
    //{
    //    // 주의 : 위치를 옮기고 사용할 것
    //    Vector2 pos = Vector2.up * Random.Range(minHeight, maxHeight);
    //    rigid.MovePosition(rigid.position + pos);
    //}

    private void Start()
    {
        Vector2 pos = Vector2.up * RandomHeight;
        transform.Translate(pos);       // 게임 시작 할 때 첫 위치 랜덤으로 지정
        //rigid.MovePosition(rigid.position + pos);
    }

    /// <summary>
    /// 왼쪽으로 이동시키는 함수
    /// </summary>
    /// <param name="moveDelta">이동시킬 정도</param>
    public void moveLeft(float moveDelta)
    {
        // 현재 위치에서 moveDelta만큼 왼쪽으로 이동 
        rigid.MovePosition(rigid.position + moveDelta * Vector2.left);
    }

    /// <summary>
    /// 통과 체크 용도
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        // 플레이가 통과 했다면
        if (collision.CompareTag("Player"))
        {
            onScored?.Invoke(); // 델리게이트 실행으로 알리기
        }
    }

}
