using AdventuresUnknownSDK.Core.Objects.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace AdventuresUnknownSDK.Core.Entities
{
    [CustomEditor(typeof(Wallet))]
    public class WalletEditor : UnityEditor.Editor
    {


        #region Properties

        #endregion

        #region Methods
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Wallet wallet = target as Wallet;

            SerializedProperty foldOut = serializedObject.FindProperty("m_IsFoldout");
            List<KeyValuePair<string, long>> currencyList = wallet.CurrencyList;
            foldOut.isExpanded = EditorGUILayout.Foldout(foldOut.isExpanded, currencyList.Count + " Currencies",true);
            if (foldOut.isExpanded)
            {
                EditorGUI.indentLevel++;


                //sort

                foreach(KeyValuePair<string,long> currencyEntry in currencyList)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(currencyEntry.Key);
                    EditorGUILayout.LabelField(currencyEntry.Value.ToString());
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUI.indentLevel--;
            }
            EditorUtility.SetDirty(wallet);
        }
        #endregion
    }
}
