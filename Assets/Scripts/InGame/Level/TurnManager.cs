using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

/// <summary>
/// レベルのエンティティのターンを管理する。
/// </summary>
public class TurnManager : MonoBehaviour
{
    /// <summary>
    /// 経過ターン数。
    /// </summary>
    [SerializeField] private int turnCount = 0;

    /// <summary>
    /// 1ターンにかかる基本時間。
    /// </summary>
    public static readonly float TurnBaseTime = 0.2f;

    private Subject<Unit> onTurnStart = new Subject<Unit>();
    /// <summary>
    /// ターン開始時。
    /// </summary>
    public System.IObservable<Unit> OnTurnStart => onTurnStart;

    private Subject<Unit> onTurnEnd = new Subject<Unit>();
    /// <summary>
    /// ターン終了時。
    /// </summary>
    public System.IObservable<Unit> OnTurnEnd => onTurnEnd;

    /// <summary>
    /// ターン実行中のTween。
    /// </summary>
    public Tween TurnTween { get; private set; } = default;

    /// <summary>
    /// ターン動作が実行中か。
    /// </summary>
    public bool IsActing => TurnTween.IsActive();

    /// <summary>
    /// ターンを進める。
    /// </summary>
    public void TurnStart(float duration)
    {
        turnCount++;
        onTurnStart.OnNext(Unit.Default);
        TurnTween = DOVirtual.DelayedCall(duration, () => TurnEnd());
    }

    /// <summary>
    /// ターンを終了する。
    /// </summary>
    private void TurnEnd()
    {
        //ここでKillするのは、先行入力を通すため。
        TurnTween.Kill();
        onTurnEnd.OnNext(Unit.Default);
    }
}
