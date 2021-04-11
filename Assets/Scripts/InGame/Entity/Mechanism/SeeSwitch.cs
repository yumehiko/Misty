using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SeeSwitch : SwitchBase
{
    [SerializeField] private Animator animator = default;
    [SerializeField] private SeeTarget seeTarget = default;

    private void Awake()
    {
        seeTarget.IsSeeing
            .Skip(1)
            .Subscribe(isSeeing => OnSeeing(isSeeing));
    }

    /// <summary>
    /// 邪眼の影響状況が変わったとき、スイッチ切り替えイベントを発行する。
    /// </summary>
    /// <param name="isSeeing"></param>
    private void OnSeeing(bool isSeeing)
    {
        if (isSeeing)
        {
            animator.Play("On");
            switchEvent.OnNext(true);
        }
        else
        {
            animator.Play("Off");
            switchEvent.OnNext(false);
        }
    }
}
