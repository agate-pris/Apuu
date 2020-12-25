using System;
using UnityEngine;
using MinMaxCurve = UnityEngine.ParticleSystem.MinMaxCurve;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableMinMaxCurve {
        [SerializeField] bool hasValue;
        [SerializeField] MinMaxCurve value;

        public bool HasValue => hasValue;
        public MinMaxCurve Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableMinMaxCurve(MinMaxCurve value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public MinMaxCurve GetValueOrDefault() => !hasValue ? default : value;
        public MinMaxCurve GetValueOrDefault(MinMaxCurve defaultValue)
            => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableMinMaxCurve(MinMaxCurve value)
            => new NullableMinMaxCurve(value);
        public static explicit operator MinMaxCurve(NullableMinMaxCurve value)
            => value.Value;
    }
}
