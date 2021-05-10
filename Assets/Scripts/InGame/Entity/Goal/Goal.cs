using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Goal : MonoBehaviour
{
    [SerializeField] private Touchable touchable = default;
    [SerializeField] private Interactable interactable = default;
    [SerializeField] private Animator animator = default;
    private int aKeyDoGoal = default;
    private int aKeyCanInteract;

    private void Awake()
    {
        InitSetting();
    }

    /// <summary>
    /// 初期設定。
    /// </summary>
    private void InitSetting()
    {
        aKeyDoGoal = Animator.StringToHash("DoGoal");
        aKeyCanInteract = Animator.StringToHash("CanInteract");

        touchable.OnTouchEnter.Subscribe(toucher => OnTouchEnter(toucher));
        touchable.OnTouchExit.Subscribe(toucher => OnTouchExit(toucher));
        interactable.OnInteract.Subscribe(_ => GoalLevel());
    }

    /// <summary>
    /// このレベルをゴールする。
    /// </summary>
    private void GoalLevel()
    {
        Debug.Log("Goal");
        animator.SetTrigger(aKeyDoGoal);
        var levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
        levelManager.BeatThisLevel();
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
