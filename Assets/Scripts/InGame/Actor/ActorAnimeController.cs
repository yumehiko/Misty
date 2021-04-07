using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

/// <summary>
/// ActorのSkeletonアニメーションを管理・実行する。
/// </summary>
public class ActorAnimeController : MonoBehaviour
{
    [SerializeField] private SkeletonMecanim skeletonMecanim = default;
    [SerializeField] private Animator animator = default;

    private int aKeyDoStep = default;

    private void Awake()
    {
        aKeyDoStep = Animator.StringToHash("DoStep");
    }

    /// <summary>
    /// スケルトンの向きを反映する。
    /// </summary>
    public void SkeletonFlip(ActorDirection direction)
    {
        if (direction == ActorDirection.Right)
        {
            skeletonMecanim.skeleton.ScaleX = 1.0f;
            return;
        }

        if (direction == ActorDirection.Left)
        {
            skeletonMecanim.skeleton.ScaleX = -1.0f;
            return;
        }
    }

    /// <summary>
    /// ステップアニメーションを実行する。
    /// </summary>
    public void StepAnime()
    {
        animator.SetTrigger(aKeyDoStep);
    }
}
