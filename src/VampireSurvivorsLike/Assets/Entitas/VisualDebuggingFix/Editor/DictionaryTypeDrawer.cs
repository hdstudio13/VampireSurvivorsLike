using System;
using System.Collections;
using System.Collections.Generic;
using DesperateDevs.Extensions;
using DesperateDevs.Unity.Editor;
using UnityEditor;

namespace Entitas.VisualDebuggingFix.Editor
{
    public class DictionaryTypeDrawer : ITypeDrawer
    {
        private static readonly Dictionary<System.Type, string> _keySearchTexts = new Dictionary<System.Type, string>();

        public bool HandlesType(System.Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Dictionary<,>);
        }

        public object DrawAndGetNewValue(
            System.Type memberType,
            string memberName,
            object value,
            object target)
        {
            IDictionary dictionary = (IDictionary) value;
            System.Type genericArgument1 = memberType.GetGenericArguments()[0];
            System.Type genericArgument2 = memberType.GetGenericArguments()[1];
            System.Type type = target.GetType();
            if (!DictionaryTypeDrawer._keySearchTexts.ContainsKey(type))
                DictionaryTypeDrawer._keySearchTexts.Add(type, string.Empty);
            EditorGUILayout.BeginHorizontal();
            if (dictionary.Count == 0)
            {
                EditorGUILayout.LabelField(memberName, "empty");
                DictionaryTypeDrawer._keySearchTexts[type] = string.Empty;
            }
            else
                EditorGUILayout.LabelField(memberName);
            object defaultValue1;
            object defaultValue2;
            if (EditorLayout.MiniButton($"new <{genericArgument1.ToCompilableString().ShortTypeName()}, {genericArgument2.ToCompilableString().ShortTypeName()}>") && EntityDrawer.CreateDefault(genericArgument1, out defaultValue1) && EntityDrawer.CreateDefault(genericArgument2, out defaultValue2))
                dictionary[defaultValue1] = defaultValue2;
            EditorGUILayout.EndHorizontal();
            if (dictionary.Count > 0)
            {
                int num = EditorGUI.indentLevel++;
                if (dictionary.Count > 5)
                {
                    EditorGUILayout.Space();
                    DictionaryTypeDrawer._keySearchTexts[type] = EditorLayout.SearchTextField(DictionaryTypeDrawer._keySearchTexts[type]);
                }
                EditorGUILayout.Space();
                ArrayList arrayList = new ArrayList(dictionary.Keys);
                for (int index = 0; index < arrayList.Count; ++index)
                {
                    object key = arrayList[index];
                    if (EditorLayout.MatchesSearchString(key.ToString().ToLower(), DictionaryTypeDrawer._keySearchTexts[type].ToLower()))
                    {
                        EntityDrawer.DrawObjectMember(genericArgument1, "key", key, target, (Action<object, object>) ((newComponent, newValue) =>
                        {
                            object obj = dictionary[key];
                            dictionary.Remove(key);
                            if (newValue == null)
                                return;
                            dictionary[newValue] = obj;
                        }));
                        EntityDrawer.DrawObjectMember(genericArgument2, nameof (value), dictionary[key], target, (Action<object, object>) ((newComponent, newValue) => dictionary[key] = newValue));
                        EditorGUILayout.Space();
                    }
                }
                EditorGUI.indentLevel = num;
            }
            return (object) dictionary;
        }
    }
}