using AdventuresUnknownSDK.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace AdventuresUnknownSDK.Core.Entities
{
    [CustomPropertyDrawer(typeof(Entity))]
    public class EntityDrawer : UnityEditor.PropertyDrawer
    {
        #region Methods

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            Entity targetActiveStats = ObjectFinder.FindObject(this.fieldInfo, property) as Entity;
            if (!property.isExpanded)
                return EditorGUIUtility.singleLineHeight;
            SerializedProperty prop = property.FindPropertyRelative("m_Description");
            float height = EditorGUI.GetPropertyHeight(prop, true);
            int statCount = 0;
            foreach (Stat stat in targetActiveStats.Stats)
            {
                if (stat.IsDefault) continue;
                statCount++;
            }
            return EditorGUIUtility.singleLineHeight * (statCount + 2) + height;
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Entity targetActiveStats = ObjectFinder.FindObject(this.fieldInfo, property) as Entity;
            position.height = EditorGUIUtility.singleLineHeight;
            if (targetActiveStats == null) return;
            if (property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, true))
            {
                EditorGUI.indentLevel++;
                Stat[] stats = targetActiveStats.Stats;
                Array.Sort(stats, (s1, s2) =>{ return s1.ModTypeIdentifier.CompareTo(s2.ModTypeIdentifier); });
                foreach (Stat stat in stats)
                {
                    if (stat.IsDefault) continue;
                    position.y += EditorGUIUtility.singleLineHeight;
                    string text = string.Format("{0} = {1} / {2} ({3}|{4}|{5}|{6})", stat.ModTypeIdentifier, stat.Current, stat.Calculated, stat.Flat, stat.Increased, stat.More, stat.FlatExtra);
                    EditorGUI.LabelField(position, text );
                }
                position.y += EditorGUIUtility.singleLineHeight;
                targetActiveStats.CanTick = EditorGUI.Toggle(position, "CanTick?",targetActiveStats.CanTick);
                position.y += EditorGUIUtility.singleLineHeight;
                SerializedProperty prop = property.FindPropertyRelative("m_Description");
                position.height = EditorGUI.GetPropertyHeight(prop,true);
                EditorGUI.PropertyField(position, prop,true);
                EditorGUI.indentLevel--;
            }
            property.serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(property.serializedObject.targetObject);
            //base.OnGUI(position, property, label);
        }

        #endregion
    }
}
