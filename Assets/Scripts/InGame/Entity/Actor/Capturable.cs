using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 捕獲対象物。
/// </summary>
public class Capturable : MonoBehaviour
{
    /// <summary>
    /// 捕獲される。
    /// </summary>
    public void BeCaptured()
    {
        Debug.Log("Captured");
        gameObject.SetActive(false);
    }
}
