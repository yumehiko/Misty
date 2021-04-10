using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/// <summary>
/// 経路探索上の障害物のうち、更新が必要なもの。
/// </summary>
public class PathCollider : MonoBehaviour
{
    [SerializeField] private GameObject colliderObject = default;

    /// <summary>
    /// コライダーをアクティブ状態にして、パスファインダーグリッドを更新する。
    /// </summary>
    public void ActiveCollider()
    {
        colliderObject.SetActive(true);

        //最初のグラフを再スキャン。
        var graphToScan = AstarPath.active.data.gridGraph;
        AstarPath.active.Scan(graphToScan);
    }

    /// <summary>
    /// コライダーを非アクティブ状態にして、パスファインダーグリッドを更新する。
    /// </summary>
    public void InactiveCollider()
    {
        colliderObject.SetActive(false);

        //最初のグラフを再スキャン。
        var graphToScan = AstarPath.active.data.gridGraph;
        AstarPath.active.Scan(graphToScan);
    }
}
