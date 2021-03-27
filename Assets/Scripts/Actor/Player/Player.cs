using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのゲーム上の実体。
/// </summary>
public class Player : MonoBehaviour
{
    private TurnAct turnAct = default;
    public TurnAct TurnAct => turnAct;

    private Movement movement = default;
    public Movement Movement => movement;

    private FaceDirection faceDirection = default;
    public FaceDirection FaceDirection => faceDirection;

    [SerializeField] private Transform sightTransform = default;

    private void Awake()
    {
        turnAct = new TurnAct();
        movement = new Movement(turnAct, transform);
        faceDirection = new FaceDirection(turnAct, sightTransform);
    }
}
