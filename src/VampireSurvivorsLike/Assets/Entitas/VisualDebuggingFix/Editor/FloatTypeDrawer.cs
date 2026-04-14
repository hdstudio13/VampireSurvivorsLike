using UnityEditor;

namespace Entitas.VisualDebuggingFix.Editor
{
    public class FloatTypeDrawer : ITypeDrawer
    {
        public bool HandlesType(System.Type type) => type == typeof (float);

        public object DrawAndGetNewValue(
            System.Type memberType,
            string memberName,
            object value,
            object target)
        {
            return (object) EditorGUILayout.FloatField(memberName, (float) value);
        }
    }
}