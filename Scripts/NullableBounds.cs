using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableBounds {
        [SerializeField] bool hasValue;
        [SerializeField] Bounds value;

        public bool HasValue => hasValue;
        public Bounds Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableBounds(Bounds value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public Bounds GetValueOrDefault() => !hasValue ? default : value;
        public Bounds GetValueOrDefault(Bounds defaultValue)
            => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableBounds(Bounds value)
            => new NullableBounds(value);
        public static explicit operator Bounds(NullableBounds value) => value.Value;
    }
}
