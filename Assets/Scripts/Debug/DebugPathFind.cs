using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DebugPathFind : MonoBehaviour
{
    [SerializeField] private Seeker seeker = default;
    [SerializeField] private Transform target = default;

    private void Start()
    {
        seeker.StartPath(transform.position, target.position, (p) => CallDebugLog(p));
        Debug.Log("StartMethodEnd");
    }

    private void CallDebugLog(Path path)
    {
        foreach (GraphNode node in path.path)
        {
            Debug.Log((Vector3)node.position);
        }
    }
}
