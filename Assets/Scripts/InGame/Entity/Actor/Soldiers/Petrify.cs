using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 指定ターン邪眼に曝されたとき、石化する。
/// </summary>
public class Petrify : MonoBehaviour
{
    [SerializeField] private SeeTarget seeTarget = default;

    /// <summary>
    /// 石化進行状況を示すheadUIのAnimator。
    /// </summary>
    [SerializeField] private Animator petrifyUIAnimator = default;
    private int aKeyIsPop = default;

    /// <summary>
    /// 石化進行状況を示すUIのFill部。
    /// </summary>
    [SerializeField] private Image petrifyUIFill = default;

    /// <summary>
    /// 石化に対する耐久力。
    /// </summary>
    [SerializeField] private int endurance = 3;

    /// <summary>
    /// 石化の進行度。
    /// </summary>
    private int progressValue = 0;

    private BoolReactiveProperty isPetrified = new BoolReactiveProperty(false);
    /// <summary>
    /// 石化したか。
    /// </summary>
    public IReadOnlyReactiveProperty<bool> IsPetrified => isPetrified;

    private TurnManager turnManager = default;

    private void Awake()
    {
        turnManager = GameObject.FindWithTag("LevelManager").GetComponent<TurnManager>();
        turnManager.OnTurnEnd.Subscribe(_ => CheckEvilSightStats());
        aKeyIsPop = Animator.StringToHash("IsPop");
    }

    /// <summary>
    /// ターン開始時に邪眼に曝されているなら、石化進行状態を1進める。
    /// </summary>
    private void CheckEvilSightStats()
    {
        //すでに石化している。
        if(isPetrified.Value)
        {
            return;
        }

        if(!seeTarget.IsSeeing.Value)
        {
            RecoverPetrifyDamage(1);
            return;
        }

        PetrifyDamage(1);
    }

    /// <summary>
    /// 石化ダメージを負う。
    /// </summary>
    /// <param name="damageValue"></param>
    private void PetrifyDamage(int damageValue)
    {
        progressValue += damageValue;
        CalcFillAmount();

        petrifyUIAnimator.SetBool(aKeyIsPop, true);

        if (progressValue >= endurance)
        {
            BePetrified();
        }
    }

    /// <summary>
    /// 石化ダメージを回復。
    /// </summary>
    private void RecoverPetrifyDamage(int recoverValue)
    {
        progressValue = Mathf.Max(0, progressValue - recoverValue);
        CalcFillAmount();
        if (progressValue == 0)
        {
            petrifyUIAnimator.SetBool(aKeyIsPop, false);
        }
    }

    /// <summary>
    /// UIのFill部を再計算。
    /// </summary>
    private void CalcFillAmount()
    {
        float amount = 1.0f * progressValue / endurance;
        petrifyUIFill.DOFillAmount(amount, 0.1f);
    }


    

    /// <summary>
    /// 石化する。
    /// </summary>
    private void BePetrified()
    {
        isPetrified.Value = true;
        petrifyUIAnimator.SetBool(aKeyIsPop, false);
    }
}
