using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableRect {
        [SerializeField] bool hasValue;
        [SerializeField] Rect value;

        public bool HasValue => hasValue;
        public Rect Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableRect(Rect value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public Rect GetValueOrDefault() => !hasValue ? default : value;
        public Rect GetValueOrDefault(Rect defaultValue) => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableRect(Rect value) => new NullableRect(value);
        public static explicit operator Rect(NullableRect value) => value.Value;
    }
}
