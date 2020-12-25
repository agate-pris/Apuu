using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableVector3 {
        [SerializeField] bool hasValue;
        [SerializeField] Vector3 value;

        public bool HasValue => hasValue;
        public Vector3 Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableVector3(Vector3 value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public Vector3 GetValueOrDefault() => !hasValue ? default : value;
        public Vector3 GetValueOrDefault(Vector3 defaultValue) => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableVector3(Vector3 value)
            => new NullableVector3(value);
        public static explicit operator Vector3(NullableVector3 value) => value.Value;
    }
}
