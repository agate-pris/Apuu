using System;
using UnityEngine;

namespace AgatePris.Apuu {
    [Serializable]
    public struct NullableVector3Int {
        [SerializeField] bool hasValue;
        [SerializeField] Vector3Int value;

        public bool HasValue => hasValue;
        public Vector3Int Value => !hasValue
            ? throw new InvalidOperationException("Nullable object must have a value.") : value;

        public NullableVector3Int(Vector3Int value) {
            hasValue = true;
            this.value = value;
        }
        public override bool Equals(object other)
            => !hasValue ? other is null : value.Equals(other);
        public override int GetHashCode() => !hasValue ? 0 : value.GetHashCode();
        public Vector3Int GetValueOrDefault() => !hasValue ? default : value;
        public Vector3Int GetValueOrDefault(Vector3Int defaultValue)
            => !hasValue ? defaultValue : value;
        public override string ToString() => !hasValue ? "" : value.ToString();

        public static implicit operator NullableVector3Int(Vector3Int value)
            => new NullableVector3Int(value);
        public static explicit operator Vector3Int(NullableVector3Int value) => value.Value;
    }
}
