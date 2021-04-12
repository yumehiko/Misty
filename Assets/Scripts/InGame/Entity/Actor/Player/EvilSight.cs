using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UniRx;

/// <summary>
/// 邪眼。視界に入ったものに魔力で干渉する。
/// </summary>
public class EvilSight : MonoBehaviour
{
    [SerializeField] private Light2D fovLight = default;
    [SerializeField] private LayerMask sightMask = default;
    private List<SeeTarget> seeTargets;

    /// <summary>
    /// この邪眼が有効か。
    /// </summary>
    private bool isActive = true;

    private void Awake()
    {
        EvilSightManager evilSightManager = GameObject.FindWithTag("LevelManager").GetComponent<EvilSightManager>();
        seeTargets = evilSightManager.SeeTargets;
        evilSightManager.OnRefleshEvilSight.Subscribe(_ => EvilSeeing());
    }

    /// <summary>
    /// 邪眼を行使する。
    /// </summary>
    private void EvilSeeing()
    {
        if(!isActive)
        {
            return;
        }

        if(seeTargets.Count == 0)
        {
            return;
        }

        Vector2 forward = -transform.up;
        Vector2 playerPosition = transform.position;
        float fovHalf = fovLight.pointLightOuterAngle / 2.0f;
        
        foreach(SeeTarget seeTarget in seeTargets)
        {
            if(AppearCheck(fovHalf, forward, playerPosition, seeTarget.transform.position))
            {
                //必要なときtrueにするだけ。falseに戻すかどうかはseeTarget側で制御する。
                seeTarget.SetSeeing = true;
            }
        }
    }

    /// <summary>
    /// 視界が対象まで繋がるかチェック。
    /// </summary>
    /// <returns>到達するならtrue</returns>
    private bool AppearCheck(float fovHalf, Vector2 forward, Vector2 playerPosition, Vector2 targetPosition)
    {
        //TODO 二つのチェックを分けるべきか？

        Vector2 direction = (playerPosition - targetPosition).normalized;
        if (!(Vector2.Angle(direction, forward) <= fovHalf))
        {
            return false;
        }

        RaycastHit2D hit = Physics2D.Linecast(playerPosition, targetPosition, sightMask);

        if (hit.collider != null) 
        {
            return false;
        }

        return true;
    }

    private void OnEnable()
    {
        isActive = true;
    }

    private void OnDisable()
    {
        isActive = false;
    }
}
