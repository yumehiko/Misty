﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


/// <summary>
/// ターンの経過に応じてActionを実行するEntity。
/// </summary>
public abstract class TurnActor : MonoBehaviour
{
    /// <summary>
    /// ターン行動ができるか。
    /// </summary>
    protected bool canTurnAction = true;

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
    /// ターン開始時に実行するAction。
    /// TurnManagerのイベントをフックにして呼び出される。
    /// </summary>
    protected abstract void TurnStart();

    /// <summary>
    /// ターン終了時に実行するAction。
    /// TurnManagerのイベントをフックにして呼び出される。
    /// </summary>
    protected abstract void TurnEnd();
}