using UnityEditor;

namespace Entitas.VisualDebuggingFix.Editor
{
    public class DoubleTypeDrawer : ITypeDrawer
    {
        public bool HandlesType(System.Type type) => type == typeof (double);

        public object DrawAndGetNewValue(
            System.Type memberType,
            string memberName,
            object value,
            object target)
        {
            return (object) EditorGUILayout.DoubleField(memberName, (double) value);
        }
    }
}