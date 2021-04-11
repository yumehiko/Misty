using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 捕獲対象物。
/// </summary>
public class Capturable : MonoBehaviour
{
    private Subject<Unit> onCaptured = new Subject<Unit>();
    /// <summary>
    /// 捕獲時。
    /// </summary>
    public System.IObservable<Unit> OnCaptured => onCaptured;

    [SerializeField] private Animator animator = default;

    /// <summary>
    /// 捕獲される。
    /// </summary>
    public void BeCaptured()
    {
        animator.Play("Captured");
        onCaptured.OnNext(Unit.Default);
    }
}
