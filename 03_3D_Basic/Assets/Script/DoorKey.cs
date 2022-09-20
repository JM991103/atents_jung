using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : MonoBehaviour
{
    public TwoWayDoor target;

    private void OnCollisionEnter(Collision collision)
    {
        target.Open();
        Destroy(this.gameObject);
    }

}
