using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_stars : Background
{
    SpriteRenderer[] sprite;

    protected override void Awake()
    {
        base.Awake();

        sprite = new SpriteRenderer[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            sprite[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }
    }

    public override void MoveRightEnd(int index)
    {
        base.MoveRightEnd(index);

        int rand = UnityEngine.Random.Range(0, 4);
        sprite[index].flipX = ((rand & 0b_01) != 0);
        sprite[index].flipY = ((rand & 0b_10) != 0);

    }

}
