using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebuggingFix.Editor
{
    public class AnimationCurveTypeDrawer : ITypeDrawer
    {
        public bool HandlesType(System.Type type) => type == typeof (AnimationCurve);

        public object DrawAndGetNewValue(
            System.Type memberType,
            string memberName,
            object value,
            object target)
        {
            return (object) EditorGUILayout.CurveField(memberName, (AnimationCurve) value);
        }
    }
}