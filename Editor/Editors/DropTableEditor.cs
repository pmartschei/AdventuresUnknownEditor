using AdventuresUnknownSDK.Core.Objects.DropTables;
using UnityEditor;
using UnityEngine;

namespace AdventuresUnknownSDK.Editor.Editors
{
    [CustomEditor(typeof(DropTable))]
    public class DropTableEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DropTable dropTable = target as DropTable;
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script", MonoScript.FromScriptableObject((DropTable)target), typeof(DropTable), false);
            GUI.enabled = true;
            dropTable.Identifier = EditorGUILayout.TextField("Identifier",dropTable.Identifier);
            dropTable.DistinctRolls = EditorGUILayout.Toggle("DistinctRolls", dropTable.DistinctRolls);
            SerializedProperty serDropChances = serializedObject.FindProperty("m_DropChances");
            serializedObject.Update();

            for(int i = 0; i < dropTable.DropChances.Count; i++)
            {
                dropTable.DropChances[i].UpdateIfNew();
            }

            EditorGUILayout.PropertyField(serDropChances, true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
