using UnityEditor;

namespace Entitas.VisualDebuggingFix.Editor
{
    public class BoolTypeDrawer : ITypeDrawer
    {
        public bool HandlesType(System.Type type) => type == typeof (bool);

        public object DrawAndGetNewValue(
            System.Type memberType,
            string memberName,
            object value,
            object target)
        {
            return (object) EditorGUILayout.Toggle(memberName, (bool) value);
        }
    }
}