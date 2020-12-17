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

        [Test]
        public void TestAllOfBehavioursAreEnabledIncludeDestroyed() {
            var gameObject = new GameObject();
            var behaviour = gameObject.AddComponent<EventTrigger>();
            var l = new List<EventTrigger>();
            Assert.IsTrue(Utility.AllOfBehavioursAreEnabledIncludeDestroyed(l));
            l.Add(behaviour);
            Assert.IsTrue(Utility.AllOfBehavioursAreEnabledIncludeDestroyed(l));
            behaviour.enabled = false;
            Assert.IsFalse(Utility.AllOfBehavioursAreEnabledIncludeDestroyed(l));
            gameObject.SetActive(false);
            Assert.IsFalse(Utility.AllOfBehavioursAreEnabledIncludeDestroyed(l));
            behaviour.enabled = true;
            Assert.IsTrue(Utility.AllOfBehavioursAreEnabledIncludeDestroyed(l));
            gameObject.SetActive(true);
            Assert.IsTrue(Utility.AllOfBehavioursAreEnabledIncludeDestroyed(l));
            Object.DestroyImmediate(behaviour);
            Assert.IsFalse(Utility.AllOfBehavioursAreEnabledIncludeDestroyed(l));
        }

        [Test]
        public void TestAllOfBehavioursAreEnabledExcludeDestroyed() {
            var gameObject = new GameObject();
            var behaviour = gameObject.AddComponent<EventTrigger>();
            var l = new List<EventTrigger>();
            Assert.IsTrue(Utility.AllOfBehavioursAreEnabledExcludeDestroyed(l));
            l.Add(behaviour);
            Assert.IsTrue(Utility.AllOfBehavioursAreEnabledExcludeDestroyed(l));
            behaviour.enabled = false;
            Assert.IsFalse(Utility.AllOfBehavioursAreEnabledExcludeDestroyed(l));
            gameObject.SetActive(false);
            Assert.IsFalse(Utility.AllOfBehavioursAreEnabledExcludeDestroyed(l));
            behaviour.enabled = true;
            Assert.IsTrue(Utility.AllOfBehavioursAreEnabledExcludeDestroyed(l));
            gameObject.SetActive(true);
            Assert.IsTrue(Utility.AllOfBehavioursAreEnabledExcludeDestroyed(l));
            Object.DestroyImmediate(behaviour);
            Assert.IsTrue(Utility.AllOfBehavioursAreEnabledExcludeDestroyed(l));
        }

        [Test]
        public void TestAnyOfBehavioursIsEnabled() {
            var gameObject = new GameObject();
            var behaviour = gameObject.AddComponent<EventTrigger>();
            var l = new List<EventTrigger>();
            Assert.IsFalse(Utility.AnyOfBehavioursIsEnabled(l));
            l.Add(behaviour);
            Assert.IsTrue(Utility.AnyOfBehavioursIsEnabled(l));
            behaviour.enabled = false;
            Assert.IsFalse(Utility.AnyOfBehavioursIsEnabled(l));
            gameObject.SetActive(false);
            Assert.IsFalse(Utility.AnyOfBehavioursIsEnabled(l));
            behaviour.enabled = true;
            Assert.IsTrue(Utility.AnyOfBehavioursIsEnabled(l));
            gameObject.SetActive(true);
            Assert.IsTrue(Utility.AnyOfBehavioursIsEnabled(l));
            Object.DestroyImmediate(behaviour);
            Assert.IsFalse(Utility.AnyOfBehavioursIsEnabled(l));
        }

        [Test]
        public void TestAllOfBehavioursAreActiveAndEnabled() {
            var gameObject = new GameObject();
            var behaviour = gameObject.AddComponent<EventTrigger>();
            var l = new List<EventTrigger>();
            Assert.IsTrue(Utility.AllOfBehavioursAreActiveAndEnabled(l));
            l.Add(behaviour);
            Assert.IsTrue(Utility.AllOfBehavioursAreActiveAndEnabled(l));
            behaviour.enabled = false;
            Assert.IsFalse(Utility.AllOfBehavioursAreActiveAndEnabled(l));
            gameObject.SetActive(false);
            Assert.IsFalse(Utility.AllOfBehavioursAreActiveAndEnabled(l));
            behaviour.enabled = true;
            Assert.IsFalse(Utility.AllOfBehavioursAreActiveAndEnabled(l));
            gameObject.SetActive(true);
            Assert.IsTrue(Utility.AllOfBehavioursAreActiveAndEnabled(l));
            Object.DestroyImmediate(behaviour);
            _ = Assert.Throws(
                typeof(MissingReferenceException),
                () => Utility.AllOfBehavioursAreActiveAndEnabled(l));
        }

        [Test]
        public void TestAllOfBehavioursAreActiveAndEnabledIncludeDestroyed() {
            var gameObject = new GameObject();
            var behaviour = gameObject.AddComponent<EventTrigger>();
            var l = new List<EventTrigger>();
            Assert.IsTrue(Utility.AllOfBehavioursAreActiveAndEnabledIncludeDestroyed(l));
            l.Add(behaviour);
            Assert.IsTrue(Utility.AllOfBehavioursAreActiveAndEnabledIncludeDestroyed(l));
            behaviour.enabled = false;
            Assert.IsFalse(Utility.AllOfBehavioursAreActiveAndEnabledIncludeDestroyed(l));
            gameObject.SetActive(false);
            Assert.IsFalse(Utility.AllOfBehavioursAreActiveAndEnabledIncludeDestroyed(l));
            behaviour.enabled = true;
            Assert.IsFalse(Utility.AllOfBehavioursAreActiveAndEnabledIncludeDestroyed(l));
            gameObject.SetActive(true);
            Assert.IsTrue(Utility.AllOfBehavioursAreActiveAndEnabledIncludeDestroyed(l));
            Object.DestroyImmediate(behaviour);
            Assert.IsFalse(Utility.AllOfBehavioursAreActiveAndEnabledIncludeDestroyed(l));
        }

        [Test]
        public void TestAllOfBehavioursAreActiveAndEnabledExcludeDestroyed() {
            var gameObject = new GameObject();
            var behaviour = gameObject.AddComponent<EventTrigger>();
            var l = new List<EventTrigger>();
            Assert.IsTrue(Utility.AllOfBehavioursAreActiveAndEnabledExcludeDestroyed(l));
            l.Add(behaviour);
            Assert.IsTrue(Utility.AllOfBehavioursAreActiveAndEnabledExcludeDestroyed(l));
            behaviour.enabled = false;
            Assert.IsFalse(Utility.AllOfBehavioursAreActiveAndEnabledExcludeDestroyed(l));
            gameObject.SetActive(false);
            Assert.IsFalse(Utility.AllOfBehavioursAreActiveAndEnabledExcludeDestroyed(l));
            behaviour.enabled = true;
            Assert.IsFalse(Utility.AllOfBehavioursAreActiveAndEnabledExcludeDestroyed(l));
            gameObject.SetActive(true);
            Assert.IsTrue(Utility.AllOfBehavioursAreActiveAndEnabledExcludeDestroyed(l));
            Object.DestroyImmediate(behaviour);
            Assert.IsTrue(Utility.AllOfBehavioursAreActiveAndEnabledExcludeDestroyed(l));
        }
    }
}
