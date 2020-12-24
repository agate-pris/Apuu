using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableUShort {
        [SerializeField] bool hasValue;
        [SerializeField] ushort value;

        public bool HasValue => hasValue;
        public ushort Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableUShort(ushort value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public ushort GetValueOrDefault() => !hasValue ? default : value;
        public ushort GetValueOrDefault(ushort defaultValue) => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableUShort(ushort value) => new NullableUShort(value);
        public static explicit operator ushort(NullableUShort value) => value.Value;
    }
}
