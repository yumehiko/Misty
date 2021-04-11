using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SeeingDebug : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer = default;
    [SerializeField] private SeeTarget seeTarget = default;

    private void Awake()
    {
        seeTarget.IsSeeing.Subscribe(isSeeing => OnSeeing(isSeeing));
    }

    private void OnSeeing(bool isSeeing)
    {
        Debug.Log("ChangeSeeing");
        if(isSeeing)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }
}
