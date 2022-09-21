using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapStiky : TrapBase
{
    // 들어오면 몇초동안 이동속도가 10%로 느려짐
    public float speedDebuff = 0.1f;
    public float duration = 3.0f;

    float originalSpeed = 0.0f;
    Player player = null;

    protected override void TrapActivate(GameObject target)
    {
        if(player == null)
        {
            Debug.Log("함정 발동");
            player = target.GetComponent<Player>();
            originalSpeed = player.moveSpeed;
            player.moveSpeed *= speedDebuff;
        }
        else
        {
            Debug.Log("디버프 해제 초기화");
            StopAllCoroutines();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player != null)
            {
                Debug.Log("디버프 영역 해제");
                StartCoroutine(RelesaseDebuff());
            }
        }
    }

    IEnumerator RelesaseDebuff()
    {
        Debug.Log("3초후 디버프 해제");
        yield return new WaitForSeconds(duration);
        player.moveSpeed = originalSpeed;
        originalSpeed = 0.0f;
        player = null;
    }
}
