using UnityEditor;

namespace Entitas.VisualDebuggingFix.Editor
{
    public class StringTypeDrawer : ITypeDrawer
    {
        public bool HandlesType(System.Type type) => type == typeof (string);

        public object DrawAndGetNewValue(
            System.Type memberType,
            string memberName,
            object value,
            object target)
        {
            return (object) EditorGUILayout.DelayedTextField(memberName, (string) value);
        }
    }
}