using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのゲーム上の実体。
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField] private Transform sightTransform = default;

    private TurnAct turnAct = default;
    public TurnAct TurnAct => turnAct;

    private Movement movement = default;
    public Movement Movement => movement;

    private FaceDirection faceDirection = default;
    public FaceDirection FaceDirection => faceDirection;

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 初期化。必要なクラスを生成。
    /// </summary>
    private void Init()
    {
        turnAct = new TurnAct();
        movement = new Movement(turnAct, transform);
        faceDirection = new FaceDirection(turnAct, sightTransform);
    }
}
