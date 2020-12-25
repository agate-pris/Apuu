using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableByte {
        [SerializeField] bool hasValue;
        [SerializeField] byte value;

        public bool HasValue => hasValue;
        public byte Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableByte(byte value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public byte GetValueOrDefault() => !hasValue ? default : value;
        public byte GetValueOrDefault(byte defaultValue) => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableByte(byte value) => new NullableByte(value);
        public static explicit operator byte(NullableByte value) => value.Value;
    }
}
