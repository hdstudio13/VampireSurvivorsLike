using System;
using DesperateDevs.Serialization;
using DesperateDevs.Unity.Editor;
using Entitas.Unity;
using Entitas.VisualDebugging.Unity;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebuggingFix.Editor
{
    [InitializeOnLoad]
    public static class EntitasHierarchyIcon
    {
        private static Texture2D _contextHierarchyIcon;
        private static Texture2D _contextErrorHierarchyIcon;
        private static Texture2D _entityHierarchyIcon;
        private static Texture2D _entityErrorHierarchyIcon;
        private static Texture2D _entityLinkHierarchyIcon;
        private static Texture2D _entityLinkWarnHierarchyIcon;
        private static Texture2D _systemsHierarchyIcon;
        private static Texture2D _systemsWarnHierarchyIcon;
        private static readonly int _systemWarningThreshold;

        private static Texture2D contextHierarchyIcon
        {
            get
            {
                if ((UnityEngine.Object) EntitasHierarchyIcon._contextHierarchyIcon == (UnityEngine.Object) null)
                    EntitasHierarchyIcon._contextHierarchyIcon = EditorLayout.LoadTexture("l:EntitasContextHierarchyIcon");
                return EntitasHierarchyIcon._contextHierarchyIcon;
            }
        }

        private static Texture2D contextErrorHierarchyIcon
        {
            get
            {
                if ((UnityEngine.Object) EntitasHierarchyIcon._contextErrorHierarchyIcon == (UnityEngine.Object) null)
                    EntitasHierarchyIcon._contextErrorHierarchyIcon = EditorLayout.LoadTexture("l:EntitasContextErrorHierarchyIcon");
                return EntitasHierarchyIcon._contextErrorHierarchyIcon;
            }
        }

        private static Texture2D entityHierarchyIcon
        {
            get
            {
                if ((UnityEngine.Object) EntitasHierarchyIcon._entityHierarchyIcon == (UnityEngine.Object) null)
                    EntitasHierarchyIcon._entityHierarchyIcon = EditorLayout.LoadTexture("l:EntitasEntityHierarchyIcon");
                return EntitasHierarchyIcon._entityHierarchyIcon;
            }
        }

        private static Texture2D entityErrorHierarchyIcon
        {
            get
            {
                if ((UnityEngine.Object) EntitasHierarchyIcon._entityErrorHierarchyIcon == (UnityEngine.Object) null)
                    EntitasHierarchyIcon._entityErrorHierarchyIcon = EditorLayout.LoadTexture("l:EntitasEntityErrorHierarchyIcon");
                return EntitasHierarchyIcon._entityErrorHierarchyIcon;
            }
        }

        private static Texture2D entityLinkHierarchyIcon
        {
            get
            {
                if ((UnityEngine.Object) EntitasHierarchyIcon._entityLinkHierarchyIcon == (UnityEngine.Object) null)
                    EntitasHierarchyIcon._entityLinkHierarchyIcon = EditorLayout.LoadTexture("l:EntitasEntityLinkHierarchyIcon");
                return EntitasHierarchyIcon._entityLinkHierarchyIcon;
            }
        }

        private static Texture2D entityLinkWarnHierarchyIcon
        {
            get
            {
                if ((UnityEngine.Object) EntitasHierarchyIcon._entityLinkWarnHierarchyIcon == (UnityEngine.Object) null)
                    EntitasHierarchyIcon._entityLinkWarnHierarchyIcon = EditorLayout.LoadTexture("l:EntitasEntityLinkWarnHierarchyIcon");
                return EntitasHierarchyIcon._entityLinkWarnHierarchyIcon;
            }
        }

        private static Texture2D systemsHierarchyIcon
        {
            get
            {
                if ((UnityEngine.Object) EntitasHierarchyIcon._systemsHierarchyIcon == (UnityEngine.Object) null)
                    EntitasHierarchyIcon._systemsHierarchyIcon = EditorLayout.LoadTexture("l:EntitasSystemsHierarchyIcon");
                return EntitasHierarchyIcon._systemsHierarchyIcon;
            }
        }

        private static Texture2D systemsWarnHierarchyIcon
        {
            get
            {
                if ((UnityEngine.Object) EntitasHierarchyIcon._systemsWarnHierarchyIcon == (UnityEngine.Object) null)
                    EntitasHierarchyIcon._systemsWarnHierarchyIcon = EditorLayout.LoadTexture("l:EntitasSystemsWarnHierarchyIcon");
                return EntitasHierarchyIcon._systemsWarnHierarchyIcon;
            }
        }

        static EntitasHierarchyIcon()
        {
            try
            {
                EntitasHierarchyIcon._systemWarningThreshold = new Preferences("Entitas.properties", Environment.UserName + ".userproperties").CreateAndConfigure<VisualDebuggingConfig>().systemWarningThreshold;
            }
            catch (Exception ex)
            {
                EntitasHierarchyIcon._systemWarningThreshold = int.MaxValue;
            }
            EditorApplication.hierarchyWindowItemOnGUI += new EditorApplication.HierarchyWindowItemCallback(EntitasHierarchyIcon.onHierarchyWindowItemOnGUI);
        }

        private static void onHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            GameObject gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (!((UnityEngine.Object) gameObject != (UnityEngine.Object) null))
                return;
            Rect position = new Rect((float) ((double) selectionRect.x + (double) selectionRect.width - 18.0), selectionRect.y, 16f, 16f);
            ContextObserverBehaviour component1 = gameObject.GetComponent<ContextObserverBehaviour>();
            if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
            {
                if (component1.contextObserver.context.retainedEntitiesCount != 0)
                    GUI.DrawTexture(position, (Texture) EntitasHierarchyIcon.contextErrorHierarchyIcon);
                else
                    GUI.DrawTexture(position, (Texture) EntitasHierarchyIcon.contextHierarchyIcon);
            }
            else
            {
                EntityBehaviour component2 = gameObject.GetComponent<EntityBehaviour>();
                if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
                {
                    if (component2.entity.isEnabled)
                        GUI.DrawTexture(position, (Texture) EntitasHierarchyIcon.entityHierarchyIcon);
                    else
                        GUI.DrawTexture(position, (Texture) EntitasHierarchyIcon.entityErrorHierarchyIcon);
                }
                else
                {
                    EntityLink component3 = gameObject.GetComponent<EntityLink>();
                    if ((UnityEngine.Object) component3 != (UnityEngine.Object) null)
                    {
                        if (component3.entity != null)
                            GUI.DrawTexture(position, (Texture) EntitasHierarchyIcon.entityLinkHierarchyIcon);
                        else
                            GUI.DrawTexture(position, (Texture) EntitasHierarchyIcon.entityLinkWarnHierarchyIcon);
                    }
                    else
                    {
                        DebugSystemsBehaviour component4 = gameObject.GetComponent<DebugSystemsBehaviour>();
                        if (!((UnityEngine.Object) component4 != (UnityEngine.Object) null))
                            return;
                        if (component4.systems.executeDuration < (double) EntitasHierarchyIcon._systemWarningThreshold)
                            GUI.DrawTexture(position, (Texture) EntitasHierarchyIcon.systemsHierarchyIcon);
                        else
                            GUI.DrawTexture(position, (Texture) EntitasHierarchyIcon.systemsWarnHierarchyIcon);
                    }
                }
            }
        }
    }
}