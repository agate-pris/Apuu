using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableColor {
        [SerializeField] bool hasValue;
        [SerializeField] Color value;

        public bool HasValue => hasValue;
        public Color Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableColor(Color value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public Color GetValueOrDefault() => !hasValue ? default : value;
        public Color GetValueOrDefault(Color defaultValue) => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableColor(Color value) => new NullableColor(value);
        public static explicit operator Color(NullableColor value) => value.Value;
    }
}
