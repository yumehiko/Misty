using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


/// <summary>
/// ターンの経過に応じてActionを実行するEntity。
/// </summary>
public abstract class Actor : MonoBehaviour
{
    /// <summary>
    /// ターン行動ができるか。
    /// </summary>
    public bool CanAction = true;
}
