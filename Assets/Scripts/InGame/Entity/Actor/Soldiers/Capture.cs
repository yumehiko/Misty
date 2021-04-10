using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーが重なったら、捕獲する。
/// </summary>
public class Capture : MonoBehaviour
{
    /// <summary>
    /// ターゲットに触れているか。
    /// </summary>
    public virtual bool CheckTargetTouch()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);

        //触れた対象が1 == 自分だけなら、False。
        if(colliders.Length == 1)
        {
            return false;
        }

        foreach (Collider2D collider in colliders)
        {
            //自分自身はスルー。
            if(collider.gameObject == gameObject)
            {
                continue;
            }

            //触れたものがPlayerがだったなら、捕獲。
            if (collider.gameObject.CompareTag("Player"))
            {
                if(TryCaptureTarget(collider.gameObject))
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// 対象の捕獲を試行する。捕獲できたらTrue。
    /// </summary>
    /// <param name="target"></param>
    protected bool TryCaptureTarget(GameObject target)
    {
        Capturable capturable = target.GetComponent<Capturable>();
        if(capturable == null)
        {
            return false;
        }

        capturable.BeCaptured();
        return true;

    }
}
