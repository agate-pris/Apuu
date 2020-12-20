using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableBool {
        [SerializeField] bool hasValue;
        [SerializeField] bool value;

        public bool HasValue => hasValue;
        public bool Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableBool(bool value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public bool GetValueOrDefault() => hasValue && value;
        public bool GetValueOrDefault(bool defaultValue) => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableBool(bool value) => new NullableBool(value);
        public static explicit operator bool(NullableBool value) => value.Value;
    }
}
