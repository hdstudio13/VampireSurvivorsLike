using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebuggingFix.Editor
{
    public class ColorTypeDrawer : ITypeDrawer
    {
        public bool HandlesType(System.Type type) => type == typeof (Color);

        public object DrawAndGetNewValue(
            System.Type memberType,
            string memberName,
            object value,
            object target)
        {
            return (object) EditorGUILayout.ColorField(memberName, (Color) value);
        }
    }
}