using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 邪眼。視界に入ったものに魔力で干渉する。
/// </summary>
public class EvilSeeing : MonoBehaviour
{
    [SerializeField] private Transform sightTransform = default;
    private List<SeeTarget> seeTargets = new List<SeeTarget>();

    public void AddSeeTarget(SeeTarget seeTarget)
    {
        seeTargets.Add(seeTarget);
    }
}
