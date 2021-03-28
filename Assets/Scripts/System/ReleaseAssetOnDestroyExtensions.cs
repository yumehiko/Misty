using UniRx.Triggers;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UniRx;
using UnityEngine.AddressableAssets;

public static class ReleaseAssetOnDestroyExtensions
{
    /// <summary>
    /// コンポーネントのDestroy時に、指定したhandleをリリースする。
    /// </summary>
    /// <param name="handle"></param>
    /// <param name="component"></param>
    /// <returns></returns>
    public static AsyncOperationHandle ReleaseAssetOnDestroy(this AsyncOperationHandle handle, Component component)
    {
        component.OnDestroyAsObservable().Subscribe(_ => Addressables.Release(handle));
        return handle;
    }
}