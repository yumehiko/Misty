using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using Pathfinding;

/// <summary>
/// 兵士。プレイヤーを捕獲しようと接近するActor。
/// </summary>
public class Soldier : Actor
{
    [SerializeField] private Movement movement = default;
    [SerializeField] private DetectPlayer detectPlayer = default;
    [SerializeField] private Seeker seeker = default;
    [SerializeField] private Capture capture = default;

    /// <summary>
    /// ターン行動ができるか。
    /// </summary>
    private bool canTurnAction = true;

    private void Awake()
    {
        SubscribeTurnManager();
    }

    protected override void TurnStart()
    {
        if(!canTurnAction)
        {
            return;
        }

        //Playerを発見しているなら、経路を計算し、次の目標地点へステップ。
        if (detectPlayer.IsDiscovered)
        {
            seeker.StartPath(transform.position,
                detectPlayer.PlayerTransform.position,
                (path) => TryMoveTowardPoint(path));
        }

    }

    protected override void TurnEnd()
    {
        if (!canTurnAction)
        {
            return;
        }

        //プレイヤーと視界が繋がるかをチェック。
        detectPlayer.DetectionPlayer();

        //プレイヤーが重なっているなら、Capture
        canTurnAction = !capture.CheckTargetTouch();
    }

    /// <summary>
    /// 経路から、次のマスを探して、可能ならそこへステップ。
    /// </summary>
    private void TryMoveTowardPoint(Path path)
    {
        if(path.path.Count < 2)
        {
            return;
        }

        movement.StepToPosition((Vector3)path.path[1].position, TurnManager.TurnBaseTime);
    }
}
