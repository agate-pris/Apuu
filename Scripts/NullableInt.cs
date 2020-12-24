using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableInt {
        [SerializeField] bool hasValue;
        [SerializeField] int value;

        public bool HasValue => hasValue;
        public int Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableInt(int value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public int GetValueOrDefault() => !hasValue ? default : value;
        public int GetValueOrDefault(int defaultValue) => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableInt(int value) => new NullableInt(value);
        public static explicit operator int(NullableInt value) => value.Value;
    }
}
