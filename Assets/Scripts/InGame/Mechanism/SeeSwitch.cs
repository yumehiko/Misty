using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SeeSwitch : SwitchBase
{
    [SerializeField] private SpriteRenderer spriteRenderer = default;
    [SerializeField] private SeeTarget seeTarget = default;

    private void Awake()
    {
        seeTarget.SeeEvent.Subscribe(isSeeing => OnSeeing(isSeeing));
    }

    /// <summary>
    /// 邪眼の影響状況が変わったとき、スイッチ切り替えイベントを発行する。
    /// </summary>
    /// <param name="isSeeing"></param>
    private void OnSeeing(bool isSeeing)
    {
        if (isSeeing)
        {
            spriteRenderer.color = Color.red;
            switchEvent.OnNext(true);
        }
        else
        {
            spriteRenderer.color = Color.white;
            switchEvent.OnNext(false);
        }
    }
}
