using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableRectInt {
        [SerializeField] bool hasValue;
        [SerializeField] RectInt value;

        public bool HasValue => hasValue;
        public RectInt Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableRectInt(RectInt value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public RectInt GetValueOrDefault() => !hasValue ? default : value;
        public RectInt GetValueOrDefault(RectInt defaultValue)
            => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableRectInt(RectInt value)
            => new NullableRectInt(value);
        public static explicit operator RectInt(NullableRectInt value) => value.Value;
    }
}
