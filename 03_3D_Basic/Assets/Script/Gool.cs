using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gool : MonoBehaviour
{
    public Action<bool> start;

    bool gool = false;
    
        private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gool = false;
            start?.Invoke(gool);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gool = true;
            start?.Invoke(gool);
        }
    }
}
