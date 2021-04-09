using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

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

    private Subject<Unit> turnEvent = new Subject<Unit>();
    public System.IObservable<Unit> TurnEvent => turnEvent;

    /// <summary>
    /// ターンを進める。
    /// </summary>
    public void AdvanceTurn()
    {
        turnCount++;
        turnEvent.OnNext(Unit.Default);
    }
}
