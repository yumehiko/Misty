using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;

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

        private async Task SetObject()
        {
            var handle = Addressables.InstantiateAsync("Actors/Player.prefab");
            await handle.Task;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("できた");
                playerObject = handle.Result;
            }
        }

        [TearDown]
        public void TearDown()
        {
            Addressables.ReleaseInstance(playerObject);
        }

        [UnityTest]
        public IEnumerator PlayerInstanceTest()
        {
            yield return new WaitForSeconds(1);
            Assert.That(playerObject != null, Is.True);
        }
    }
}
