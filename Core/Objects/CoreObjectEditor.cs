using AdventuresUnknownSDK.Core.Objects;
using UnityEditor;
using UnityEngine;

namespace AdventuresUnknownSDK.Core.Objects
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(CoreObject), true)]
    public class CoreObjectEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            string assetPath = AssetDatabase.GetAssetPath(serializedObject.targetObject);
            CoreObject coreObject = target as CoreObject;
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script", MonoScript.FromScriptableObject((CoreObject)target), typeof(CoreObject), false);
            GUI.enabled = coreObject.IsIdentifierEditableInEditor();
            SerializedProperty spIdentifier = serializedObject.FindProperty("m_Identifier");
            EditorGUI.showMixedValue = spIdentifier.hasMultipleDifferentValues;
            EditorGUI.BeginChangeCheck();
            string identifier = EditorGUILayout.TextField("Identifier", ((CoreObject)target).Identifier).ToLowerInvariant();
            if (EditorGUI.EndChangeCheck())
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    CoreObject co = targets[i] as CoreObject;
                    co.Identifier = identifier;
                }
            }
            EditorGUI.showMixedValue = false;
            GUI.enabled = true;
            DrawPropertiesExcluding(serializedObject, "m_Identifier", "m_Script");
            serializedObject.ApplyModifiedProperties();
            for (int i = 0; i < targets.Length; i++)
            {
                CoreObject co = targets[i] as CoreObject;
                if (!co) continue;
                if (co.ShouldRename)
                    AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(targets[i]), ((CoreObject)targets[i]).Identifier);
                co.ShouldRename = false;
            }
        }
    }

}
