using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 邪眼でみられたとき、反応して視界イベントを発行する。
/// </summary>
public class SeeTarget : MonoBehaviour
{
    protected Subject<bool> seeEvent = new Subject<bool>();

    /// <summary>
    /// スイッチがOnかOffに切り替わったとき、OnNextを発行。
    /// </summary>
    public System.IObservable<bool> SeeEvent => seeEvent;

    public bool IsSeeing = false;
    private bool prevSeeing = false;

    private void Awake()
    {
        RegisterEvilSights();
    }

    private void LateUpdate()
    {
        //TODO 多分Turn単位の処理に変えられる。
        //プレイヤーが移動を終えた時点で視界の状態を確認するとか……。
        //そしたらLateUpdateは使わなくてすむかも。
        CheckSeeingChange();
    }

    /// <summary>
    /// 邪眼の干渉状態が変化したかを確認する。
    /// </summary>
    private void CheckSeeingChange()
    {
        if (IsSeeing != prevSeeing)
        {
            seeEvent.OnNext(IsSeeing);
            prevSeeing = IsSeeing;
        }

        IsSeeing = false;
    }

    /// <summary>
    /// シーン上の邪眼者に自身を対象として登録。
    /// </summary>
    private void RegisterEvilSights()
    {
        GameObject[] evilSightObjects = GameObject.FindGameObjectsWithTag("EvilSight");
        foreach (GameObject evilSightObject in evilSightObjects)
        {
            EvilSight evilSight = evilSightObject.GetComponent<EvilSight>();
            if(evilSight == null)
            {
                continue;
            }

            evilSight.AddSeeTarget(this);
        }
    }
}
