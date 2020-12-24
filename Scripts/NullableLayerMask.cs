using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableLayerMask {
        [SerializeField] bool hasValue;
        [SerializeField] LayerMask value;

        public bool HasValue => hasValue;
        public LayerMask Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableLayerMask(LayerMask value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public LayerMask GetValueOrDefault() => !hasValue ? default : value;
        public LayerMask GetValueOrDefault(LayerMask defaultValue)
            => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableLayerMask(LayerMask value)
            => new NullableLayerMask(value);
        public static explicit operator LayerMask(NullableLayerMask value) => value.Value;
    }
}
