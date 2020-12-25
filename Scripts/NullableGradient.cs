using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableGradient {
        [SerializeField] bool hasValue;
        [SerializeField] Gradient value;

        public bool HasValue => hasValue;
        public Gradient Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableGradient(Gradient value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public Gradient GetValueOrDefault() => !hasValue ? default : value;
        public Gradient GetValueOrDefault(Gradient defaultValue)
            => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableGradient(Gradient value)
            => new NullableGradient(value);
        public static explicit operator Gradient(NullableGradient value) => value.Value;
    }
}
