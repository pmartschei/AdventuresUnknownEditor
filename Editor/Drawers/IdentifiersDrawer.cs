using AdventuresUnknownSDK.Core.Objects;
using AdventuresUnknownSDK.Core.Objects.Items;
using AdventuresUnknownSDK.Core.Utils;
using AdventuresUnknownSDK.Core.Utils.Identifiers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace AdventuresUnknownSDK.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(ObjectIdentifier), true)]
    public class IdentifiersDrawer : PropertyDrawer
    {
        /// <summary> Cached style to use to draw the popup button. </summary>
        private GUIStyle popupStyle;
        #region Methods

        class FuncData
        {
            public SerializedProperty Property;
            public string Value;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (popupStyle == null)
            {
                popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
                popupStyle.imagePosition = ImagePosition.ImageOnly;
            }

            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            EditorGUI.BeginChangeCheck();

            //property.serializedObject.Update();

            SerializedProperty spIdentifier = property.FindPropertyRelative("m_Identifier");


            // Calculate rect for configuration button
            Rect buttonRect = new Rect(position);
            buttonRect.yMin += popupStyle.margin.top;
            buttonRect.width = popupStyle.fixedHeight + popupStyle.margin.right;
            position.xMin = buttonRect.xMax + popupStyle.margin.right;
            property.serializedObject.ApplyModifiedProperties();
            object obj = ObjectFinder.FindObject(this.fieldInfo, property);
            IEnumerable enumerable = obj as IEnumerable;
            ObjectIdentifier objectIdentifier = obj as ObjectIdentifier;

            if (enumerable != null)
            {
                int index = 0;
                int.TryParse(property.propertyPath.Last((c) => c != ']').ToString(), out index);
                foreach (var enumObj in enumerable)
                {
                    if (index == 0)
                    {
                        objectIdentifier = enumObj as ObjectIdentifier;
                        break;
                    }
                    index--;
                }
            }

            //ObjectIdentifier objectIdentifier = GetValue(GetParent(property), property.name) as ObjectIdentifier;

            // Store old indent level and set it to 0, the PrefixLabel takes care of it
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            //if (Event.current.commandName.Equals("ObjectSelectorUpdated"))
            //{
            //    UnityEngine.Object g = EditorGUIUtility.GetObjectPickerObject();
            //    Debug.Log(g);
            //}

            if (objectIdentifier != null)
            {
                List<CoreObject> coreObjects = new List<CoreObject>();
                Type[] types = objectIdentifier.GetSupportedTypes();
                if (types != null)
                {
                    foreach (Type type in types)
                    {
                        UnityEngine.Object[] foundObjects = Resources.FindObjectsOfTypeAll(type);
                        foreach (UnityEngine.Object foundObject in foundObjects)
                        {
                            CoreObject foundCoreObject = foundObject as CoreObject;
                            if (foundCoreObject)
                            {
                                coreObjects.Add(foundCoreObject);
                            }
                        }
                    }
                }
                coreObjects.Sort((first, second) => { return first.Identifier.CompareTo(second.Identifier); });
                string[] names = new string[coreObjects.Count];
                GenericMenu gm = new GenericMenu();
                for (int i = 0; i < coreObjects.Count; i++)
                {
                    names[i] = coreObjects[i].Identifier;
                    FuncData funcData = new FuncData();
                    funcData.Property = spIdentifier;
                    funcData.Value = names[i];
                    gm.AddItem(new GUIContent(names[i].Replace('.', '/')), false, OnItemChoosed, funcData);
                }
                if (GUI.Button(buttonRect, GUIContent.none, popupStyle))
                {
                    gm.DropDown(buttonRect);
                    
                    //EditorGUIUtility.ShowObjectPicker<CoreObject>(null, false, "", 0);
                }
                //spIdentifier.stringValue = EditorGUI.TextField(position, spIdentifier.stringValue);

                Event evt = Event.current; switch (evt.type)
                {
                    case EventType.DragUpdated:
                    case EventType.DragPerform:
                        if (!position.Contains(evt.mousePosition))
                            return;

                        if (DragAndDrop.objectReferences.Length != 1) return;
                        CoreObject coreObject = DragAndDrop.objectReferences[0] as CoreObject;
                        if (!coreObject) return;

                        DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                        if (evt.type == EventType.DragPerform)
                        {
                            DragAndDrop.AcceptDrag();

                            spIdentifier.stringValue = coreObject.Identifier;
                            spIdentifier.serializedObject.ApplyModifiedProperties();
                            property.serializedObject.ApplyModifiedProperties();
                        }
                        break;
                }
                EditorGUI.PropertyField(position, spIdentifier, GUIContent.none);
            }
            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
            }

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
        
        private void OnItemChoosed(object data)
        {
            FuncData funcData = data as FuncData;
            if (funcData == null) return;
            funcData.Property.stringValue = funcData.Value;
            funcData.Property.serializedObject.ApplyModifiedProperties();
        }
        #endregion
    }
}
