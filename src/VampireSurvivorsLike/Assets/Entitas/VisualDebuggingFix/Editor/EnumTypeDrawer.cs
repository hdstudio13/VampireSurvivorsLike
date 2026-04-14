using System;
using UnityEditor;

namespace Entitas.VisualDebuggingFix.Editor
{
    public class EnumTypeDrawer : ITypeDrawer
    {
        public bool HandlesType(System.Type type) => type.IsEnum;

        public object DrawAndGetNewValue(
            System.Type memberType,
            string memberName,
            object value,
            object target)
        {
            return !memberType.IsDefined(typeof (FlagsAttribute), false) ? (object) EditorGUILayout.EnumPopup(memberName, (Enum) value) : (object) EditorGUILayout.EnumFlagsField(memberName, (Enum) value);
        }
    }
}