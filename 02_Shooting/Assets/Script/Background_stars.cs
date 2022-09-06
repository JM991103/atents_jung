using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_stars : Background
{
    SpriteRenderer sprite;

    protected override void Awake()
    {
        base.Awake();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        minusX();
        
    }

    public override void minusX()
    {
        base.minusX();
        int rand = Random.Range(0, 4);
        sprite.flipX = ((rand & 0b_01) != 0);
        sprite.flipY = ((rand & 0b_10) != 0);

    }

}
