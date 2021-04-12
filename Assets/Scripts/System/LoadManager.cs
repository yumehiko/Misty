using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンのロードを管理。
/// </summary>
public class LoadManager : MonoBehaviour
{
    public static LoadManager LoadManagerInstance { get; private set; } = default;

    private void Awake()
    {
        if (LoadManagerInstance == null)
        {
            LoadManagerInstance = this;
        }
    }

    /// <summary>
    /// 現在アクティブなシーンをリスタート。
    /// </summary>
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
