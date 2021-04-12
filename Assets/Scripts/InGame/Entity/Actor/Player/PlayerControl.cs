using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 入力を確認し、プレイヤーを操作する。
/// </summary>
public class PlayerControl : MonoBehaviour
{
    [SerializeField] private ActorAnimeController animeController = default;
    [SerializeField] private Movement movement = default;
    [SerializeField] private FaceDirection faceDirection = default;
    [SerializeField] private Interactor interactor = default;
    [SerializeField] private Capturable capturable = default;
    

    private bool canControl = true;

    private TurnManager turnManager = default;

    private void Awake()
    {
        turnManager = TurnManager.GetTurnManager();

        //FaceTurn完了時のEvilSight更新イベントを登録。
        EvilSightManager evilSightManager = turnManager.GetComponent<EvilSightManager>();
        faceDirection.OnFaceTurnComplete.Subscribe(_ => evilSightManager.RefleshEvilSight());

        //捕獲時、行動不能に。
        capturable.OnCaptured.Subscribe(_ => canControl = false);
    }

    private void Update()
    {
        if(!canControl)
        {
            return;
        }

        ActorDirection inputDirection = CheckInputDirection();
        InputMoveControl(inputDirection);

        if (Input.GetKeyDown(KeyCode.F))
        {
            InputInteract();
        }
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
        //入力がないなら無視。
        if(direction == ActorDirection.None)
        {
            return;
        }

        //プレイヤーが何らかの行動中なら、入力を先行入力として保持し、終了。
        if (turnManager.IsActing)
        {
            turnManager.OnSolveInputBuffer
                .First()
                .Subscribe(_ => InputMoveControl(direction));
            return;
        }

        //入力した方向とFaceDirectionが異なるなら、向きを変えて終了。
        if (direction != faceDirection.Direction)
        {
            faceDirection.TurnToDirection(direction, 0.1f);
            animeController.SkeletonFlip(direction);
            return;
        }

        //入力方向に移動できるなら、移動。
        if(movement.CheckCanMove(Movement.DirectionToVector2(direction)))
        {
            turnManager.TurnStart(TurnManager.TurnBaseTime);
            movement.MoveToDirection(direction, TurnManager.TurnBaseTime);
            return;
        }

        //入力はしたけど何もできなかった。
    }

    /// <summary>
    /// インタラクトキーを入力する。
    /// </summary>
    private void InputInteract()
    {
        //プレイヤーが何らかの行動中なら、入力を先行入力として保持し、終了。
        if (turnManager.IsActing)
        {
            _ = turnManager.OnSolveInputBuffer
                .First()
                .Subscribe(_ => InputInteract());
            return;
        }

        interactor.TryInteract();

        turnManager.TurnStart(TurnManager.TurnBaseTime);
    }
}
