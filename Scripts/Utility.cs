using UnityEngine;

namespace AgatePris.Apuu {
    public class Utility {
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
    }
}
