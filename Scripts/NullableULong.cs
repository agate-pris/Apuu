using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableULong {
        [SerializeField] bool hasValue;
        [SerializeField] ulong value;

        public bool HasValue => hasValue;
        public ulong Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableULong(ulong value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public ulong GetValueOrDefault() => !hasValue ? default : value;
        public ulong GetValueOrDefault(ulong defaultValue) => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableULong(ulong value) => new NullableULong(value);
        public static explicit operator ulong(NullableULong value) => value.Value;
    }
}
