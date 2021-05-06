using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// インタラクト可能な対象
/// </summary>
public class Interactable : MonoBehaviour
{
    [SerializeField] private Animator animator = default;
    private int aKeyCanInteract;

    private Subject<Interactor> onInteract = new Subject<Interactor>();
    /// <summary>
    /// インタラクトしたとき。
    /// </summary>
    public System.IObservable<Interactor> OnInteract => onInteract;

    private void Awake()
    {
        InitSetting();
    }

    /// <summary>
    /// 初期設定。
    /// </summary>
    private void InitSetting()
    {
        aKeyCanInteract = Animator.StringToHash("CanInteract");
    }

    /// <summary>
    /// 対象がInteractorなら、対象に自身を登録する。
    /// </summary>
    /// <param name="interactor"></param>
    public void RegisterToInteractor(Toucher toucher)
    {
        Interactor interactor = toucher.GetComponent<Interactor>();
        if(interactor == null)
        {
            return;
        }

        interactor.SetTarget(this);
        animator.SetBool(aKeyCanInteract, true);
    }

    /// <summary>
    /// ToucherのInteract対象を削除する。
    /// </summary>
    /// <param name="toucher"></param>
    public void RemoveFromInteractor(Toucher toucher)
    {
        Interactor interactor = toucher.GetComponent<Interactor>();
        if (interactor == null)
        {
            return;
        }

        interactor.RemoveTarget();
        animator.SetBool(aKeyCanInteract, false);
    }

    /// <summary>
    /// インタラクトを実行する。
    /// </summary>
    public void DoInteract(Interactor interactor)
    {
        onInteract.OnNext(interactor);
    }
}
