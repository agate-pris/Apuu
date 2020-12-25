using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableVector4 {
        [SerializeField] bool hasValue;
        [SerializeField] Vector4 value;

        public bool HasValue => hasValue;
        public Vector4 Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableVector4(Vector4 value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public Vector4 GetValueOrDefault() => !hasValue ? default : value;
        public Vector4 GetValueOrDefault(Vector4 defaultValue) => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableVector4(Vector4 value)
            => new NullableVector4(value);
        public static explicit operator Vector4(NullableVector4 value) => value.Value;
    }
}
