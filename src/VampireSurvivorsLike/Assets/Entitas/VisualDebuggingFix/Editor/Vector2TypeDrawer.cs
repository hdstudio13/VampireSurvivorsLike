using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebuggingFix.Editor
{
    public class Vector2TypeDrawer : ITypeDrawer
    {
        public bool HandlesType(System.Type type) => type == typeof (Vector2);

        public object DrawAndGetNewValue(
            System.Type memberType,
            string memberName,
            object value,
            object target)
        {
            return (object) EditorGUILayout.Vector2Field(memberName, (Vector2) value);
        }
    }
}