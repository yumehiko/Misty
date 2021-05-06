using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 鏡。配置でき、邪眼視界を持つ。
/// </summary>
public class Mirror : Item
{
    [SerializeField] private FaceDirection faceDirection = default;

    protected override void InitSetting()
    {
        base.InitSetting();
        onPlace.Subscribe(inventory => SetFaceDirection(inventory));
    }

    /// <summary>
    /// 鏡の向きを配置者と同じ向きに設定する。
    /// </summary>
    /// <param name="inventory"></param>
    private void SetFaceDirection(Inventory inventory)
    {
        FaceDirection faceDirection = inventory.GetComponent<FaceDirection>();
        this.faceDirection.TurnToDirection(faceDirection.Direction, 0.0f);
    }
}
