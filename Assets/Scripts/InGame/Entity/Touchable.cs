using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 触れて、インタラクトできるもの。それぞれのイベントを発行する。
/// TODO: Colliderでよくないか？　でも触れても反応したくないものはどうしよう？ tag？
/// </summary>
public class Touchable : MonoBehaviour
{
    private Subject<Toucher> onTouchEnter = new Subject<Toucher>();
    private Subject<Toucher> onTouchExit = new Subject<Toucher>();

    /// <summary>
    /// Toucherが触れたとき。
    /// </summary>
    public System.IObservable<Toucher> OnTouchEnter => onTouchEnter;

    /// <summary>
    /// Toucherが離れたとき。
    /// </summary>
    public System.IObservable<Toucher> OnTouchExit => onTouchExit;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Toucher toucher = collision.GetComponent<Toucher>();
        if (toucher == null)
        {
            return;
        }

        onTouchEnter.OnNext(toucher);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Toucher toucher = collision.GetComponent<Toucher>();
        if (toucher == null)
        {
            return;
        }

        onTouchExit.OnNext(toucher);
    }
}
