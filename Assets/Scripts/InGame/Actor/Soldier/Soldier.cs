using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 兵士。プレイヤーを捕獲しようと接近するActor。
/// </summary>
public class Soldier : MonoBehaviour
{
    private TurnAct turnAct = default;
    public TurnAct TurnAct => turnAct;

    private Movement movement = default;
    public Movement Movement => movement;

    private void Awake()
    {
        turnAct = new TurnAct();
        movement = new Movement(turnAct, transform);
    }
}
