using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 邪眼でみられたとき、反応して視界イベントを発行する。
/// </summary>
public class SeeTarget : MonoBehaviour
{
    private EvilSightManager evilSightManager;

    private BoolReactiveProperty isSeeing = new BoolReactiveProperty();
    /// <summary>
    /// スイッチがOnかOffに切り替わったとき、OnNextを発行。
    /// </summary>
    public IReadOnlyReactiveProperty<bool> IsSeeing => isSeeing;

    public bool SetSeeing = false;

    private void Awake()
    {
        RegisterEvilSights();
    }

    /// <summary>
    /// 邪眼の干渉状態が変化したかを確認する。
    /// </summary>
    private void CheckSeeingChange()
    {
        if (SetSeeing != isSeeing.Value)
        {
            isSeeing.Value = SetSeeing;
        }

        SetSeeing = false;
    }

    /// <summary>
    /// シーン上の邪眼者に自身を対象として登録。
    /// </summary>
    private void RegisterEvilSights()
    {
        evilSightManager = GameObject.FindWithTag("LevelManager").GetComponent<EvilSightManager>();
        evilSightManager.AddSeeTarget(this);
        evilSightManager.OnRefleshSeeTarget
            //.DelayFrame(1)
            .Subscribe(_ => CheckSeeingChange());
    }
}
