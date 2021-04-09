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
        TurnManager turnManager = GameObject.FindWithTag("LevelManager").GetComponent<TurnManager>();
        turnManager.TurnEvent.Subscribe(_ => TurnAction());
    }

    private void TurnAction()
    {
        ActStart();

        //プレイヤーを発見しているなら、毎ターン左へ歩く。
        if (detectPlayer.IsDiscovered)
        {
            movement.MoveToDirection(ActorDirection.Left, TurnManager.TurnBaseTime);
            return;
        }


        //そうでないなら、単に無為に過ごす。
        DOVirtual.DelayedCall(TurnManager.TurnBaseTime, () => ActEnd());
    }
}
