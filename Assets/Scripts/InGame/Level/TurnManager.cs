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
     */

    /// <summary>
    /// 経過ターン数。
    /// </summary>
    private int turnCount = 0;

    /// <summary>
    /// 1ターンにかかる基本時間。
    /// </summary>
    public static readonly float TurnBaseTime = 0.2f;

    private Subject<Unit> onTurnStart = new Subject<Unit>();
    /// <summary>
    /// ターン開始時。
    /// </summary>
    public System.IObservable<Unit> OnTurnStart => onTurnStart;

    private Subject<Unit> onTurnEndSightEvent = new Subject<Unit>();
    /// <summary>
    /// ターン終了直前、視界処理などのタイミング。
    /// </summary>
    public System.IObservable<Unit> OnTurnEndSightEvent => onTurnEndSightEvent;

    private Subject<Unit> onTurnEnd = new Subject<Unit>();
    /// <summary>
    /// ターン終了時。
    /// </summary>
    public System.IObservable<Unit> OnTurnEnd => onTurnEnd;


    /// <summary>
    /// ターン実行中のTween。
    /// </summary>
    public Tween TurnTween { get; private set; } = default;

    /// <summary>
    /// ターン動作が実行中か。
    /// </summary>
    public bool IsActing => TurnTween.IsActive();

    /// <summary>
    /// ターンを進める。
    /// </summary>
    public void TurnStart(float duration)
    {
        turnCount++;
        onTurnStart.OnNext(Unit.Default);
        TurnTween = DOVirtual.DelayedCall(duration + 0.05f, () => TurnEnd());
    }

    /// <summary>
    /// ターンを終了する。
    /// </summary>
    private void TurnEnd()
    {
        onTurnEndSightEvent.OnNext(Unit.Default);

        //ここでKillするのは、先行入力を通すため。
        //でも、killせずにプレイヤーの先行入力処理タイミング用のイベントをどっかに移した方がよさそう。
        //懸念：ターン処理が何らかの理由で遅れて、終わってない場合でも、プレイヤーの動作が終わったらとにかく呼ばれてしまう。
        //処理速度によっては都合が悪いかも。
        TurnTween.Kill();
        onTurnEnd.OnNext(Unit.Default);
    }
}
