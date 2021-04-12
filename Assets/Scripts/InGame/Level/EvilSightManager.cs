using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// レベルごとに1つ。邪眼の状態を管理する。
/// </summary>
public class EvilSightManager : MonoBehaviour
{
    [SerializeField] private TurnManager turnManager = default;

    public List<SeeTarget> SeeTargets { get; private set; } = new List<SeeTarget>();

    private Subject<Unit> onRefleshEvilSight = new Subject<Unit>();
    /// <summary>
    /// EvilSight更新タイミング。
    /// </summary>
    public System.IObservable<Unit> OnRefleshEvilSight => onRefleshEvilSight;

    private Subject<Unit> onRefleshSeeTarget = new Subject<Unit>();
    /// <summary>
    /// SeeTarget更新タイミング。
    /// </summary>
    public System.IObservable<Unit> OnRefleshSeeTarget => onRefleshSeeTarget;

    private void Awake()
    {
        turnManager.OnEvilSightReflesh.Subscribe(_ => RefleshEvilSight());
    }

    /// <summary>
    /// SeeTargetを登録する。
    /// </summary>
    public void AddSeeTarget(SeeTarget seeTarget)
    {
        SeeTargets.Add(seeTarget);
    }

    /// <summary>
    /// 全ての邪眼の影響を更新。
    /// </summary>
    public void RefleshEvilSight()
    {
        onRefleshEvilSight.OnNext(Unit.Default);
        onRefleshSeeTarget.OnNext(Unit.Default);
    }
}
