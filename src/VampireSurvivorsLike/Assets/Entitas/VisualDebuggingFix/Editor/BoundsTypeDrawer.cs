using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebuggingFix.Editor
{
    public class BoundsTypeDrawer : ITypeDrawer
    {
        public bool HandlesType(System.Type type) => type == typeof (Bounds);

        public object DrawAndGetNewValue(
            System.Type memberType,
            string memberName,
            object value,
            object target)
        {
            return (object) EditorGUILayout.BoundsField(memberName, (Bounds) value);
        }
    }
}