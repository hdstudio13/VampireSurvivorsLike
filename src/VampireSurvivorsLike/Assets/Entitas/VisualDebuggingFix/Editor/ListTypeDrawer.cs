using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DesperateDevs.Extensions;
using DesperateDevs.Unity.Editor;
using UnityEditor;

namespace Entitas.VisualDebuggingFix.Editor
{
    public class ListTypeDrawer : ITypeDrawer
    {
        public bool HandlesType(System.Type type)
        {
            return ((IEnumerable<System.Type>) type.GetInterfaces()).Contains<System.Type>(typeof (IList));
        }

        public object DrawAndGetNewValue(
            System.Type memberType,
            string memberName,
            object value,
            object target)
        {
            IList list = (IList) value;
            System.Type genericArgument = memberType.GetGenericArguments()[0];
            if (list.Count == 0)
                list = this.drawAddElement(list, memberName, genericArgument);
            else
                EditorGUILayout.LabelField(memberName);
            int num = EditorGUI.indentLevel++;
            Func<IList> func1 = (Func<IList>) null;
            for (int index = 0; index < list.Count; ++index)
            {
                int localIndex = index;
                EditorGUILayout.BeginHorizontal();
                EntityDrawer.DrawObjectMember(genericArgument, $"{memberName}[{localIndex}]", list[localIndex], target, (Action<object, object>) ((newComponent, newValue) => list[localIndex] = newValue));
                Func<IList> func2 = ListTypeDrawer.drawEditActions(list, genericArgument, localIndex);
                if (func2 != null)
                    func1 = func2;
                EditorGUILayout.EndHorizontal();
            }
            if (func1 != null)
                list = func1();
            EditorGUI.indentLevel = num;
            return (object) list;
        }

        private static Func<IList> drawEditActions(IList list, System.Type elementType, int index)
        {
            if (EditorLayout.MiniButtonLeft("↑") && index > 0)
                return (Func<IList>) (() =>
                {
                    int index1 = index - 1;
                    object obj = list[index1];
                    list[index1] = list[index];
                    list[index] = obj;
                    return list;
                });
            if (EditorLayout.MiniButtonMid("↓") && index < list.Count - 1)
                return (Func<IList>) (() =>
                {
                    int index2 = index + 1;
                    object obj = list[index2];
                    list[index2] = list[index];
                    list[index] = obj;
                    return list;
                });
            if (EditorLayout.MiniButtonMid("+"))
            {
                object defaultValue;
                if (EntityDrawer.CreateDefault(elementType, out defaultValue))
                {
                    int insertAt = index + 1;
                    return (Func<IList>) (() =>
                    {
                        list.Insert(insertAt, defaultValue);
                        return list;
                    });
                }
            }
            if (!EditorLayout.MiniButtonRight("-"))
                return (Func<IList>) null;
            int removeAt = index;
            return (Func<IList>) (() =>
            {
                list.RemoveAt(removeAt);
                return list;
            });
        }

        private IList drawAddElement(IList list, string memberName, System.Type elementType)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(memberName, "empty");
            object defaultValue;
            if (EditorLayout.MiniButton("add " + elementType.ToCompilableString().ShortTypeName()) && EntityDrawer.CreateDefault(elementType, out defaultValue))
                list.Add(defaultValue);
            EditorGUILayout.EndHorizontal();
            return list;
        }
    }
}