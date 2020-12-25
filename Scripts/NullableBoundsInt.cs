using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableBoundsInt {
        [SerializeField] bool hasValue;
        [SerializeField] BoundsInt value;

        public bool HasValue => hasValue;
        public BoundsInt Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableBoundsInt(BoundsInt value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public BoundsInt GetValueOrDefault() => !hasValue ? default : value;
        public BoundsInt GetValueOrDefault(BoundsInt defaultValue)
            => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableBoundsInt(BoundsInt value)
            => new NullableBoundsInt(value);
        public static explicit operator BoundsInt(NullableBoundsInt value) => value.Value;
    }
}
