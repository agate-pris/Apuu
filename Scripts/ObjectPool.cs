using System;
using UnityEngine;
using Component = UnityEngine.Component;
using Object = UnityEngine.Object;

namespace AgatePris.Apuu {
    public class ObjectPool<T> : UniRx.Toolkit.ObjectPool<T> where T : Component {
        enum Pattern {
            Default,
            AtPositionAndRotation,
            AtPositionAndRotationInLocalSpace,
        }

        readonly T original;
        Pattern pattern;
        Vector3 position;
        Quaternion rotation;
        Transform parent;
        bool afterCreateInstance = false;

        protected void CorrectTransform(in Transform transform) {
            if (afterCreateInstance) {
                afterCreateInstance = false;
                return;
            }
            switch (pattern) {
                case Pattern.Default: {
                    transform.localScale = original.transform.localScale;
                    transform.SetPositionAndRotation(
                        original.transform.localPosition,
                        original.transform.localRotation);
                    break;
                }
                case Pattern.AtPositionAndRotation: {
                    transform.localScale = original.transform.localScale;
                    transform.SetPositionAndRotation(position, rotation);
                    break;
                }
                case Pattern.AtPositionAndRotationInLocalSpace: {
                    transform.localScale = original.transform.localScale;
                    transform.SetParent(parent, false);
                    transform.SetPositionAndRotation(position, rotation);
                    break;
                }
                default: { throw new InvalidOperationException(); }
            }
        }

        protected override T CreateInstance() {
            afterCreateInstance = true;
            switch (pattern) {
                case Pattern.Default: { return Object.Instantiate(original); }
                case Pattern.AtPositionAndRotation: {
                    return Object.Instantiate(original, position, rotation);
                }
                case Pattern.AtPositionAndRotationInLocalSpace: {
                    return Object.Instantiate(original, position, rotation, parent);
                }
                default: { throw new InvalidOperationException(); }
            }
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
        public T Rent(in Vector3 position, in Quaternion rotation) {
            pattern = Pattern.AtPositionAndRotation;
            this.position = position;
            this.rotation = rotation;
            try {
                var instance = Rent();
                pattern = Pattern.Default;
                return instance;
            } catch (Exception e) {
                pattern = Pattern.Default;
                throw e;
            }
        }
        public T Rent(in Vector3 position, in Quaternion rotation, in Transform parent) {
            pattern = Pattern.AtPositionAndRotationInLocalSpace;
            this.position = position;
            this.rotation = rotation;
            this.parent = parent;
            try {
                var instance = Rent();
                pattern = Pattern.Default;
                this.parent = null;
                return instance;
            } catch (Exception e) {
                pattern = Pattern.Default;
                this.parent = null;
                throw e;
            }
        }
    }
}
