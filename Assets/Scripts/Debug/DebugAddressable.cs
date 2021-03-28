using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;

public class DebugAddressable : MonoBehaviour
{

    private void Start()
    {
        AddressableInstantiate("Actors/Player.prefab");
        Debug.Log("Called");
    }

    private void AddressableInstantiate(string path)
    {
        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(path, transform);
        handle.Completed += op =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log(op.Result.name);
            }
        };
    }
}
