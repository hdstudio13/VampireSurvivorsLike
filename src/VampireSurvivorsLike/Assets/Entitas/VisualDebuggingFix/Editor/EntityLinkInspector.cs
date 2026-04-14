using System;
using System.Collections.Generic;
using System.Linq;
using Entitas.Unity;
using Entitas.VisualDebugging.Unity;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebuggingFix.Editor
{
    [CustomEditor(typeof (EntityLink))]
    public class EntityLinkInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EntityLink link = (EntityLink) this.target;
            if (link.entity != null && GUILayout.Button("Unlink"))
                link.Unlink();
            if (link.entity != null)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField(link.entity.ToString());
                if (GUILayout.Button("Show entity"))
                    Selection.activeGameObject = ((IEnumerable<EntityBehaviour>) UnityEngine.Object.FindObjectsOfType<EntityBehaviour>()).Single<EntityBehaviour>((Func<EntityBehaviour, bool>) (e => e.entity == link.entity)).gameObject;
                EditorGUILayout.Space();
                EntityDrawer.DrawEntity(link.entity);
            }
            else
                EditorGUILayout.LabelField("Not linked to an entity");
        }
    }
}