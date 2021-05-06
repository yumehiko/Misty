using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// アイテム。拾うことができ、配置できる。
/// </summary>
public abstract class Item : MonoBehaviour
{
    [SerializeField] private Touchable touchable = default;
    [SerializeField] protected Interactable interactable = default;

    protected Subject<Inventory> onPlace = new Subject<Inventory>();
    /// <summary>
    /// 配置時イベント。
    /// </summary>
    public System.IObservable<Inventory> OnPlace => onPlace;

    private void Awake()
    {
        InitSetting();
    }

    /// <summary>
    /// このオブジェクトのための初期設定。
    /// Awakeの代わりに使う。
    /// </summary>
    protected virtual void InitSetting()
    {
        touchable.OnTouchEnter.Subscribe(toucher => interactable.RegisterToInteractor(toucher));
        touchable.OnTouchExit.Subscribe(toucher => interactable.RemoveFromInteractor(toucher));
        interactable.OnInteract.Subscribe(interactor => TryPickUpItem(interactor));
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
    public void PlaceOnGround(Inventory inventory)
    {
        gameObject.SetActive(true);
        transform.position = inventory.transform.position;
        onPlace.OnNext(inventory);
    }
}
