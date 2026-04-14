using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebuggingFix.Editor
{
    public class Vector4TypeDrawer : ITypeDrawer
    {
        public bool HandlesType(System.Type type) => type == typeof (Vector4);

        public object DrawAndGetNewValue(
            System.Type memberType,
            string memberName,
            object value,
            object target)
        {
            return (object) EditorGUILayout.Vector4Field(memberName, (Vector4) value);
        }
    }
}