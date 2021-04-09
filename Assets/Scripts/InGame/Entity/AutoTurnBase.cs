using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// ターン経過時に、自動的にアクションを実行する。
/// </summary>
public abstract class AutoTurnBase : MonoBehaviour
{
    private void Awake()
    {
        SubscribeManager();
    }

    /// <summary>
    /// TurnManagerと連携し、イベントを購読する。
    /// </summary>
    protected void SubscribeManager()
    {
        TurnManager turnManager = GameObject.FindWithTag("LevelManager").GetComponent<TurnManager>();
        turnManager.TurnEvent.Subscribe(_ => AutoTurnAction());
    }

    protected abstract void AutoTurnAction();
}
