using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableVector2 {
        [SerializeField] bool hasValue;
        [SerializeField] Vector2 value;

        public bool HasValue => hasValue;
        public Vector2 Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableVector2(Vector2 value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public Vector2 GetValueOrDefault() => !hasValue ? default : value;
        public Vector2 GetValueOrDefault(Vector2 defaultValue) => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableVector2(Vector2 value)
            => new NullableVector2(value);
        public static explicit operator Vector2(NullableVector2 value) => value.Value;
    }
}
