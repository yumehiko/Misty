using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 入力を確認し、プレイヤーを操作する。
/// </summary>
public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Player player = default;

    private ActorDirection inputBuffer = ActorDirection.None;

    private void Update()
    {
        ActorDirection inputDirection = CheckInputDirection();
        InputMoveControl(inputDirection);
    }

    /// <summary>
    /// キー入力からActorDirectionを返す。
    /// </summary>
    /// <returns></returns>
    private ActorDirection CheckInputDirection()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            return ActorDirection.Up;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            return ActorDirection.Left;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            return ActorDirection.Down;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            return ActorDirection.Right;
        }

        return ActorDirection.None;
    }

    /// <summary>
    /// 入力の確認と先行入力の登録。
    /// </summary>
    private void InputMoveControl(ActorDirection direction)
    {
        if(direction == ActorDirection.None)
        {
            return;
        }

        if (player.TurnAct.IsActing)
        {
            inputBuffer = direction;
            player.TurnAct.ActCompleteEvent
                .First()
                .Subscribe(_ => SolveBufferInput());
            return;
        }

        if (direction != player.FaceDirection.Direction)
        {
            player.FaceDirection.TurnToDirection(direction, 0.1f);
        }
        else
        {
            player.Movement.MoveToDirection(direction, 0.2f);
        }
    }

    /// <summary>
    /// 先行入力を保持している場合、それを入力する。
    /// </summary>
    private void SolveBufferInput()
    {
        if(inputBuffer == ActorDirection.None)
        {
            return;
        }

        InputMoveControl(inputBuffer);
        inputBuffer = ActorDirection.None;
    }
}
