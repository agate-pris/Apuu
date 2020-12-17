using System.Collections.Generic;
using UnityEngine;

namespace AgatePris.Apuu {
    public class Utility {
        public static bool AllOfObjectsAreDestroyed<T>(in T objects) where T : IEnumerable<Object> {
            foreach (var i in objects) {
                if (i) {
                    return false;
                }
            }
            return true;
        }
        public static bool AnyOfObjectsIsDestroyed<T>(in T objects) where T : IEnumerable<Object> {
            foreach (var i in objects) {
                if (!i) {
                    return true;
                }
            }
            return false;
        }
        public static bool AllOfBehavioursAreEnabled<T>(in T behaviours)
            where T : IEnumerable<Behaviour> {
            foreach (var i in behaviours) {
                if (!i.enabled) {
                    return false;
                }
            }
            return true;
        }
        public static bool AllOfBehavioursAreEnabledIncludeDestroyed<T>(in T behaviours)
            where T : IEnumerable<Behaviour> {
            foreach (var i in behaviours) {
                if (!i || !i.enabled) {
                    return false;
                }
            }
            return true;
        }
        public static bool AllOfBehavioursAreEnabledExcludeDestroyed<T>(in T behaviours)
            where T : IEnumerable<Behaviour> {
            foreach (var i in behaviours) {
                if (!i) {
                    continue;
                }
                if (!i.enabled) {
                    return false;
                }
            }
            return true;
        }
        public static bool AnyOfBehavioursIsEnabled<T>(in T behaviours)
            where T : IEnumerable<Behaviour> {
            foreach (var i in behaviours) {
                if (!i) {
                    continue;
                }
                if (i.enabled) {
                    return true;
                }
            }
            return false;
        }
        public static bool AllOfBehavioursAreActiveAndEnabled<T>(in T behaviours)
            where T : IEnumerable<Behaviour> {
            foreach (var i in behaviours) {
                if (!i.isActiveAndEnabled) {
                    return false;
                }
            }
            return true;
        }
        public static bool AllOfBehavioursAreActiveAndEnabledIncludeDestroyed<T>(in T behaviours)
            where T : IEnumerable<Behaviour> {
            foreach (var i in behaviours) {
                if (!i || !i.isActiveAndEnabled) {
                    return false;
                }
            }
            return true;
        }
        public static bool AllOfBehavioursAreActiveAndEnabledExcludeDestroyed<T>(in T behaviours)
            where T : IEnumerable<Behaviour> {
            foreach (var i in behaviours) {
                if (!i) {
                    continue;
                }
                if (!i.isActiveAndEnabled) {
                    return false;
                }
            }
            return true;
        }
        public static T InstantiateWithOriginalPositionAndRotation<T>(
            in T original, in Vector3 position, in Quaternion rotation) where T : Component
            => Object.Instantiate(
                original,
                position + (rotation * original.transform.localPosition),
                rotation * original.transform.localRotation);
        public static T InstantiateWithOriginalPositionAndRotation<T>(
            in T original,
            in Vector3 position,
            in Quaternion rotation,
            in Transform parent) where T : Component {
            var matrix
                = parent.localToWorldMatrix
                * Matrix4x4.Rotate(Quaternion.Inverse(parent.rotation) * rotation);
            return Object.Instantiate(
                original,
                position + matrix.MultiplyVector(original.transform.localPosition),
                rotation * original.transform.localRotation,
                parent);
        }
        public static void SimulateTransform(
            in Transform transform,
            in Vector3 parentPosition,
            in Quaternion parentRotation,
            in Vector3 childLocalPosition,
            in Quaternion childLocalRotation)
            => transform.SetPositionAndRotation(
                parentPosition + (parentRotation * childLocalPosition),
                parentRotation * childLocalRotation);
        public static void SimulateTransform(
            in Transform transform,
            in Vector3 parentPosition,
            in Quaternion parentRotation,
            in Vector3 childLocalPosition,
            in Quaternion childLocalRotation,
            in Transform actualParent) {
            transform.SetParent(actualParent, false);
            var vector
                = Quaternion.Inverse(actualParent.rotation) * parentRotation * childLocalPosition;
            transform.SetPositionAndRotation(
                parentPosition + actualParent.TransformVector(vector),
                parentRotation * childLocalRotation);
        }
    }
}
