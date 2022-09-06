using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Transform[] bgslots;
    public float scrollingSpeed = 2.5f;
    const float Background_Width = 13.6f;

    protected virtual void Awake()
    {
        bgslots = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)      // 정확한 인덱스가 필요할 때 유리
        {
            bgslots[i] = transform.GetChild(i);
        }
    }

    private void Update()
    {
        minusX();
    }

    public virtual void minusX()
    {
        float minusX = transform.position.x - Background_Width;
        foreach (Transform slot in bgslots)      //속도가 그냥 for보다 빠름
        {
            slot.Translate(scrollingSpeed * Time.deltaTime * -transform.right);

            if (slot.position.x < minusX)
            {
                // 오른쪽으로 Backgroung_Width의 3배(bgslot.Length에 3개가 들어있으니까)만큼 이동
                slot.Translate(Background_Width * bgslots.Length * transform.right);
            }
        }
    }
}
