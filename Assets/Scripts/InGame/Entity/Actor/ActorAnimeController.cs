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
    private int aKeyDirection = default;

    private void Awake()
    {
        aKeyDoStep = Animator.StringToHash("DoStep");
        aKeyDirection = Animator.StringToHash("Direction");
    }

    public void SetSkeletonDirection(ActorDirection direction)
    {
        if(direction == ActorDirection.Up)
        {
            animator.SetInteger(aKeyDirection, 2);
            skeletonMecanim.skeleton.ScaleX = 1.0f;
            return;
        }

        if (direction == ActorDirection.Down)
        {
            animator.SetInteger(aKeyDirection, 0);
            skeletonMecanim.skeleton.ScaleX = 1.0f;
            return;
        }

        if (direction == ActorDirection.Right)
        {
            animator.SetInteger(aKeyDirection, 1);
            skeletonMecanim.skeleton.ScaleX = 1.0f;
            return;
        }

        if(direction == ActorDirection.Left)
        {
            animator.SetInteger(aKeyDirection, 1);
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
