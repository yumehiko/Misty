using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 感圧版。Toucherが触れると反応し、Switchをonにする。
/// </summary>
public class PressurePlate : SwitchBase
{
    [SerializeField] private Touchable touchable = default;
    [SerializeField] private Animator animator = default;
    private int aKeyIsOn = default;

    /// <summary>
    /// 現在乗っているToucherの数。
    /// </summary>
    private int pressureCount = 0;

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
        touchable.OnTouchEnter.Subscribe(_ => PressureOn());
        touchable.OnTouchExit.Subscribe(_ => PressureOff());
    }

    /// <summary>
    /// 感圧版に何かが乗ったとき。
    /// </summary>
    private void PressureOn()
    {
        if(pressureCount == 0)
        {
            switchEvent.OnNext(true);
            animator.SetBool(aKeyIsOn, true);
        }
        pressureCount++;
    }

    private void PressureOff()
    {
        pressureCount--;
        if (pressureCount <= 0)
        {
            pressureCount = 0;
            switchEvent.OnNext(false);
            animator.SetBool(aKeyIsOn, false);
        }
    }
}
