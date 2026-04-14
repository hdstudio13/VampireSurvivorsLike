using System;
using System.Collections;
using DesperateDevs.Extensions;
using DesperateDevs.Unity.Editor;
using UnityEditor;

namespace Entitas.VisualDebuggingFix.Editor
{
    public class ArrayTypeDrawer : ITypeDrawer
    {
        public bool HandlesType(System.Type type) => type.IsArray;

        public object DrawAndGetNewValue(
            System.Type memberType,
            string memberName,
            object value,
            object target)
        {
            Array array = (Array) value;
            System.Type elementType = memberType.GetElementType();
            int indentLevel = EditorGUI.indentLevel;
            if (array.Rank == 1)
                array = this.drawRank1(array, memberName, elementType, indentLevel, target);
            else if (array.Rank == 2)
                array = this.drawRank2(array, memberName, elementType, target);
            else if (array.Rank == 3)
                array = this.drawRank3(array, memberName, elementType, target);
            EditorGUI.indentLevel = indentLevel;
            return (object) array;
        }

        private Array drawRank1(
            Array array,
            string memberName,
            System.Type elementType,
            int indent,
            object target)
        {
            int length = array.GetLength(0);
            if (length == 0)
                array = this.drawAddElement(array, memberName, elementType);
            else
                EditorGUILayout.LabelField(memberName);
            EditorGUI.indentLevel = indent + 1;
            Func<Array> func1 = (Func<Array>) null;
            for (int index = 0; index < length; ++index)
            {
                int localIndex = index;
                EditorGUILayout.BeginHorizontal();
                EntityDrawer.DrawObjectMember(elementType, $"{memberName}[{localIndex}]", array.GetValue(localIndex), target, (Action<object, object>) ((newComponent, newValue) => array.SetValue(newValue, localIndex)));
                Func<Array> func2 = ArrayTypeDrawer.drawEditActions(array, elementType, localIndex);
                if (func2 != null)
                    func1 = func2;
                EditorGUILayout.EndHorizontal();
            }
            if (func1 != null)
                array = func1();
            return array;
        }

        private Array drawAddElement(Array array, string memberName, System.Type elementType)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(memberName, "empty");
            object defaultValue;
            if (EditorLayout.MiniButton("add " + elementType.ToCompilableString().ShortTypeName()) && EntityDrawer.CreateDefault(elementType, out defaultValue))
            {
                Array instance = Array.CreateInstance(elementType, 1);
                instance.SetValue(defaultValue, 0);
                array = instance;
            }
            EditorGUILayout.EndHorizontal();
            return array;
        }

        private Array drawRank2(Array array, string memberName, System.Type elementType, object target)
        {
            EditorGUILayout.LabelField(memberName);
            for (int index1 = 0; index1 < array.GetLength(0); ++index1)
            {
                int localIndex1 = index1;
                for (int index2 = 0; index2 < array.GetLength(1); ++index2)
                {
                    int localIndex2 = index2;
                    EntityDrawer.DrawObjectMember(elementType, $"{memberName}[{localIndex1}, {localIndex2}]", array.GetValue(localIndex1, localIndex2), target, (Action<object, object>) ((newComponent, newValue) => array.SetValue(newValue, localIndex1, localIndex2)));
                }
                EditorGUILayout.Space();
            }
            return array;
        }

        private Array drawRank3(Array array, string memberName, System.Type elementType, object target)
        {
            EditorGUILayout.LabelField(memberName);
            for (int index1 = 0; index1 < array.GetLength(0); ++index1)
            {
                int localIndex1 = index1;
                for (int index2 = 0; index2 < array.GetLength(1); ++index2)
                {
                    int localIndex2 = index2;
                    for (int index3 = 0; index3 < array.GetLength(2); ++index3)
                    {
                        int localIndex3 = index3;
                        EntityDrawer.DrawObjectMember(elementType, $"{memberName}[{localIndex1}, {localIndex2}, {localIndex3}]", array.GetValue(localIndex1, localIndex2, localIndex3), target, (Action<object, object>) ((newComponent, newValue) => array.SetValue(newValue, localIndex1, localIndex2, localIndex3)));
                    }
                    EditorGUILayout.Space();
                }
                EditorGUILayout.Space();
            }
            return array;
        }

        private static Func<Array> drawEditActions(Array array, System.Type elementType, int index)
        {
            if (EditorLayout.MiniButtonLeft("↑") && index > 0)
                return (Func<Array>) (() =>
                {
                    int index1 = index - 1;
                    object obj = array.GetValue(index1);
                    array.SetValue(array.GetValue(index), index1);
                    array.SetValue(obj, index);
                    return array;
                });
            if (EditorLayout.MiniButtonMid("↓") && index < array.Length - 1)
                return (Func<Array>) (() =>
                {
                    int index2 = index + 1;
                    object obj = array.GetValue(index2);
                    array.SetValue(array.GetValue(index), index2);
                    array.SetValue(obj, index);
                    return array;
                });
            if (EditorLayout.MiniButtonMid("+"))
            {
                object defaultValue;
                if (EntityDrawer.CreateDefault(elementType, out defaultValue))
                    return (Func<Array>) (() => ArrayTypeDrawer.arrayInsertAt(array, elementType, defaultValue, index + 1));
            }
            return EditorLayout.MiniButtonRight("-") ? (Func<Array>) (() => ArrayTypeDrawer.arrayRemoveAt(array, elementType, index)) : (Func<Array>) null;
        }

        private static Array arrayRemoveAt(Array array, System.Type elementType, int removeAt)
        {
            ArrayList arrayList = new ArrayList((ICollection) array);
            arrayList.RemoveAt(removeAt);
            return arrayList.ToArray(elementType);
        }

        private static Array arrayInsertAt(Array array, System.Type elementType, object value, int insertAt)
        {
            ArrayList arrayList = new ArrayList((ICollection) array);
            arrayList.Insert(insertAt, value);
            return arrayList.ToArray(elementType);
        }
    }
}