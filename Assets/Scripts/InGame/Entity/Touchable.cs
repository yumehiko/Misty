using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 触れて、インタラクトできるもの。それぞれのイベントを発行する。
/// </summary>
public class Touchable : MonoBehaviour
{
    [SerializeField] private Animator animator = default;
    private int aKeyCanInteract;

    private BoolReactiveProperty isTouch = new BoolReactiveProperty(false);
    /// <summary>
    /// 触れたときTrue、離れたときFalse。
    /// </summary>
    public IReadOnlyReactiveProperty<bool> IsTouch => isTouch;

    private Subject<Interactor> interactEvent = new Subject<Interactor>();
    /// <summary>
    /// インタラクトしたとき。
    /// </summary>
    public System.IObservable<Interactor> InteractEvent => interactEvent;

    private void Awake()
    {
        aKeyCanInteract = Animator.StringToHash("CanInteract");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーに触れたとき、インタラクトターゲットに自身を登録する。
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Interactor>().SetTarget(this);
            animator.SetBool(aKeyCanInteract, true);
            isTouch.Value = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //プレイヤーが離れたとき、インタラクトターゲットを削除。
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Interactor>().RemoveTarget();
            animator.SetBool(aKeyCanInteract, false);
            isTouch.Value = false;
        }
    }

    /// <summary>
    /// インタラクトを実行する。
    /// </summary>
    public void DoInteract(Interactor interactor)
    {
        interactEvent.OnNext(interactor);
    }
}
