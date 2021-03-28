using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;

namespace Tests
{
    public class PlayerTest
    {
        GameObject playerObject;

        [SetUp]
        public void SetUp()
        {
            _ = SetObject();
        }

        private async UniTask SetObject()
        {
            var handle = Addressables.InstantiateAsync("Actors/Player.prefab");
            //ロード完了をUnitaskでawaitする。
            await handle.ToUniTask();
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                playerObject = handle.Result;
            }
        }

        [UnityTest]
        public IEnumerator PlayerInstanceTest()
        {
            yield return new WaitForSeconds(1);
            Assert.That(playerObject != null, Is.True);
        }
    }
}
