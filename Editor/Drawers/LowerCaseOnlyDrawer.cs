using AdventuresUnknownSDK.Core.Attributes;
using UnityEditor;
using UnityEngine;

namespace AdventuresUnknownSDK.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(LowerCaseOnlyAttribute))]
    public class LowerCaseOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property,
                                                GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position,
                                   SerializedProperty property,
                                   GUIContent label)
        {
            if (property.type.Equals(typeof(string).Name))
            {
                property.stringValue = property.stringValue.ToLowerInvariant();
            }
            EditorGUI.PropertyField(position, property, label, true);
        }
    }
}
