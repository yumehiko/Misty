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

    private void Awake()
    {
        TurnManager turnManager = TurnManager.GetTurnManager();

        //経路探索タイミングを購読。
        turnManager.OnPathFind
            .Where(_ => CanAction)
            .Where(_ => detectPlayer.IsDiscovered)
            .Subscribe(_ => seeker.StartPath(
                transform.position,
                detectPlayer.PlayerTransform.position,
                (path) => TryMoveTowardPoint(path)));
    }

    /// <summary>
    /// 経路から、次のマスを探して、可能ならそこへステップ。
    /// </summary>
    private void TryMoveTowardPoint(Path path)
    {
        if(path == null)
        {
            Debug.Log("PathNull");
            return;
        }

        if(path.path.Count < 2)
        {
            return;
        }

        movement.StepToPosition((Vector3)path.path[1].position, TurnManager.TurnBaseTime);
    }
}
