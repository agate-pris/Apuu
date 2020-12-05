using UnityEngine;

namespace AgatePris.Apuu {
    public class RefMut<T> : ScriptableObject {
        public T Value { get; set; }
    }
}
