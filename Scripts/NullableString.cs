using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableString {
        [SerializeField] bool hasValue;
        [SerializeField] string value;

        public bool HasValue => hasValue;
        public string Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableString(string value) {
            hasValue = value is object;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public string GetValueOrDefault() => !hasValue ? default : value;
        public string GetValueOrDefault(string defaultValue) => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableString(string value) => new NullableString(value);
        public static explicit operator string(NullableString value) => value.Value;
    }
}
