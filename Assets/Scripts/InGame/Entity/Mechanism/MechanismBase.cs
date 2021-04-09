using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public abstract class MechanismBase : MonoBehaviour
{
    [SerializeField] protected List<SwitchBase> switches = default;
    /// <summary>
    /// 現在Onになっている接続されたswitchの総数（動力）
    /// </summary>
    protected int power = 0;

    /// <summary>
    /// 現在メカニズムがOnかどうか。
    /// </summary>
    protected bool isOn = false;

    private void Awake()
    {
        RegisterSwitches();
    }

    /// <summary>
    /// 連動するスイッチを登録する。
    /// </summary>
    protected void RegisterSwitches()
    {
        foreach (SwitchBase switchBase in switches)
        {
            switchBase.SwitchEvent.Subscribe(isOn => ChangePowerValue(isOn));
        }
    }

    /// <summary>
    /// 有効な動力源数を追加/削減し、動力状態を確認する。
    /// </summary>
    protected void ChangePowerValue(bool isAdd)
    {
        power += isAdd ? 1 : -1;

        if(power > 0 && !isOn)
        {
            isOn = true;
            ActiveMachanism(); 
            return;
        }

        if(power <= 0 && isOn)
        {
            power = 0;
            isOn = false;
            DeActiveMechanism();
            return;
        }
    }

    /// <summary>
    /// メカニズムを起動する。
    /// </summary>
    protected abstract void ActiveMachanism();

    /// <summary>
    /// メカニズムを停止する。
    /// </summary>
    protected abstract void DeActiveMechanism();
}
