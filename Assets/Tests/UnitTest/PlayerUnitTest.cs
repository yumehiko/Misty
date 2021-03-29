using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerUnitTest
    {
        private FaceDirection faceDirection;

        [SetUp]
        public void SetUp()
        {
            GameObject gameObject = new GameObject("GameObject");
            TurnAct turnAct = new TurnAct();
            faceDirection = new FaceDirection(turnAct, gameObject.transform);
        }

        [Test]
        public void DirectionTest()
        {
            faceDirection.TurnToDirection(ActorDirection.Up, 0.1f);
            Assert.That(faceDirection.Direction, Is.EqualTo(ActorDirection.Up));
        }
    }
}
