using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// レベルに関連するプレイヤーの入力。
/// </summary>
public class LevelInputManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }


    /// <summary>
    /// このレベルをリスタートする。
    /// </summary>
    private void RestartLevel()
    {
        LoadManager.LoadManagerInstance.RestartScene();
    }
}
