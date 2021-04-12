using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// プレイヤーが重なったら、捕獲する。
/// </summary>
public class Capture : MonoBehaviour
{
    [SerializeField] private Actor actor = default;

    private void Awake()
    {
        _ = TurnManager.GetTurnManager()
            .OnCapture.Subscribe(_ => CheckTargetTouch());
    }

    /// <summary>
    /// ターゲットに触れているか。
    /// </summary>
    protected virtual void CheckTargetTouch()
    {
        if(!actor.CanAction)
        {
            return;
        }

        Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);

        //触れた対象が1 == 自分だけなら、False。
        if(colliders.Length == 1)
        {
            return;
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
                TryCaptureTarget(collider.gameObject);
            }
        }
    }

    /// <summary>
    /// 対象の捕獲を試行する。捕獲できたらTrue。
    /// </summary>
    /// <param name="target"></param>
    protected void TryCaptureTarget(GameObject target)
    {
        Capturable capturable = target.GetComponent<Capturable>();
        if(capturable == null)
        {
            return;
        }

        capturable.BeCaptured();
        actor.CanAction = false;
    }
}
