using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 入力を確認し、プレイヤーを操作する。
/// </summary>
public class PlayerControl : MonoBehaviour
{
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
        InputInteract();
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
            ActorDirection inputBuffer = direction;
            turnManager.OnSolveInputBuffer
                .First()
                .Subscribe(_ => SolveBufferInput(inputBuffer));
            return;
        }

        //入力した方向とFaceDirectionが異なるなら、向きを変えて終了。
        if (direction != faceDirection.Direction)
        {
            faceDirection.TurnToDirection(direction, 0.1f);
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
        if (!Input.GetKeyDown(KeyCode.F))
        {
            return;
        }

        //プレイヤーが何らかの行動中なら、入力を先行入力として保持し、終了。
        //TODO とりあえず入力無視してるだけ。キーコードを引数にする仕組みが欲しいな……。
        if (turnManager.IsActing)
        {
            ActorDirection inputBuffer = ActorDirection.None;
            _ = turnManager.OnSolveInputBuffer
                .First()
                .Subscribe(_ => SolveBufferInput(inputBuffer));
            return;
        }

        turnManager.TurnStart(TurnManager.TurnBaseTime);

        //インタラクト対象が無い場合、無視。
        if (!interactor.HasTouchable())
        {
            //音とか鳴らすかも。
            return;
        }

        interactor.TryInteract();
    }

    /// <summary>
    /// 先行入力を保持している場合、それを入力する。
    /// </summary>
    private void SolveBufferInput(ActorDirection inputBuffer)
    {
        if(inputBuffer == ActorDirection.None)
        {
            return;
        }
        InputMoveControl(inputBuffer);
    }
}
