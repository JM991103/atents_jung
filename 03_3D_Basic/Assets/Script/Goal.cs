using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public Action<bool> start;

    bool goal = false;

    ParticleSystem ps;

    private void Awake()
    {
        ps = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            goal = false;
            start?.Invoke(goal);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ps.Play();
            goal = true;
            start?.Invoke(goal);
        }
    }
}
