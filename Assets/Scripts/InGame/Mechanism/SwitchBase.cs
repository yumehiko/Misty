using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 機構のスイッチ部のベースクラス。
/// </summary>
public abstract class SwitchBase : MonoBehaviour
{
    protected Subject<bool> switchEvent = new Subject<bool>();

    /// <summary>
    /// スイッチがOnかOffに切り替わったとき、OnNextを発行。
    /// </summary>
    public System.IObservable<bool> SwitchEvent => switchEvent;
}
