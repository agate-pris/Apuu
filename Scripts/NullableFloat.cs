using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableFloat {
        [SerializeField] bool hasValue;
        [SerializeField] float value;

        public bool HasValue => hasValue;
        public float Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableFloat(float value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public float GetValueOrDefault() => !hasValue ? default : value;
        public float GetValueOrDefault(float defaultValue) => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableFloat(float value) => new NullableFloat(value);
        public static explicit operator float(NullableFloat value) => value.Value;
    }
}
