using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableQuaternion {
        [SerializeField] bool hasValue;
        [SerializeField] Quaternion value;

        public bool HasValue => hasValue;
        public Quaternion Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableQuaternion(Quaternion value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public Quaternion GetValueOrDefault() => !hasValue ? default : value;
        public Quaternion GetValueOrDefault(Quaternion defaultValue)
            => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableQuaternion(Quaternion value)
            => new NullableQuaternion(value);
        public static explicit operator Quaternion(NullableQuaternion value) => value.Value;
    }
}
