using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableSByte {
        [SerializeField] bool hasValue;
        [SerializeField] sbyte value;

        public bool HasValue => hasValue;
        public sbyte Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableSByte(sbyte value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public sbyte GetValueOrDefault() => !hasValue ? default : value;
        public sbyte GetValueOrDefault(sbyte defaultValue) => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableSByte(sbyte value) => new NullableSByte(value);
        public static explicit operator sbyte(NullableSByte value) => value.Value;
    }
}
