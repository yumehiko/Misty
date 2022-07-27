using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelCameraControl : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera levelCamera = default;
    public CinemachineVirtualCamera LevelCamera => levelCamera;

    /// <summary>
    /// カメラをシェイクする。
    /// </summary>
    public void ShakeCamera()
    {

    }
}
