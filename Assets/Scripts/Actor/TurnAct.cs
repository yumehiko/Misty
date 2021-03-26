using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;

/// <summary>
/// ターンごとの行動の、開始と終了の判定を行う。
/// </summary>
public class TurnAct : MonoBehaviour
{
    private Subject<Unit> actCompleteEvent = new Subject<Unit>();
    public System.IObservable<Unit> ActCompleteEvent => actCompleteEvent;

    public Tweener ActTweener = default;


    /// <summary>
    /// 動作を完了した時。
    /// </summary>
    public void OnActComplete()
    {
        ActTweener.Kill();
        actCompleteEvent.OnNext(Unit.Default);
    }

    /// <summary>
    /// 動作中か。
    /// </summary>
    /// <returns></returns>
    public bool IsActing()
    {
        return ActTweener.IsActive();
    }
}
