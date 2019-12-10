//using AdventuresUnknownSDK.Core.Objects.DropTables;
//using UnityEditor;
//using UnityEngine;

//namespace AdventuresUnknownSDK.Editor.Editors
//{
//    [CustomEditor(typeof(DropTable))]
//    public class DropTableEditor : UnityEditor.Editor
//    {
//        public override void OnInspectorGUI()
//        {
//            DropTable dropTable = target as DropTable;
//            GUI.enabled = false;
//            EditorGUILayout.ObjectField("Script", MonoScript.FromScriptableObject((DropTable)target), typeof(DropTable), false);
//            GUI.enabled = true;
//            SerializedProperty spIdentifier = serializedObject.FindProperty("m_Identifier");
//            EditorGUI.showMixedValue = spIdentifier.hasMultipleDifferentValues;
//            EditorGUI.BeginChangeCheck();
//            string identifier = EditorGUILayout.TextField("Identifier", ((DropTable)target).Identifier).ToLowerInvariant();
//            if (EditorGUI.EndChangeCheck())
//            {
//                for (int i = 0; i < targets.Length; i++)
//                {
//                    DropTable co = targets[i] as DropTable;
//                    co.Identifier = identifier;
//                }
//            }
//            EditorGUI.showMixedValue = false;
//            dropTable.DistinctRolls = EditorGUILayout.Toggle("DistinctRolls", dropTable.DistinctRolls);
//            SerializedProperty serDropChances = serializedObject.FindProperty("m_DropChances");
//            serializedObject.Update();

//            for(int i = 0; i < dropTable.DropChances.Count; i++)
//            {
//                dropTable.DropChances[i].UpdateIfNew();
//            }

//            EditorGUILayout.PropertyField(serDropChances, true);
//            serializedObject.ApplyModifiedProperties();
//            for (int i = 0; i < targets.Length; i++)
//            {
//                DropTable co = targets[i] as DropTable;
//                if (!co) continue;
//                if (co.ShouldRename)
//                    AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(targets[i]), ((DropTable)targets[i]).Identifier);
//                co.ShouldRename = false;
//            }
//        }
//    }
//}
