using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// インベントリ。アイテムを1つだけ保持できる。
/// </summary>
public class Inventory : MonoBehaviour
{
    private Item currentItem  = null;
    public bool HasItem => currentItem != null;

    /// <summary>
    /// 指定されたアイテムをインベントリに入れる。
    /// </summary>
    /// <param name="item"></param>
    public void ItemToInventory(Item item)
    {
        currentItem = item;
    }

    /// <summary>
    /// インベントリのアイテムを設置する。
    /// </summary>
    public void PlaceItem()
    {
        currentItem.PlaceOnGround(this);
        currentItem = null;
    }
}
