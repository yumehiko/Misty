using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class LeverSwitch : SwitchBase
{
    [SerializeField] private Touchable touchable = default;
    [SerializeField] private Animator animator = default;
    private int aKeyCanInteract = default;
    private int aKeyIsOn = default;

    /// <summary>
    /// レバーがOnか。
    /// </summary>
    [SerializeField] private bool isOn = false;

    private void Awake()
    {
        aKeyCanInteract = Animator.StringToHash("CanInteract");
        aKeyIsOn = Animator.StringToHash("IsOn");

        touchable.IsTouch.Subscribe(isTouch => ChangeTouchStats(isTouch));
        touchable.InteractEvent.Subscribe(_ => SwitchLever(!isOn));

        //初期状態でOnなら、イベントを発行。
        if(isOn)
        {
            DoSwitchEvent();
        }
    }

    /// <summary>
    /// 接触状態を変更。
    /// </summary>
    private void ChangeTouchStats(bool isTouch)
    {
        animator.SetBool(aKeyCanInteract, isTouch);
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
