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
        Player player;
        Movement movement;
        FaceDirection faceDirection;
        UniTask setUpTask;

        [SetUp]
        public void SetUp()
        {
            setUpTask = SetObject().Preserve();
        }

        /// <summary>
        /// 必要なオブジェクトを生成
        /// </summary>
        /// <returns></returns>
        private async UniTask SetObject()
        {
            AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync("Actors/Player.prefab");
            
            //ロード完了をUnitaskでawaitする。
            await handle.ToUniTask();
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                playerObject = handle.Result;
                player = playerObject.GetComponent<Player>();
                movement = player.Movement;
                faceDirection = player.FaceDirection;
            }
        }

        /// <summary>
        /// プレイヤーが生成できたかのテスト。
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator PlayerInstanceTest()
        {
            yield return setUpTask.ToCoroutine();

            Assert.That(playerObject, Is.Not.Null);
        }

        /// <summary>
        /// プレイヤーの移動テスト。
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator PlayerMoveInputTest()
        {
            yield return setUpTask.ToCoroutine();

            movement.MoveToDirection(ActorDirection.Up, 0.2f);
            yield return new WaitForSeconds(0.3f);
            Assert.That(playerObject.transform.position, Is.EqualTo(new Vector3(0.0f, 1.28f)));
        }

        /// <summary>
        /// プレイヤーの視界の回転テスト。
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator FaceDirectionTest()
        {
            yield return setUpTask.ToCoroutine();

            Transform sightTransform = GameObject.Find("Sight").transform;
            faceDirection.TurnToDirection(ActorDirection.Right, 0.1f);
            yield return new WaitForSeconds(0.2f);
            Assert.That(sightTransform.eulerAngles, Is.EqualTo(new Vector3(0.0f, 0.0f, 270.0f)));
        }
    }
}
