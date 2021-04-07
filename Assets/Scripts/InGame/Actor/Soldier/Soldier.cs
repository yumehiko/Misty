using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

/// <summary>
/// 兵士。プレイヤーを捕獲しようと接近するActor。
/// </summary>
public class Soldier : MonoBehaviour
{
    [SerializeField] private LayerMask moveMask = default;
    [SerializeField] private ActorAnimeController animeController = default;

    private TurnAct turnAct = default;
    public TurnAct TurnAct => turnAct;

    private Movement movement = default;
    public Movement Movement => movement;

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 初期化。
    /// </summary>
    private void Init()
    {
        turnAct = new TurnAct();
        movement = new Movement(turnAct, transform, moveMask, animeController);
    }
}
