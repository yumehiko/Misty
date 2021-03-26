using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Movement movement = default;

    private void Update()
    {
        InputMoveControl();
    }

    /// <summary>
    /// 入力の確認と先行入力の登録。
    /// </summary>
    private void InputMoveControl()
    {
        ActorDirection inputDirection = ActorDirection.None;

        if (Input.GetKeyDown(KeyCode.W))
        {
            inputDirection = ActorDirection.Up;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            inputDirection = ActorDirection.Left;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            inputDirection = ActorDirection.Down;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            inputDirection = ActorDirection.Right;
        }

        if(inputDirection == ActorDirection.None)
        {
            return;
        }

        
        //動作中の場合、先行入力しておく。
        if (movement.IsMotioning())
        {
            _ = movement.MoveCompleteEvent
                .First()
                .Subscribe(_ => movement.MoveToDirection(inputDirection));
        }
        
        movement.MoveToDirection(inputDirection);
    }
}
