using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableAnimationCurve {
        [SerializeField] bool hasValue;
        [SerializeField] AnimationCurve value;

        public bool HasValue => hasValue;
        public AnimationCurve Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableAnimationCurve(AnimationCurve value) {
            hasValue = value is object;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public AnimationCurve GetValueOrDefault() => !hasValue ? default : value;
        public AnimationCurve GetValueOrDefault(AnimationCurve defaultValue)
            => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableAnimationCurve(AnimationCurve value)
            => new NullableAnimationCurve(value);
        public static explicit operator AnimationCurve(NullableAnimationCurve value) => value.Value;
    }
}
