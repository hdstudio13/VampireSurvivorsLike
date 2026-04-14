using UnityEditor;

namespace Entitas.VisualDebuggingFix.Editor
{
    public class CharTypeDrawer : ITypeDrawer
    {
        public bool HandlesType(System.Type type) => type == typeof (char);

        public object DrawAndGetNewValue(
            System.Type memberType,
            string memberName,
            object value,
            object target)
        {
            string str = EditorGUILayout.TextField(memberName, ((char) value).ToString());
            return (object) (char) (str.Length > 0 ? (int) str[0] : 0);
        }
    }
}