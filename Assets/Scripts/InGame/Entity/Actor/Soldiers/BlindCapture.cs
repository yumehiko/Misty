﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 盲目な捕獲アクション。誰であれ重なったら捕獲する。
/// </summary>
public class BlindCapture : Capture
{
    protected override void CheckTargetTouch()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);

        //触れた対象が1 == 自分だけなら、False。
        if (colliders.Length == 1)
        {
            return;
        }

        foreach (Collider2D collider in colliders)
        {
            //自分自身はスルー。
            if (collider.gameObject == gameObject)
            {
                continue;
            }

            //触れたものが自分自身でないなら、なんであれ、捕獲。
            TryCaptureTarget(collider.gameObject);
        }

        return;
    }
}
