using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

/// <summary>
/// 兵士。プレイヤーを捕獲しようと接近するActor。
/// </summary>
public class Soldier : Actor
{
    [SerializeField] private Movement movement = default;
    [SerializeField] private DetectPlayer detectPlayer = default;

    private void Awake()
    {
        SubscribeTurnManager();
    }

    /// <summary>
    /// ターン開始時に実行。TurnManagerのイベントをフックにして呼び出される。
    /// </summary>
    protected override void TurnStart()
    {
        //プレイヤーを発見しているなら、毎ターン左へ歩く。
        if (detectPlayer.IsDiscovered)
        {
            movement.MoveToDirection(ActorDirection.Left, TurnManager.TurnBaseTime);
            return;
        }
    }

    /// <summary>
    /// ターン終了時に実行。TurnManagerのイベントをフックにして呼び出される。
    /// </summary>
    protected override void TurnEnd()
    {
        detectPlayer.DetectionPlayer();
    }
}
