using System;
using AdventuresUnknownSDK.Core.Objects.Localization;
using UnityEditor;
using UnityEngine;

namespace AdventuresUnknownSDK.Editor.Editors
{
    [CustomEditor(typeof(LocalizationTable), true)]
    public class LocalizationTableEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            LocalizationTable coreObject = target as LocalizationTable;
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script", MonoScript.FromScriptableObject((LocalizationTable)target), typeof(LocalizationTable), false);
            GUI.enabled = true;
            DrawPropertiesExcluding(serializedObject,"m_Script", "m_LocalizationData");

            SerializedProperty localizationDataProperty = serializedObject.FindProperty("m_LocalizationData");
            if (localizationDataProperty.isExpanded = EditorGUILayout.Foldout(localizationDataProperty.isExpanded, localizationDataProperty.displayName))
            {
                EditorGUI.indentLevel++;
                GUILayout.BeginHorizontal();
                GUILayout.Space(EditorGUI.indentLevel * 10);
                if (GUILayout.Button("Sort"))
                {
                    SortSerializedProperty(localizationDataProperty, 
                        (first, second) => 
                        {
                            return first.FindPropertyRelative("m_Identifier").stringValue.CompareTo(second.FindPropertyRelative("m_Identifier").stringValue) > 0;
                        });
                }
                GUILayout.EndHorizontal();
                if (localizationDataProperty.isArray)
                {
                    for(int i = 0; i < localizationDataProperty.arraySize; i++)
                    {
                        SerializedProperty elementProperty = localizationDataProperty.GetArrayElementAtIndex(i);
                        SerializedProperty identifierProperty = elementProperty.FindPropertyRelative("m_Identifier");
                        SerializedProperty stringProperty = elementProperty.FindPropertyRelative("m_String");
                        EditorGUILayout.BeginHorizontal();
                        identifierProperty.stringValue = EditorGUILayout.TextField(identifierProperty.stringValue).ToLowerInvariant();
                        stringProperty.stringValue = EditorGUILayout.TextField(stringProperty.stringValue);
                        if (GUILayout.Button("D"))
                        {
                            localizationDataProperty.DeleteArrayElementAtIndex(i);
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
                GUILayout.BeginHorizontal();
                GUILayout.Space(EditorGUI.indentLevel * 10);
                if (GUILayout.Button("Add"))
                {
                    localizationDataProperty.arraySize++;
                }
                GUILayout.EndHorizontal();
                EditorGUI.indentLevel--;
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void SortSerializedProperty(SerializedProperty localizationDataProperty, Func<SerializedProperty, SerializedProperty, bool> predicate)
        {
            if (localizationDataProperty == null) return;
            if (!localizationDataProperty.isArray) return;
            if (localizationDataProperty.arraySize == 0) return;

            for(int i = 0; i < localizationDataProperty.arraySize; i++)
            {
                for (int j = i+1; j < localizationDataProperty.arraySize; j++)
                {
                    SerializedProperty first = localizationDataProperty.GetArrayElementAtIndex(i);
                    SerializedProperty second = localizationDataProperty.GetArrayElementAtIndex(j);

                    if (predicate(first, second))
                    {
                        localizationDataProperty.MoveArrayElement(i, j);
                        localizationDataProperty.MoveArrayElement(j - 1, i);
                    }
                }
            }
        }
    }
}
