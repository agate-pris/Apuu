using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableVector2Int {
        [SerializeField] bool hasValue;
        [SerializeField] Vector2Int value;

        public bool HasValue => hasValue;
        public Vector2Int Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableVector2Int(Vector2Int value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public Vector2Int GetValueOrDefault() => !hasValue ? default : value;
        public Vector2Int GetValueOrDefault(Vector2Int defaultValue)
            => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableVector2Int(Vector2Int value)
            => new NullableVector2Int(value);
        public static explicit operator Vector2Int(NullableVector2Int value) => value.Value;
    }
}
