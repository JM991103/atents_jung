using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpike : TrapBase
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected override void TrapActivate(GameObject other)
    {
        anim.SetTrigger("SpikesUp");
    }
}
