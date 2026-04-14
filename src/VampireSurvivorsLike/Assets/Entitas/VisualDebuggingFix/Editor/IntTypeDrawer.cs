using UnityEditor;

namespace Entitas.VisualDebuggingFix.Editor
{
  public class IntTypeDrawer : ITypeDrawer
    {
        public bool HandlesType(System.Type type) => type == typeof (int);

        public object DrawAndGetNewValue(
            System.Type memberType,
            string memberName,
            object value,
            object target)
        {
            return (object) EditorGUILayout.IntField(memberName, (int) value);
        }
    }
}