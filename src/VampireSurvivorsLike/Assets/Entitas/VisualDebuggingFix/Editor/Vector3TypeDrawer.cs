using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebuggingFix.Editor
{
    public class Vector3TypeDrawer : ITypeDrawer
    {
        public bool HandlesType(System.Type type) => type == typeof (Vector3);

        public object DrawAndGetNewValue(
            System.Type memberType,
            string memberName,
            object value,
            object target)
        {
            return (object) EditorGUILayout.Vector3Field(memberName, (Vector3) value);
        }
    }
}