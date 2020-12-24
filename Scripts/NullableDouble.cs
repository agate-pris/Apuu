using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableDouble {
        [SerializeField] bool hasValue;
        [SerializeField] double value;

        public bool HasValue => hasValue;
        public double Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableDouble(double value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public double GetValueOrDefault() => !hasValue ? default : value;
        public double GetValueOrDefault(double defaultValue) => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableDouble(double value) => new NullableDouble(value);
        public static explicit operator double(NullableDouble value) => value.Value;
    }
}
