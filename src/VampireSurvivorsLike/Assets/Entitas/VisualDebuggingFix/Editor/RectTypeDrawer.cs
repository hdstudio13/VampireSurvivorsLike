using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebuggingFix.Editor
{
    public class RectTypeDrawer : ITypeDrawer
    {
        public bool HandlesType(System.Type type) => type == typeof (Rect);

        public object DrawAndGetNewValue(
            System.Type memberType,
            string memberName,
            object value,
            object target)
        {
            return (object) EditorGUILayout.RectField(memberName, (Rect) value);
        }
    }
}