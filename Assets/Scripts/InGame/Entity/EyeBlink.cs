using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// まばたきをする。
/// </summary>
public class EyeBlink : MonoBehaviour
{
    [SerializeField] private Animator animator = default;
    private int aKeyDoEyeBlink;

    private void Start()
    {
        aKeyDoEyeBlink = Animator.StringToHash("DoEyeBlink");

        float delay = Random.Range(0.5f, 8.0f);

        DOVirtual.DelayedCall(delay,
            () => DoEyeBlink())
            .SetLink(gameObject)
            .SetUpdate(UpdateType.Normal, false);
    }

    private void DoEyeBlink()
    {
        animator.SetTrigger(aKeyDoEyeBlink);

        float delay = Random.Range(0.5f, 8.0f);

        DOVirtual.DelayedCall(delay,
            () => DoEyeBlink())
            .SetLink(gameObject)
            .SetUpdate(UpdateType.Normal, false);

    }
}
