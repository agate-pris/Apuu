using UnityEngine;

namespace AgatePris.Apuu {
    public class ApuuTransformExt : MonoBehaviour {
        [SerializeField] TransformRef reference = null;

        void Awake() {
            if (reference) {
                reference.Value = transform;
            }
        }
        void OnDestroy() {
            if (reference) {
                reference.Value = null;
            }
        }
    }
}
