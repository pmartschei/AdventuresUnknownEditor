using System;
using System.IO;
using AdventuresUnknownSDK.Core.Objects.Localization;
using UnityEditor;
using UnityEngine;

namespace AdventuresUnknownSDK.Core.Objects.Mods
{
    [CustomEditor(typeof(AttributesObject), true)]
    public class AttributesObjectEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            AttributesObject attributesObject = target as AttributesObject;
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script", MonoScript.FromScriptableObject((AttributesObject)target), typeof(AttributesObject), false);
            GUI.enabled = true;
            DrawPropertiesExcluding(serializedObject,"m_Script");

            if (GUILayout.Button("Import"))
            {
                string path = EditorUtility.OpenFilePanel("Import Attributes via CSV", "", "csv");
                if (path.Length != 0)
                {
                    string text = File.ReadAllText(path);
                    string[] lines = text.Split(new[] { Environment.NewLine },StringSplitOptions.None);
                    attributesObject.Attributes = new Attribute[lines.Length - 3];
                    for(int i = 0; i < attributesObject.Attributes.Length; i++)
                    {
                        attributesObject.Attributes[i] = new Attribute();
                    }
                    for(int i = 0; i < lines.Length-3; i++)
                    {
                        string[] values = lines[i+3].Split(',');
                        LoadAttribute(values, attributesObject.Attributes[i]);
                    }
                }
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void LoadAttribute(string[] values, Attribute attribute)
        {
            int[] levels = new int[] { 1, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100 };

            bool hasDifferentValues = false;
            float lastValue = 0.0f;
            bool isFirstValue = true;

            for (int j = 1; j < Math.Min(levels.Length + 1, values.Length); j++)
            {
                float currentValue = 0.0f;
                if (float.TryParse(values[j], out currentValue))
                {
                    if (!isFirstValue)
                    {
                        if (lastValue != currentValue)
                        {
                            hasDifferentValues = true;
                            break;
                        }
                    }
                    lastValue = currentValue;
                    isFirstValue = false;
                }
            }
            attribute.IsCurve = hasDifferentValues;
            if (hasDifferentValues)
            {
                attribute.ValueCurve.keys = null;
                for (int j = 1; j < Math.Min(levels.Length + 1, values.Length); j++)
                {
                    float currentValue = 0.0f;
                    if (float.TryParse(values[j], out currentValue))
                    {
                        lastValue = currentValue;
                    }
                    Keyframe keyframe = new Keyframe();
                    keyframe.time = levels[j - 1];
                    keyframe.value = lastValue;
                    attribute.ValueCurve.AddKey(keyframe);
                    attribute.ValueCurve.MoveKey(attribute.ValueCurve.length - 1, keyframe);
                    AnimationUtility.SetKeyLeftTangentMode(attribute.ValueCurve, attribute.ValueCurve.length - 1, AnimationUtility.TangentMode.Linear);
                    AnimationUtility.SetKeyRightTangentMode(attribute.ValueCurve, attribute.ValueCurve.length - 1, AnimationUtility.TangentMode.Linear);
                }
            }
            else
            {
                attribute.Value = lastValue;
            }
        }

    }
}
