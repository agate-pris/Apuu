using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AgatePris.Apuu {
    public class UtilityTest {
        [Test]
        public void TestAllOfObjectsAreDestroyed() {
            var gameObject = new GameObject();
            var behaviour = gameObject.AddComponent<EventTrigger>();
            var l = new List<EventTrigger>();
            Assert.IsTrue(Utility.AllOfObjectsAreDestroyed(l));
            l.Add(behaviour);
            Assert.IsFalse(Utility.AllOfObjectsAreDestroyed(l));
            Object.DestroyImmediate(behaviour);
            Assert.IsTrue(Utility.AllOfObjectsAreDestroyed(l));
        }
    }
}
