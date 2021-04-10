using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GameManager : MonoBehaviour
{
    public static GameManager GameManagerInstance { get; private set; } = default;

    private void Awake()
    {
        GameManagerInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool IsSceneSwapping { get; private set; } = false;
}
