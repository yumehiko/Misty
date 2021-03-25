using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// オーディオのサンプルが取得できるかの確認用。
/// </summary>
public class DebugSampleCheck : MonoBehaviour
{
    [SerializeField] private Text text = default;

    void Update()
    {
        text.text = $"{MusicJockey.MusicJockeyInstance.MainSource.timeSamples}";
    }
}
