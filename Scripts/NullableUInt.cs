using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableUInt {
        [SerializeField] bool hasValue;
        [SerializeField] uint value;

        public bool HasValue => hasValue;
        public uint Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableUInt(uint value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public uint GetValueOrDefault() => !hasValue ? default : value;
        public uint GetValueOrDefault(uint defaultValue) => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableUInt(uint value) => new NullableUInt(value);
        public static explicit operator uint(NullableUInt value) => value.Value;
    }
}
