using System;
using System.Collections.Generic;
using System.Linq;
using DesperateDevs.Unity.Editor;
using Entitas.VisualDebugging.Unity;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebuggingFix.Editor
{
    [CustomEditor(typeof (ContextObserverBehaviour))]
    public class ContextObserverInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            ContextObserver contextObserver = ((ContextObserverBehaviour) this.target).contextObserver;
            EditorLayout.BeginVerticalBox();
            EditorGUILayout.LabelField(contextObserver.context.contextInfo.name, EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Entities", contextObserver.context.count.ToString());
            EditorGUILayout.LabelField("Reusable entities", contextObserver.context.reusableEntitiesCount.ToString());
            int retainedEntitiesCount = contextObserver.context.retainedEntitiesCount;
            if (retainedEntitiesCount != 0)
            {
                Color color = GUI.color;
                GUI.color = Color.red;
                EditorGUILayout.LabelField("Retained entities", retainedEntitiesCount.ToString());
                GUI.color = color;
                EditorGUILayout.HelpBox("WARNING: There are retained entities.\nDid you call entity.Retain(owner) and forgot to call entity.Release(owner)?", MessageType.Warning);
            }
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Create Entity"))
            {
                IEntity entity = contextObserver.context.CreateEntity2();
                Selection.activeGameObject = ((IEnumerable<EntityBehaviour>) UnityEngine.Object.FindObjectsOfType<EntityBehaviour>()).Single<EntityBehaviour>((Func<EntityBehaviour, bool>) (eb => eb.entity == entity)).gameObject;
            }
            Color backgroundColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Destroy All Entities"))
                contextObserver.context.DestroyAllEntities();
            GUI.backgroundColor = backgroundColor;
            EditorGUILayout.EndHorizontal();
            EditorLayout.EndVerticalBox();
            IGroup[] groups = contextObserver.groups;
            if (groups.Length != 0)
            {
                EditorLayout.BeginVerticalBox();
                EditorGUILayout.LabelField($"Groups ({groups.Length})", EditorStyles.boldLabel);
                foreach (IGroup group in (IEnumerable<IGroup>) ((IEnumerable<IGroup>) groups).OrderByDescending<IGroup, int>((Func<IGroup, int>) (g => g.count)))
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(group.ToString());
                    EditorGUILayout.LabelField(group.count.ToString(), GUILayout.Width(48f));
                    EditorGUILayout.EndHorizontal();
                }
                EditorLayout.EndVerticalBox();
            }
            EditorUtility.SetDirty(this.target);
        }
    }
}