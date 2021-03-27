using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum ActorDirection
{
    None,
    Up,
    Down,
    Right,
    Left
}

/// <summary>
/// 顔の向く方向。
/// </summary>
public class FaceDirection
{
    private TurnAct turnAct = default;
    private Transform transform = default;

    public FaceDirection(TurnAct turnAct, Transform transform)
    {
        this.turnAct = turnAct;
        this.transform = transform;
    }

    public ActorDirection Direction { get; private set; } = ActorDirection.Up;

    /// <summary>
    /// 入力された方向へ向く。
    /// </summary>
    /// <param name="direction"></param>
    public void TurnToDirection(ActorDirection direction, float duration)
    {
        if (Direction == direction)
        {
            return;
        }

        Vector3 degree = new Vector3(0.0f, 0.0f, DirectionToDegree(direction));
        turnAct.ActTweener = transform.DORotate(degree, duration)
            .OnComplete(() => turnAct.OnActComplete());

        Direction = direction;
    }

    /// <summary>
    /// ActorDirectionをDegreeへ変換。
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static float DirectionToDegree(ActorDirection direction)
    {
        switch (direction)
        {
            case ActorDirection.Up:
                return 0.0f;
            case ActorDirection.Down:
                return 180.0f;
            case ActorDirection.Right:
                return 270.0f;
            case ActorDirection.Left:
                return 90.0f;
            default:
                return 0.0f;
        }
    }
}
