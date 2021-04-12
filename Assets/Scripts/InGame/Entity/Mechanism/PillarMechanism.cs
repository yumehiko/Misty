using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 機械式柱メカニズム。
/// </summary>
public class PillarMechanism : MechanismBase
{
    [SerializeField] private GameObject colliderObject = default;
    [SerializeField] private Animator animator = default;
    private EvilSightManager evilSightManager;

    private void Start()
    {
        RegisterSwitches();
        evilSightManager = GameObject.FindWithTag("LevelManager").GetComponent<EvilSightManager>();
    }

    /// <summary>
    /// 柱を下げて、通行可能にする。
    /// </summary>
    protected override void ActiveMachanism()
    {
        animator.SetTrigger("DoOpen");
        colliderObject.SetActive(false);

        ReScanPathFinder();
        //evilSightManager.RefleshEvilSight();
        
        Observable.NextFrame()
            .Subscribe(_ => evilSightManager.RefleshEvilSight());
        
    }

    /// <summary>
    /// 柱を上げて、通行不可能にする。
    /// </summary>
    protected override void DeActiveMechanism()
    {
        animator.SetTrigger("DoClose");
        colliderObject.SetActive(true);

        ReScanPathFinder();
        //evilSightManager.RefleshEvilSight();
        
        Observable.NextFrame()
            .Subscribe(_ => evilSightManager.RefleshEvilSight());
        

    }
}
