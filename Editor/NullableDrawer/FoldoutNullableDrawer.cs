using UnityEditor;
using UnityEngine;
using static UnityEditor.EditorGUI;
using static UnityEditor.EditorGUIUtility;

namespace AgatePris.Apuu.NullableDrawer {
    public class FoldoutNullableDrawer : PropertyDrawer {
        float LabelHeight => singleLineHeight;
        SerializedProperty HasValueProperty(in SerializedProperty property)
            => property.FindPropertyRelative("hasValue");
        SerializedProperty ValueProperty(in SerializedProperty property)
            => property.FindPropertyRelative("value");
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => !property.isExpanded
            ? LabelHeight
            : LabelHeight
            + EditorGUI.GetPropertyHeight(HasValueProperty(property), true)
            + EditorGUI.GetPropertyHeight(ValueProperty(property), true);
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            {
                var pos = new Rect(position.x, position.y, position.width, LabelHeight);
                if (!(property.isExpanded = Foldout(pos, property.isExpanded, label, true))) {
                    return;
                }
            }
            ++indentLevel;
            float hasValueHeight;
            bool hasValue;
            {
                var prop = HasValueProperty(property);
                hasValueHeight = EditorGUI.GetPropertyHeight(prop, true);
                Rect pos;
                {
                    var y = position.y + LabelHeight;
                    pos = new Rect(position.x, y, position.width, hasValueHeight);
                }
                _ = PropertyField(pos, prop, true);
                hasValue = prop.boolValue;
            }
            using (new DisabledScope(!hasValue)) {
                var prop = ValueProperty(property);
                Rect pos;
                {
                    var y = position.y + LabelHeight + hasValueHeight;
                    var height = EditorGUI.GetPropertyHeight(prop, true);
                    pos = new Rect(position.x, y, position.width, height);
                }
                _ = PropertyField(pos, prop, true);
            }
            --indentLevel;
        }
    }
}
