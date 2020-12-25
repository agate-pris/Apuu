using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableChar {
        [SerializeField] bool hasValue;
        [SerializeField] char value;

        public bool HasValue => hasValue;
        public char Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableChar(char value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public char GetValueOrDefault() => !hasValue ? default : value;
        public char GetValueOrDefault(char defaultValue) => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableChar(char value) => new NullableChar(value);
        public static explicit operator char(NullableChar value) => value.Value;
    }
}
