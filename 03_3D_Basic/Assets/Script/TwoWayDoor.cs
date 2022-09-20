using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class TwoWayDoor : Door
{

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Vector3 playerToDoor = transform.position - other.transform.position;
            if (Vector3.Angle(transform.forward, playerToDoor) > 90.0f)
            {
                anim.SetTrigger("OpeninFront");
            }
            else
            {
                anim.SetTrigger("OpeninBack");
            }


        }
    }


    

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("Close");
        }
    }


    public virtual void Open()
    {
        anim.SetTrigger("OpeninFront");
    }


    public virtual void Close()
    {
        anim.SetTrigger("Close");
    }

}
