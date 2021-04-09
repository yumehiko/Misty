using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Spine.Unity;

/// <summary>
/// 指定されたTransformを動かす。
/// </summary>
public class Movement : MonoBehaviour
{
    [SerializeField] private LayerMask moveMask;
    [SerializeField] private ActorAnimeController animeController;


    /// <summary>
    /// 現在の位置から、指定方向に1単位進む。
    /// </summary>
    /// <param name="point"></param>
    public void MoveToDirection(ActorDirection direction, float duration)
    {
        const float unit = 0.96f;
        Vector2 angle = DirectionToVector2(direction);

        if(!CheckCanMove(angle))
        {
            //TODO ここで壁にぶつかるアニメーションとかを入れておく
            Debug.Log("CantMove");
            return;
        }

        animeController.SkeletonFlip(direction);
        animeController.StepAnime();

        _ = transform.DOMove(angle * unit, duration)
            .SetRelative();
    }

    /// <summary>
    /// 指定された座標に進めるかチェックする。
    /// </summary>
    /// <param name="moveOffset">現在地から目標地までの方向と距離。</param>
    /// <returns>進めるならTrue</returns>
    public bool CheckCanMove(Vector2 angle)
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, (Vector2)transform.position + angle, moveMask);
        return hit.collider == null;
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
