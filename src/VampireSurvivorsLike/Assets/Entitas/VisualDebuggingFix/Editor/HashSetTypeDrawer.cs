using System;
using System.Collections;
using System.Collections.Generic;
using DesperateDevs.Extensions;
using DesperateDevs.Unity.Editor;
using UnityEditor;

namespace Entitas.VisualDebuggingFix.Editor
{
    public class HashSetTypeDrawer : ITypeDrawer
    {
        public bool HandlesType(System.Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof (HashSet<>);
        }

        public object DrawAndGetNewValue(
            System.Type memberType,
            string memberName,
            object value,
            object target)
        {
            System.Type genericArgument = memberType.GetGenericArguments()[0];
            ArrayList itemsToRemove = new ArrayList();
            ArrayList itemsToAdd = new ArrayList();
            int num1 = !((IEnumerable) value).GetEnumerator().MoveNext() ? 1 : 0;
            EditorGUILayout.BeginHorizontal();
            if (num1 != 0)
                EditorGUILayout.LabelField(memberName, "empty");
            else
                EditorGUILayout.LabelField(memberName);
            object defaultValue;
            if (EditorLayout.MiniButton("new " + genericArgument.ToCompilableString().ShortTypeName()) && EntityDrawer.CreateDefault(genericArgument, out defaultValue))
                itemsToAdd.Add(defaultValue);
            EditorGUILayout.EndHorizontal();
            if (num1 == 0)
            {
                EditorGUILayout.Space();
                int num2 = EditorGUI.indentLevel++;
                foreach (object obj in (IEnumerable) value)
                {
                    object item = obj;
                    EditorGUILayout.BeginHorizontal();
                    EntityDrawer.DrawObjectMember(genericArgument, string.Empty, item, target, (Action<object, object>) ((newComponent, newValue) =>
                    {
                        itemsToRemove.Add(item);
                        itemsToAdd.Add(newValue);
                    }));
                    if (EditorLayout.MiniButton("-"))
                        itemsToRemove.Add(item);
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUI.indentLevel = num2;
            }
            foreach (object obj in itemsToRemove)
                memberType.GetMethod("Remove").Invoke(value, new object[1]
                {
                    obj
                });
            foreach (object obj in itemsToAdd)
                memberType.GetMethod("Add").Invoke(value, new object[1]
                {
                    obj
                });
            return value;
        }
    }
}