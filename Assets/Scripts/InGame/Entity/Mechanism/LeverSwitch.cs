using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class LeverSwitch : SwitchBase
{
    [SerializeField] private Touchable touchable = default;
    [SerializeField] private Interactable interactable = default;
    [SerializeField] private Animator animator = default;
    private int aKeyIsOn = default;

    /// <summary>
    /// レバーがOnか。
    /// </summary>
    [SerializeField] private bool isOn = false;

    private void Awake()
    {
        InitSetting();
    }

    /// <summary>
    /// 初期設定。
    /// </summary>
    private void InitSetting()
    {
        aKeyIsOn = Animator.StringToHash("IsOn");

        touchable.OnTouchEnter.Subscribe(toucher => interactable.RegisterToInteractor(toucher));
        touchable.OnTouchExit.Subscribe(toucher => interactable.RemoveFromInteractor(toucher));
        interactable.OnInteract.Subscribe(_ => SwitchLever(!isOn));

        //初期状態でOnなら、イベントを発行。
        if (isOn)
        {
            DoSwitchEvent();
        }
    }

    /// <summary>
    /// レバーの状態を指定する。
    /// </summary>
    private void SwitchLever(bool switchTo)
    {
        isOn = switchTo;
        DoSwitchEvent();
    }

    /// <summary>
    /// 状態に応じたイベントを発行。
    /// </summary>
    private void DoSwitchEvent()
    {
        switchEvent.OnNext(isOn);
        animator.SetBool(aKeyIsOn, isOn);
    }
}
