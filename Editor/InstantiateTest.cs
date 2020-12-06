using NUnit.Framework;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

namespace AgatePris.Apuu {
    public class InstantiateTest {
        [Test]
        public void Test() {
            var parent = new GameObject();
            parent.transform.localPosition = Random.insideUnitSphere;
            parent.transform.localRotation = Random.rotation;
            parent.transform.localScale = Vector3.one + Random.insideUnitSphere;
            var child = new GameObject();
            child.transform.SetParent(parent.transform, false);
            child.transform.localPosition = Random.insideUnitSphere;
            child.transform.localRotation = Random.rotation;
            child.transform.localScale = Vector3.one + Random.insideUnitSphere;
            var childClone = Object.Instantiate(child.transform);
            Assert.IsNull(childClone.parent);
            Assert.AreEqual(child.transform.localPosition, childClone.transform.localPosition);
            Assert.AreEqual(child.transform.localRotation, childClone.transform.localRotation);
            Assert.AreEqual(child.transform.localScale, childClone.transform.localScale);
        }
    }
}
