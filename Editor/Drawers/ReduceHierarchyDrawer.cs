using AdventuresUnknownSDK.Core.Attributes;
using UnityEditor;
using UnityEngine;

namespace AdventuresUnknownSDK.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(ReduceHierarchyAttribute))]
    public class ReduceHierarchyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property,
                                                GUIContent label)
        {
            bool original = property.isExpanded;
            property.isExpanded = true;
            SerializedProperty copy = property.Copy();
            int count = copy.CountInProperty();
            property.isExpanded = original;
            if (count <= 2 && property.hasVisibleChildren)
            {
                property.NextVisible(true);
                return EditorGUI.GetPropertyHeight(property, label, true);
            }
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position,
                                   SerializedProperty property,
                                   GUIContent label)
        {
            bool original = property.isExpanded;
            property.isExpanded = true;
            SerializedProperty copy = property.Copy();
            int count = copy.CountInProperty();
            property.isExpanded = original;
            if (count <= 2 && property.hasVisibleChildren)
            {
                property.NextVisible(true);
                EditorGUI.PropertyField(position, property, label, true);
            }
            else
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }
    }
}
