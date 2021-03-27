using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// このゲームオブジェクトを動かす。
/// </summary>
public class Movement
{
    private TurnAct turnAct = default;
    private Transform transform = default;

    public Movement(TurnAct turnAct, Transform transform)
    {
        this.turnAct = turnAct;
        this.transform = transform;
    }

    /// <summary>
    /// 現在の位置から、指定方向に1単位進む。
    /// </summary>
    /// <param name="point"></param>
    public void MoveToDirection(ActorDirection direction, float duration)
    {
        Vector2 angle = DirectionToVector2(direction);

        float unit = 1.28f;
        turnAct.ActTweener = transform.DOMove(angle * unit, duration)
            .SetRelative()
            .OnComplete(() => turnAct.OnActComplete());
    }

    /// <summary>
    /// ActorDirectionをVector2方向成分へ変換。
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static Vector2 DirectionToVector2(ActorDirection direction)
    {
        switch (direction)
        {
            case ActorDirection.Up:
                return Vector2.up;
            case ActorDirection.Down:
                return Vector2.down;
            case ActorDirection.Right:
                return Vector2.right;
            case ActorDirection.Left:
                return Vector2.left;
            default:
                return Vector2.zero;
        }
    }
}
