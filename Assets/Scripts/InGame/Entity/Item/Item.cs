using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private Touchable touchable = default;

    private void Awake()
    {
        touchable.InteractEvent.Subscribe(interactor => TryPickUpItem(interactor));
    }

    /// <summary>
    /// このアイテムをインベントリに入れる。
    /// </summary>
    /// <param name="interactor"></param>
    private void TryPickUpItem(Interactor interactor)
    {
        Inventory inventory = interactor.GetComponent<Inventory>();

        if(inventory == null)
        {
            return;
        }

        if(inventory.HasItem)
        {
            inventory.PlaceItem();
        }

        inventory.ItemToInventory(this);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// このアイテムを指定の座標に配置する。
    /// </summary>
    public abstract void PlaceOnGround(Inventory inventory);
}
