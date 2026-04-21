using System;
using System.Collections.Generic;
using System.Linq;
using Entitas.VisualDebugging.Unity;
using UnityEditor;

namespace Entitas.VisualDebuggingFix.Editor
{
    [CustomEditor(typeof(EntityBehaviour))]
    [CanEditMultipleObjects]
    public class EntityInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            if (this.targets.Length == 1)
                EntityDrawer.DrawEntity(((EntityBehaviour) this.target).entity);
            else
                EntityDrawer.DrawMultipleEntities(((IEnumerable<UnityEngine.Object>) this.targets).Select<UnityEngine.Object, IEntity>((Func<UnityEngine.Object, IEntity>) (t => ((EntityBehaviour) t).entity)).ToArray<IEntity>());
            if (!(this.target != (UnityEngine.Object) null))
                return;
            EditorUtility.SetDirty(this.target);
        }
    }
}