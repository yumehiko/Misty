﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// レベルに関連するプレイヤーの入力。
/// </summary>
public class LevelManager : MonoBehaviour
{
    [SerializeField] private SceneReference nextScene = default;

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
        LoadManager.Instance.RestartScene();
    }

    /// <summary>
    /// このレベルをクリアする。
    /// </summary>
    public void BeatThisLevel()
    {
        LoadManager.Instance.LoadScene(nextScene);
    }
}
