using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 鏡。配置でき、邪眼視界を持つ。
/// </summary>
public class Mirror : Item
{
    [SerializeField] private Animator animator = default;
    private int aKeyCanInteract;

    [SerializeField] private FaceDirection faceDirection = default;

    protected override void InitSetting()
    {
        touchable.OnTouchEnter.Subscribe(toucher => OnTouchEnter(toucher));
        touchable.OnTouchExit.Subscribe(toucher => OnTouchExit(toucher));
        interactable.OnInteract.Subscribe(interactor => TryPickUpItem(interactor));
        onPlace.Subscribe(inventory => SetFaceDirection(inventory));
        aKeyCanInteract = Animator.StringToHash("CanInteract");
    }

    /// <summary>
    /// 鏡の向きを配置者と同じ向きに設定する。
    /// </summary>
    /// <param name="inventory"></param>
    private void SetFaceDirection(Inventory inventory)
    {
        FaceDirection faceDirection = inventory.GetComponent<FaceDirection>();
        this.faceDirection.TurnToDirection(faceDirection.Direction, 0.0f);
    }

    /// <summary>
    /// 触れたとき。
    /// </summary>
    /// <param name="toucher"></param>
    private void OnTouchEnter(Toucher toucher)
    {
        interactable.RegisterToInteractor(toucher);
        animator.SetBool(aKeyCanInteract, true);
    }

    /// <summary>
    /// 離れたとき。
    /// </summary>
    /// <param name="toucher"></param>
    private void OnTouchExit(Toucher toucher)
    {
        interactable.RemoveFromInteractor(toucher);
        animator.SetBool(aKeyCanInteract, false);
    }
}
