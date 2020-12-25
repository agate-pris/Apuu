using UnityEditor;
using UnityEngine;
using static UnityEditor.EditorGUI;
using static UnityEditor.EditorGUIUtility;

namespace AgatePris.Apuu.NullableDrawer {
    public class OverlapWrapNullableDrawer : PropertyDrawer {
        static float LabelHeight => singleLineHeight;

        SerializedProperty HasValueProperty(in SerializedProperty property)
            => property.FindPropertyRelative("hasValue");
        SerializedProperty ValueProperty(in SerializedProperty property)
            => property.FindPropertyRelative("value");

        readonly GUIContent valueLabel = new GUIContent(" ");

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => EditorGUI.GetPropertyHeight(ValueProperty(property), valueLabel, true);
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            bool hasValue;
            {
                var prop = HasValueProperty(property);
                if (wideMode) {
                    {
                        var pos = new Rect(position.x, position.y, labelWidth, LabelHeight);
                        hasValue = prop.boolValue = ToggleLeft(pos, label, prop.boolValue);
                    }
                    {
                        Rect rect;
                        {
                            var x = position.x + labelWidth;
                            var width = position.width - labelWidth;
                            rect = new Rect(x, position.y, width, LabelHeight);
                        }
                        Color color = isProSkin
                            ? new Color32(56, 56, 56, 255)
                            : new Color32(194, 194, 194, 255);
                        DrawRect(rect, color);
                    }
                } else {
                    var pos = new Rect(position.x, position.y, position.width, LabelHeight);
                    hasValue = prop.boolValue = ToggleLeft(pos, label, prop.boolValue);
                }
            }
            using (new DisabledScope(!hasValue)) {
                var prop = ValueProperty(property);
                _ = PropertyField(position, prop, valueLabel, true);
            }
        }
    }
}
