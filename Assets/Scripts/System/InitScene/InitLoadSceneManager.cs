using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitLoadSceneManager : MonoBehaviour
{
    [SerializeField] private SceneReference firstScene = default;

    private void Start()
    {
        LoadManager.Instance.LoadScene(firstScene);
    }
}
