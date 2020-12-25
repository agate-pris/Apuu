using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableShort {
        [SerializeField] bool hasValue;
        [SerializeField] short value;

        public bool HasValue => hasValue;
        public short Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableShort(short value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public short GetValueOrDefault() => !hasValue ? default : value;
        public short GetValueOrDefault(short defaultValue) => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableShort(short value) => new NullableShort(value);
        public static explicit operator short(NullableShort value) => value.Value;
    }
}
