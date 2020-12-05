using UnityEngine;

namespace AgatePris.Apuu {
    public class Ref<T> : ScriptableObject {
        [SerializeField] T value = default;
        public T Value => value;
    }
}
