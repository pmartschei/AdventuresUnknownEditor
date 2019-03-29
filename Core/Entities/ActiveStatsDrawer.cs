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
    [CustomPropertyDrawer(typeof(ActiveStats))]
    public class ActiveStatsDrawer : PropertyDrawer
    {
        #region Methods

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ActiveStats targetActiveStats = ObjectFinder.FindObject(this.fieldInfo, property) as ActiveStats;
            if (!property.isExpanded)
                return EditorGUIUtility.singleLineHeight;
            return EditorGUIUtility.singleLineHeight * (targetActiveStats.Stats.Length + 1);
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ActiveStats targetActiveStats = ObjectFinder.FindObject(this.fieldInfo, property) as ActiveStats;
            if (targetActiveStats == null) return;
            if (property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, true))
            {
                EditorGUI.indentLevel++;
                foreach (Stat stat in targetActiveStats.Stats)
                {
                    position.y += EditorGUIUtility.singleLineHeight;
                    EditorGUI.LabelField(position, stat.ModTypeIdentifier + " = "+ stat.Current +" / " + stat.Calculated);
                }
                EditorGUI.indentLevel--;
            }
            //base.OnGUI(position, property, label);
        }
        #endregion
    }
}
