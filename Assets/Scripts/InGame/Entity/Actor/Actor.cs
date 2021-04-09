using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public abstract class Actor : MonoBehaviour
{
    private void Awake()
    {
        SubscribeTurnManager();
    }

    /// <summary>
    /// ターンマネージャーのターンイベントを購読し、時間の流れを受け取る。
    /// </summary>
    protected void SubscribeTurnManager()
    {
        TurnManager turnManager = GameObject.FindWithTag("LevelManager").GetComponent<TurnManager>();
        turnManager.OnTurnStart.Subscribe(_ => TurnStart());
        turnManager.OnTurnEnd.Subscribe(_ => TurnEnd());
    }

    /// <summary>
    /// ターン開始時。
    /// </summary>
    protected abstract void TurnStart();

    /// <summary>
    /// ターン終了時。
    /// </summary>
    protected abstract void TurnEnd();
}
