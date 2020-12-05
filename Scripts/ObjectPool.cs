using UnityEngine;
using Component = UnityEngine.Component;
using Object = UnityEngine.Object;

namespace AgatePris.Apuu {
    public class ObjectPool<T> : UniRx.Toolkit.ObjectPool<T> where T : Component {
        readonly T original;
        bool afterCreateInstance = false;

        protected void CorrectTransform(in Transform transform) {
            if (afterCreateInstance) {
                afterCreateInstance = false;
                return;
            }
            transform.localScale = original.transform.localScale;
            transform.SetPositionAndRotation(
                original.transform.localPosition,
                original.transform.localRotation);
        }

        protected override T CreateInstance() {
            afterCreateInstance = true;
            return Object.Instantiate(original);
        }
        protected override void OnBeforeRent(T instance) {
            CorrectTransform(instance.transform);
            instance.gameObject.SetActive(true);
        }
        protected override void OnBeforeReturn(T instance) {
            instance.gameObject.SetActive(false);
            instance.transform.SetParent(null, false);
        }

        public ObjectPool(in T original) {
            this.original = original;
        }
    }
}
