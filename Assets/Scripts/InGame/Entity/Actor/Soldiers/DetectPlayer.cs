using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// プレイヤーまで視線が通るかを確認し、通るならイベントを発行。
/// </summary>
public class DetectPlayer : MonoBehaviour
{
    /// <summary>
    /// プレイヤーの発見状態。
    /// </summary>
    public bool IsDiscovered { get; private set; } = false;

    public Transform PlayerTransform { get; private set; } = default;

    /// <summary>
    /// 視界を遮るレイヤー。
    /// </summary>
    [SerializeField] private LayerMask sightMask = default;

    /// <summary>
    /// ビックリマークUI。
    /// </summary>
    [SerializeField] private Image exclamationMark = default;

    [SerializeField] private ActorAnimeController actorAnime = default;

    private void Awake()
    {
        PlayerTransform = GameObject.FindWithTag("Player").transform;
    }

    /// <summary>
    /// プレイヤーを発見できるか確認する。
    /// </summary>
    public void DetectionPlayer()
    {
        if(IsDiscovered)
        {
            return;
        }

        IsDiscovered = AppearCheck(transform.position, PlayerTransform.position);

        //見つけたとき。
        if (IsDiscovered)
        {
            exclamationMark.color = Color.white;
            exclamationMark.DOFade(0.0f, 0.5f);
            actorAnime.SkeletonFlip(XDiffToDirection(transform.position, PlayerTransform.position));
        }
    }

    /// <summary>
    /// 視界が対象まで繋がるかチェック。
    /// </summary>
    /// <returns>到達するならtrue</returns>
    private bool AppearCheck(Vector2 myPosition, Vector2 targetPosition)
    {
        RaycastHit2D hit = Physics2D.Linecast(myPosition, targetPosition, sightMask);
        return hit.collider == null;
    }

    /// <summary>
    /// X座標の差からActorDirectionの左右どちらかを算出。
    /// </summary>
    /// <param name="startPosition"></param>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    public static ActorDirection XDiffToDirection(Vector2 startPosition, Vector2 targetPosition)
    {
        if(startPosition.x >= targetPosition.x)
        {
            return ActorDirection.Left;
        }

        return ActorDirection.Right;
    }
}
