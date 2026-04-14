using System;
using UnityEditor;

namespace Entitas.VisualDebuggingFix.Editor
{
    public class DateTimeTypeDrawer : ITypeDrawer
    {
        public bool HandlesType(System.Type type) => type == typeof (DateTime);

        public object DrawAndGetNewValue(
            System.Type memberType,
            string memberName,
            object value,
            object target)
        {
            string text = value.ToString();
            string s = EditorGUILayout.TextField(memberName, text);
            return !(s != text) ? value : (object) DateTime.Parse(s);
        }
    }
}