using AdventuresUnknownSDK.Core.Objects.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace AdventuresUnknownSDK.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(LocalizationString))]
    public class LocalizationStringDrawer : PropertyDrawer
    {


        #region Properties

        #endregion

        #region Methods
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty serializedProperty = property.FindPropertyRelative("m_LocalizedIdentifier");
            return base.GetPropertyHeight(serializedProperty, label);
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty serializedProperty = property.FindPropertyRelative("m_LocalizedIdentifier");
            serializedProperty.stringValue = EditorGUI.TextField(position, label, serializedProperty.stringValue).ToLowerInvariant();
        }
        #endregion
    }
}
