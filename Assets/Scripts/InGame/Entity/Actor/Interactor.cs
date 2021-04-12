using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Actorがインタラクト対象を管理し、必要ならインタラクトする。
/// </summary>
public class Interactor : MonoBehaviour
{
    [SerializeField] private Inventory inventory = default;

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
    /// 可能なら、インタラクトを実行する。
    /// </summary>
    public void TryInteract()
    {
        if(target != null)
        {
            target.DoInteract(this);
            return;
        }

        if(inventory.HasItem)
        {
            inventory.PlaceItem();
            return;
        }

        //なにもインタラクトできなかったとき。音でもならす？
    }
}
