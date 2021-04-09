using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Actorがインタラクト対象を管理し、必要ならインタラクトする。
/// </summary>
public class Interactor : MonoBehaviour
{
    /// <summary>
    /// インタラクト対象。
    /// </summary>
    private Touchable target = null;

    /// <summary>
    /// ターゲットをセット。
    /// </summary>
    /// <param name="newTarget"></param>
    public void SetTarget(Touchable newTarget)
    {
        target = newTarget;
    }

    /// <summary>
    /// ターゲットを削除。
    /// </summary>
    public void RemoveTarget()
    {
        target = null;
    }

    /// <summary>
    /// インタラクト可能な対象があるか（触れているか）。
    /// </summary>
    /// <returns></returns>
    public bool HasTouchable()
    {
        return target != null;
    }

    /// <summary>
    /// 可能なら、インタラクトを実行する。
    /// </summary>
    public void TryInteract()
    {
        if(target == null)
        {
            return;
        }

        target.DoInteract();
    }
}
