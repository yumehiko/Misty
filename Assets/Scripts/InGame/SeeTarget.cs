using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 邪眼でみられたとき、反応する機構。
/// </summary>
public class SeeTarget : MonoBehaviour
{
    private Subject<bool> seeing = new Subject<bool>();
    public System.IObservable<bool> Seeing => seeing;

    private void Awake()
    {
        GameObject.FindWithTag("Player").GetComponent<Player>();
    }
}
