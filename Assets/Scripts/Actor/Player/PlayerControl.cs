using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private TurnAct turnAct = default;
    [SerializeField] private Movement movement = default;
    [SerializeField] private FaceDirection faceDirection = default;

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

        if (turnAct.IsActing)
        {
            inputBuffer = direction;
            turnAct.ActCompleteEvent
                .First()
                .Subscribe(_ => SolveBufferInput());
            return;
        }

        if (direction != faceDirection.Direction)
        {
            faceDirection.TurnToDirection(direction, 0.1f);
        }
        else
        {
            movement.MoveToDirection(direction, 0.2f);
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
