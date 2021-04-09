using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

/// <summary>
/// キャラクターのAction（演技）の管理。
/// </summary>
public class Actor : MonoBehaviour
{
    protected Subject<Unit> onActStart = new Subject<Unit>();
    /// <summary>
    /// ターン動作開始時。
    /// </summary>
    public System.IObservable<Unit> OnActStart => onActStart;

    protected Subject<Unit> onActEnd = new Subject<Unit>();
    /// <summary>
    /// ターン動作終了時。
    /// </summary>
    public System.IObservable<Unit> OnActEnd => onActEnd;

    public Tweener ActTweener = default;

    /// <summary>
    /// 動作中か。
    /// </summary>
    /// <returns></returns>
    public bool IsActing => ActTweener.IsActive();

    /// <summary>
    /// 動作開始時。
    /// </summary>
    public void ActStart()
    {
        onActStart.OnNext(Unit.Default);
    }

    /// <summary>
    /// 動作を完了した時。
    /// </summary>
    public void ActEnd()
    {
        ActTweener.Kill();
        onActEnd.OnNext(Unit.Default);
    }

}
