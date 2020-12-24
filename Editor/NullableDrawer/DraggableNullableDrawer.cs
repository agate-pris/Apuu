using UnityEditor;
using UnityEngine;
using static UnityEditor.EditorGUI;
using static UnityEditor.EditorGUIUtility;

namespace AgatePris.Apuu.NullableDrawer {
    public class DraggableNullableDrawer : PropertyDrawer {
        static float LabelHeight => singleLineHeight;
        static float TogglePadding => EditorStyles.toggle.padding.left;

        static SerializedProperty HasValueProperty(in SerializedProperty property)
            => property.FindPropertyRelative("hasValue");
        static SerializedProperty ValueProperty(in SerializedProperty property)
            => property.FindPropertyRelative("value");

        readonly GUIContent valueLabel = new GUIContent(" ");

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => EditorGUI.GetPropertyHeight(ValueProperty(property), valueLabel, true);
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var indentWithTogglePadding = (indentLevel * 15) + TogglePadding;
            bool hasValue;
            {
                var width = Mathf.Min(
                    labelWidth, indentWithTogglePadding - standardVerticalSpacing);
                var pos = new Rect(position.x, position.y, width, LabelHeight);
                var prop = HasValueProperty(property);
                hasValue = prop.boolValue = Toggle(pos, prop.boolValue);
            }
            {
                var x = position.x + TogglePadding;
                var width = labelWidth - TogglePadding;
                var pos = new Rect(x, position.y, width, LabelHeight);
                LabelField(pos, label);
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
            using (new DisabledScope(!hasValue)) {
                var prop = ValueProperty(property);
                var offset = Mathf.Min(labelWidth, indentWithTogglePadding);
                Rect pos;
                {
                    var x = position.x + offset;
                    var width = position.width - offset;
                    var height = EditorGUI.GetPropertyHeight(prop, valueLabel, true);
                    pos = new Rect(x, position.y, width, height);
                }
                var labelWidthCopy = labelWidth;
                labelWidth = Mathf.Max(float.Epsilon, labelWidth - offset);
                var indentLevelCopy = indentLevel;
                indentLevel = 0;
                _ = PropertyField(pos, prop, valueLabel, true);
                indentLevel = indentLevelCopy;
                labelWidth = labelWidthCopy;
            }
        }
    }
}
