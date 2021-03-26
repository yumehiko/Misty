using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;

public enum ActorDirection
{
    None,
    Up,
    Down,
    Right,
    Left
}

/// <summary>
/// このゲームオブジェクトを動かす。
/// </summary>
public class Movement : MonoBehaviour
{
    private Subject<Unit> moveCompleteEvent = new Subject<Unit>();
    public System.IObservable<Unit> MoveCompleteEvent => moveCompleteEvent;
    public ActorDirection Direction /*{ get; private set; }*/ = ActorDirection.Up;

    private Tweener moveTween = default;

    /// <summary>
    /// 現在の位置から、指定方向に1単位進む。
    /// </summary>
    /// <param name="point"></param>
    public void MoveToDirection(ActorDirection direction)
    {
        if(IsMotioning())
        {
            return;
        }

        //向きが違うなら、まず向きだけを変える。
        if(Direction != direction)
        {
            TurnToDirection(direction);
            return;
        }

        Vector2 angle = DirectionToVector2(direction);

        float unit = 1.0f;
        moveTween = transform.DOMove(angle * unit, 0.2f)
            .SetRelative()
            .OnComplete(() => OnMoveComplete());
    }

    /// <summary>
    /// 入力された方向へ向く。
    /// </summary>
    /// <param name="direction"></param>
    public void TurnToDirection(ActorDirection direction)
    {
        if(Direction == direction)
        {
            return;
        }

        Vector3 degree = new Vector3(0.0f, 0.0f, DirectionToDegree(direction));
        moveTween = transform.DORotate(degree, 0.1f)
            .OnComplete(() => OnMoveComplete());

        Direction = direction;
    }

    /// <summary>
    /// ActorDirectionからVector2方向成分を返す。
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

    /// <summary>
    /// ActorDirectionからDegreeを返す。
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

    /// <summary>
    /// 動作中か。
    /// </summary>
    /// <returns></returns>
    public bool IsMotioning()
    {
        return moveTween.IsActive();
    }

    /// <summary>
    /// 移動を完了した時。
    /// </summary>
    private void OnMoveComplete()
    {
        moveTween.Kill();
        moveCompleteEvent.OnNext(Unit.Default);
    }
}
