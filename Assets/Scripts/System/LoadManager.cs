using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンのロードを管理。
/// </summary>
public class LoadManager : MonoBehaviour
{
    public static LoadManager Instance { get; private set; } = default;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    /// <summary>
    /// 現在アクティブなシーンをリスタート。
    /// </summary>
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// 指定したシーンをロード。
    /// </summary>
    /// <param name="scene"></param>
    public void LoadScene(SceneReference scene)
    {
        SceneManager.LoadScene(scene);
    }
}
