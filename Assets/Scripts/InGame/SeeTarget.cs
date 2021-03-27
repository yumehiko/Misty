using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 邪眼でみられたとき、反応する機構。
/// </summary>
public class SeeTarget : MonoBehaviour
{
    private Subject<bool> seeingEvent = new Subject<bool>();
    public System.IObservable<bool> SeeingEvent => seeingEvent;

    public bool IsSeeing = false;
    private bool prevSeeing = false;

    private void Awake()
    {
        RegisterEvilSights();
    }

    private void Update()
    {
        //TODO 多分Turn単位の処理に変えられる。
        CheckSeeingChange();
    }

    /// <summary>
    /// 邪眼の干渉状態が変化したかを確認する。
    /// </summary>
    private void CheckSeeingChange()
    {
        if (IsSeeing != prevSeeing)
        {
            seeingEvent.OnNext(IsSeeing);
            prevSeeing = IsSeeing;
        }

        IsSeeing = false;
    }

    /// <summary>
    /// シーン上の邪眼者に自身を登録。
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
