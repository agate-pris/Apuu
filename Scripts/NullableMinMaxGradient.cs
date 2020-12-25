using System;
using UnityEngine;
using MinMaxGradient = UnityEngine.ParticleSystem.MinMaxGradient;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableMinMaxGradient {
        [SerializeField] bool hasValue;
        [SerializeField] MinMaxGradient value;

        public bool HasValue => hasValue;
        public MinMaxGradient Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableMinMaxGradient(MinMaxGradient value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public MinMaxGradient GetValueOrDefault() => !hasValue ? default : value;
        public MinMaxGradient GetValueOrDefault(MinMaxGradient defaultValue)
            => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableMinMaxGradient(MinMaxGradient value)
            => new NullableMinMaxGradient(value);
        public static explicit operator MinMaxGradient(NullableMinMaxGradient value) => value.Value;
    }
}
