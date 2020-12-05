using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace AgatePris.Apuu {
    public class ObjectPoolTest {
        static void AssertMatricesAreEqual(in Matrix4x4 expected, in Matrix4x4 actual) {
            for (var row = 0; row < 4; ++row) {
                for (var col = 0; col < 4; ++col) {
                    Assert.AreApproximatelyEqual(
                        expected[row, col],
                        actual[row, col],
                        0.001f,
                        $"expected:\n{expected}\nactual:\n{actual}");
                }
            }
        }
        static Transform CreateTransform() => new GameObject().transform;
        static Transform CreateTransform(in Transform parent) {
            var t = CreateTransform();
            t.SetParent(parent, false);
            return t;
        }
        static void RandomizeTransform(in Transform transform) {
            transform.localPosition = Random.insideUnitSphere;
            transform.localRotation = Random.rotation;
            transform.localScale = Vector3.one + Random.insideUnitSphere;
        }

        readonly Transform originalParentParent;
        readonly Transform originalParent;
        readonly Transform original;
        readonly Transform parentParentParent;
        readonly Transform parentParent;
        readonly Transform parent;
        readonly ObjectPool<Transform> objectPool;

        public ObjectPoolTest() {
            originalParentParent = CreateTransform();
            originalParent = CreateTransform(originalParentParent);
            original = CreateTransform(original);
            parentParentParent = CreateTransform();
            parentParent = CreateTransform(parentParentParent);
            parent = CreateTransform(parentParent);
            objectPool = new ObjectPool<Transform>(original);
        }

        Transform RentTest() {
            var expected = Object.Instantiate(original);
            var rent = objectPool.Rent();

            Assert.AreEqual(null, rent.parent);
            AssertMatricesAreEqual(expected.localToWorldMatrix, rent.localToWorldMatrix);

            Object.DestroyImmediate(expected.gameObject);

            return rent;
        }
        Transform RentAtPositionAndRotationTest() {
            var position = Random.insideUnitSphere;
            var rotation = Random.rotation;

            var expected = Object.Instantiate(original, position, rotation);
            var rent = objectPool.Rent(position, rotation);

            Assert.AreEqual(null, rent.parent);
            AssertMatricesAreEqual(expected.localToWorldMatrix, rent.localToWorldMatrix);

            Object.DestroyImmediate(expected.gameObject);

            return rent;
        }
        Transform RentAtPositionAndRotationInLocalSpaceTest() {
            var position = Random.insideUnitSphere;
            var rotation = Random.rotation;

            var expected = Object.Instantiate(original, position, rotation, parent);
            var rent = objectPool.Rent(position, rotation, parent);

            Assert.AreEqual(parent, rent.parent);
            AssertMatricesAreEqual(expected.localToWorldMatrix, rent.localToWorldMatrix);

            Object.DestroyImmediate(expected.gameObject);

            return rent;
        }

        [Test]
        public void Test() {
            const int numberOfTrials = 99;

            Transform test() {
                RandomizeTransform(originalParentParent);
                RandomizeTransform(originalParent);
                RandomizeTransform(original);
                RandomizeTransform(parentParentParent);
                RandomizeTransform(parentParent);
                RandomizeTransform(parent);

                switch (Random.Range(0, 3)) {
                    case 0: { return RentTest(); }
                    case 1: { return RentAtPositionAndRotationTest(); }
                    case 2: { return RentAtPositionAndRotationInLocalSpaceTest(); }
                    default: { throw new InvalidOperationException(); }
                }
            }

            {
                var rentList = new List<Transform>();
                for (var i = 0; i < numberOfTrials; ++i) {
                    rentList.Add(test());
                }
            }

            for (var i = 0; i < numberOfTrials; ++i) {
                _ = test();
            }
        }
    }
}
