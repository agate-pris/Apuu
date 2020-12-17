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

        [Test]
        public void TestAnyOfObjectsIsDestroyed() {
            var gameObject = new GameObject();
            var behaviour = gameObject.AddComponent<EventTrigger>();
            var l = new List<EventTrigger>();
            Assert.IsFalse(Utility.AnyOfObjectsIsDestroyed(l));
            l.Add(behaviour);
            Assert.IsFalse(Utility.AnyOfObjectsIsDestroyed(l));
            Object.DestroyImmediate(behaviour);
            Assert.IsTrue(Utility.AnyOfObjectsIsDestroyed(l));
        }

        [Test]
        public void TestAllOfBehavioursAreEnabled() {
            var gameObject = new GameObject();
            var behaviour = gameObject.AddComponent<EventTrigger>();
            var l = new List<EventTrigger>();
            Assert.IsTrue(Utility.AllOfBehavioursAreEnabled(l));
            l.Add(behaviour);
            Assert.IsTrue(Utility.AllOfBehavioursAreEnabled(l));
            behaviour.enabled = false;
            Assert.IsFalse(Utility.AllOfBehavioursAreEnabled(l));
            gameObject.SetActive(false);
            Assert.IsFalse(Utility.AllOfBehavioursAreEnabled(l));
            behaviour.enabled = true;
            Assert.IsTrue(Utility.AllOfBehavioursAreEnabled(l));
            Object.DestroyImmediate(behaviour);
            _ = Assert.Throws(
                typeof(MissingReferenceException), () => Utility.AllOfBehavioursAreEnabled(l));
        }
    }
}
