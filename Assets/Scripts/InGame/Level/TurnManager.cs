using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

/// <summary>
/// レベルのエンティティのターンを管理する。
/// </summary>
public class TurnManager : MonoBehaviour
{
    /*
     * ターンイベント処理のタイミングまとめ：
     * 
     * ・TODO / ここに経路探索イベントをいれた方がよいかも。
     * 
     * 
     * 
     * ・onTurnStart：ターンスタート時処理。
     * 　・プレイヤーの入力待機。
     * 　・兵士の経路チェック、および移動先の決定。
     * 　
     * ・onTurnEndEvilSight：ターン終了時直前。数フレームにわたる可能性あり。TODO/名前変えたい
     * 　・邪眼状態のリフレッシュ
     * 　onTurnEndEvilSightは、処理中に1フレーム待つ処理とかが挟まってるけど、そうでなくてイベントを繰り返し発行させる方がよいかも。
     * 
     * 
     * ・TODO / 現状では捕獲と石化が同時に怒ってるので、ここにOnCaptureEventが必要。
     * 
     * ・onTurnEnd：ターン終了時処理。
     * 　・石化
     * 　・捕獲
     * 　・兵士がプレイヤーを発見できるかチェック。
     * 　
     * 　TODO / メモる必要もないくらい自明な名前で必要なだけ処理タイミングイベントを増やした方がよいかも。
     * 
     * 新しいターンイベント一覧：
     * ・onPathFind：経路探索計算。
     * ・onWaitInput：プレイヤーの入力待機。
     * （0.2秒のアニメーション待機）
     * ・onEvilSightReflesh
     * ・onCapture
     * ・onPetrify
     * ・onDetectPlayer
     */

    /// <summary>
    /// 経過ターン数。
    /// </summary>
    private int turnCount = 0;

    /// <summary>
    /// 1ターンにかかる基本時間。
    /// </summary>
    public static readonly float TurnBaseTime = 0.2f;

    private Subject<Unit> onDetectPlayer = new Subject<Unit>();
    private Subject<Unit> onPathFind = new Subject<Unit>();
    private Subject<Unit> onActInput = new Subject<Unit>();
    private Subject<Unit> onEvilSightReflesh = new Subject<Unit>();
    private Subject<Unit> onCapture = new Subject<Unit>();
    private Subject<Unit> onPetrify = new Subject<Unit>();
    private Subject<Unit> onSolveInputBuffer = new Subject<Unit>();



    /// <summary>
    /// 兵士などがプレイヤーを探すタイミング。
    /// </summary>
    public System.IObservable<Unit> OnDetectPlayer => onDetectPlayer;

    /// <summary>
    /// 経路探索タイミング。
    /// </summary>
    public System.IObservable<Unit> OnPathFind => onPathFind;

    /// <summary>
    /// プレイヤーの入力実行タイミング。
    /// </summary>
    public System.IObservable<Unit> OnActInput => onActInput;

    /// <summary>
    /// 邪眼の影響更新タイミング。
    /// </summary>
    public System.IObservable<Unit> OnEvilSightReflesh => onEvilSightReflesh;

    /// <summary>
    /// 捕獲確認タイミング。
    /// </summary>
    public System.IObservable<Unit> OnCapture => onCapture;

    /// <summary>
    /// 石化確認タイミング。
    /// </summary>
    public System.IObservable<Unit> OnPetrify => onPetrify;

    /// <summary>
    /// 先行入力実行タイミング。
    /// </summary>
    public System.IObservable<Unit> OnSolveInputBuffer => onSolveInputBuffer;



    /// <summary>
    /// ターン実行中のTween。
    /// </summary>
    public Tween TurnTween { get; private set; } = default;

    /// <summary>
    /// ターン動作が実行中か。
    /// </summary>
    public bool IsActing { get; private set; } = false;



    /// <summary>
    /// ターンを進める。
    /// </summary>
    public void TurnStart(float duration)
    {
        turnCount++;
        onDetectPlayer.OnNext(Unit.Default);
        onPathFind.OnNext(Unit.Default);
        onActInput.OnNext(Unit.Default);
        IsActing = true;
        TurnTween = DOVirtual.DelayedCall(duration, () => TurnEnd());
    }

    /// <summary>
    /// ターンを終了する。
    /// </summary>
    private void TurnEnd()
    {
        onEvilSightReflesh.OnNext(Unit.Default);
        onCapture.OnNext(Unit.Default);
        onPetrify.OnNext(Unit.Default);
        IsActing = false;
        onSolveInputBuffer.OnNext(Unit.Default);
    }

    /// <summary>
    /// シーン上のターンマネージャーを取得。
    /// </summary>
    /// <returns></returns>
    public static TurnManager GetTurnManager()
    {
        return GameObject.FindWithTag("LevelManager").GetComponent<TurnManager>();
    }
}
