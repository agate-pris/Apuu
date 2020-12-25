using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableLong {
        [SerializeField] bool hasValue;
        [SerializeField] long value;

        public bool HasValue => hasValue;
        public long Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableLong(long value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public long GetValueOrDefault() => !hasValue ? default : value;
        public long GetValueOrDefault(long defaultValue) => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableLong(long value) => new NullableLong(value);
        public static explicit operator long(NullableLong value) => value.Value;
    }
}
