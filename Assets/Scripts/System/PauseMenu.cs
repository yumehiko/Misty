using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu PauseMenuInstance = default;

    private void Awake()
    {
        // PauseMenuはシングルトン。
        PauseMenuInstance = this;
    }



    [SerializeField] private GameManager gameManager = default;
    [SerializeField] private GameObject pauseMenuObject = default;

    /// <summary>
    /// ポーズ時サブジェクト。
    /// </summary>
    private Subject<bool> pauseSubject = new Subject<bool>();

    /// <summary>
    /// ポーズ時イベント。
    /// </summary>
    public IObservable<bool> OnPause => pauseSubject;

    private void Update()
    {
        CheckPauseKeyInput();
    }

    /// <summary>
    /// ポーズ キーの入力を待機。
    /// </summary>
    private void CheckPauseKeyInput()
    {
        if (gameManager.IsSceneSwapping)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (pauseMenuObject.activeSelf == false)
            {
                ActiveMenu();
            }
            else
            {
                InActiveMenu();
            }
        }
    }

    /// <summary>
    /// PauseMenuを起動。
    /// </summary>
    public void ActiveMenu()
    {
        Time.timeScale = 0.0f;
        pauseMenuObject.SetActive(true);
        pauseSubject.OnNext(true);
    }

    /// <summary>
    /// PauseMenuを終了。
    /// </summary>
    public void InActiveMenu()
    {
        Time.timeScale = 1.0f;
        pauseMenuObject.SetActive(false);
        pauseSubject.OnNext(false);
    }
}
