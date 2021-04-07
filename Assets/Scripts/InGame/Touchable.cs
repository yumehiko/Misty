using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 触れて、インタラクトできるもの。それぞれのイベントを発行する。
/// </summary>
public class Touchable : MonoBehaviour
{
    private BoolReactiveProperty isTouch = new BoolReactiveProperty(false);
    /// <summary>
    /// 触れたときTrue、離れたときFalse。
    /// </summary>
    public IReadOnlyReactiveProperty<bool> IsTouch => isTouch;

    private Subject<Unit> interactEvent = new Subject<Unit>();
    /// <summary>
    /// インタラクトしたとき。
    /// </summary>
    public System.IObservable<Unit> InteractEvent => interactEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーに触れたとき、インタラクトターゲットに自身を登録する。
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Interactor>().SetTarget(this);
            isTouch.Value = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //プレイヤーが離れたとき、インタラクトターゲットを削除。
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Interactor>().RemoveTarget();
            isTouch.Value = false;
        }
    }

    /// <summary>
    /// インタラクトを実行する。
    /// </summary>
    public void DoInteract()
    {
        interactEvent.OnNext(Unit.Default);
    }
}
