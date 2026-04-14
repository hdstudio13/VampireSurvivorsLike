using UnityEditor;

namespace Entitas.VisualDebuggingFix.Editor
{
    public class UnityObjectTypeDrawer : ITypeDrawer
    {
        public bool HandlesType(System.Type type)
        {
            return type == typeof (UnityEngine.Object) || type.IsSubclassOf(typeof (UnityEngine.Object));
        }

        public object DrawAndGetNewValue(
            System.Type memberType,
            string memberName,
            object value,
            object target)
        {
            return (object) EditorGUILayout.ObjectField(memberName, (UnityEngine.Object) value, memberType, true);
        }
    }
}