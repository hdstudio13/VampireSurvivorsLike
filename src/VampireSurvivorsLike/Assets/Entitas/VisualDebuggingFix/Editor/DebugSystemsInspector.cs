using System;
using System.Collections.Generic;
using System.Linq;
using DesperateDevs.Serialization;
using DesperateDevs.Unity.Editor;
using Entitas.VisualDebugging.Unity;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebuggingFix.Editor
{
    [CustomEditor(typeof (DebugSystemsBehaviour))]
    public class DebugSystemsInspector : UnityEditor.Editor
    {
        private Graph _systemsMonitor;
        private Queue<float> _systemMonitorData;
        private const int SYSTEM_MONITOR_DATA_LENGTH = 60;
        private static bool _showDetails = false;
        private static bool _showSystemsMonitor = true;
        private static bool _showSystemsList = true;
        private static bool _showInitializeSystems = true;
        private static bool _showExecuteSystems = true;
        private static bool _showCleanupSystems = true;
        private static bool _showTearDownSystems = true;
        private static bool _hideEmptySystems = true;
        private static string _systemNameSearchString = string.Empty;
        private int _systemWarningThreshold;
        private float _threshold;
        private DebugSystemsInspector.SortMethod _systemSortMethod;
        private int _lastRenderedFrameCount;
        private GUIContent _stepButtonContent;
        private GUIContent _pauseButtonContent;

        private void OnEnable()
        {
            try
            {
                this._systemWarningThreshold = new Preferences("Entitas.properties", Environment.UserName + ".userproperties").CreateAndConfigure<VisualDebuggingConfig>().systemWarningThreshold;
            }
            catch (Exception ex)
            {
                this._systemWarningThreshold = int.MaxValue;
            }
        }

        public override void OnInspectorGUI()
        {
            DebugSystems systems = ((DebugSystemsBehaviour) this.target).systems;
            EditorGUILayout.Space();
            DebugSystemsInspector.drawSystemsOverview(systems);
            EditorGUILayout.Space();
            this.drawSystemsMonitor(systems);
            EditorGUILayout.Space();
            this.drawSystemList(systems);
            EditorGUILayout.Space();
            EditorUtility.SetDirty(this.target);
        }

        private static void drawSystemsOverview(DebugSystems systems)
        {
            DebugSystemsInspector._showDetails = EditorLayout.DrawSectionHeaderToggle("Details", DebugSystemsInspector._showDetails);
            if (!DebugSystemsInspector._showDetails)
                return;
            EditorLayout.BeginSectionContent();
            EditorGUILayout.LabelField(systems.name, EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Initialize Systems", systems.totalInitializeSystemsCount.ToString());
            EditorGUILayout.LabelField("Execute Systems", systems.totalExecuteSystemsCount.ToString());
            EditorGUILayout.LabelField("Cleanup Systems", systems.totalCleanupSystemsCount.ToString());
            EditorGUILayout.LabelField("TearDown Systems", systems.totalTearDownSystemsCount.ToString());
            EditorGUILayout.LabelField("Total Systems", systems.totalSystemsCount.ToString());
            EditorLayout.EndSectionContent();
        }

        private void drawSystemsMonitor(DebugSystems systems)
        {
            if (this._systemsMonitor == null)
            {
                this._systemsMonitor = new Graph(60);
                this._systemMonitorData = new Queue<float>((IEnumerable<float>) new float[60]);
            }
            DebugSystemsInspector._showSystemsMonitor = EditorLayout.DrawSectionHeaderToggle("Performance", DebugSystemsInspector._showSystemsMonitor);
            if (!DebugSystemsInspector._showSystemsMonitor)
                return;
            EditorLayout.BeginSectionContent();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            double num = systems.executeDuration;
            EditorGUILayout.LabelField("Execution duration", num.ToString());
            num = systems.cleanupDuration;
            EditorGUILayout.LabelField("Cleanup duration", num.ToString());
            EditorGUILayout.EndVertical();
            if (this._stepButtonContent == null)
                this._stepButtonContent = EditorGUIUtility.IconContent("StepButton On");
            if (this._pauseButtonContent == null)
                this._pauseButtonContent = EditorGUIUtility.IconContent("PauseButton On");
            systems.paused = GUILayout.Toggle(systems.paused, this._pauseButtonContent, (GUIStyle) "CommandLeft");
            if (GUILayout.Button(this._stepButtonContent, (GUIStyle) "CommandRight"))
            {
                systems.paused = true;
                systems.StepExecute();
                systems.StepCleanup();
                this.addDuration((float) systems.executeDuration + (float) systems.cleanupDuration);
            }
            EditorGUILayout.EndHorizontal();
            if (!EditorApplication.isPaused && !systems.paused)
                this.addDuration((float) systems.executeDuration + (float) systems.cleanupDuration);
            this._systemsMonitor.Draw(this._systemMonitorData.ToArray(), 80f);
            EditorLayout.EndSectionContent();
        }

        private void drawSystemList(DebugSystems systems)
        {
            DebugSystemsInspector._showSystemsList = EditorLayout.DrawSectionHeaderToggle("Systems", DebugSystemsInspector._showSystemsList);
            if (!DebugSystemsInspector._showSystemsList)
                return;
            EditorLayout.BeginSectionContent();
            EditorGUILayout.BeginHorizontal();
            DebugSystems.avgResetInterval = (AvgResetInterval) EditorGUILayout.EnumPopup("Reset average duration Ø", (Enum) DebugSystems.avgResetInterval);
            if (GUILayout.Button("Reset Ø now", EditorStyles.miniButton, GUILayout.Width(88f)))
                systems.ResetDurations();
            EditorGUILayout.EndHorizontal();
            this._threshold = EditorGUILayout.Slider("Threshold Ø ms", this._threshold, 0.0f, 33f);
            DebugSystemsInspector._hideEmptySystems = EditorGUILayout.Toggle("Hide empty systems", DebugSystemsInspector._hideEmptySystems);
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            this._systemSortMethod = (DebugSystemsInspector.SortMethod) EditorGUILayout.EnumPopup((Enum) this._systemSortMethod, EditorStyles.popup, GUILayout.Width(150f));
            DebugSystemsInspector._systemNameSearchString = this.safeSearchTextField(DebugSystemsInspector._systemNameSearchString);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
            DebugSystemsInspector._showInitializeSystems = EditorLayout.DrawSectionHeaderToggle("Initialize Systems", DebugSystemsInspector._showInitializeSystems);
            if (DebugSystemsInspector._showInitializeSystems && DebugSystemsInspector.shouldShowSystems(systems, SystemInterfaceFlags.IInitializeSystem))
            {
                EditorLayout.BeginSectionContent();
                if (this.drawSystemInfos(systems, SystemInterfaceFlags.IInitializeSystem) == 0)
                    EditorGUILayout.LabelField(string.Empty);
                EditorLayout.EndSectionContent();
            }
            DebugSystemsInspector._showExecuteSystems = EditorLayout.DrawSectionHeaderToggle("Execute Systems", DebugSystemsInspector._showExecuteSystems);
            if (DebugSystemsInspector._showExecuteSystems && DebugSystemsInspector.shouldShowSystems(systems, SystemInterfaceFlags.IExecuteSystem))
            {
                EditorLayout.BeginSectionContent();
                if (this.drawSystemInfos(systems, SystemInterfaceFlags.IExecuteSystem) == 0)
                    EditorGUILayout.LabelField(string.Empty);
                EditorLayout.EndSectionContent();
            }
            DebugSystemsInspector._showCleanupSystems = EditorLayout.DrawSectionHeaderToggle("Cleanup Systems", DebugSystemsInspector._showCleanupSystems);
            if (DebugSystemsInspector._showCleanupSystems && DebugSystemsInspector.shouldShowSystems(systems, SystemInterfaceFlags.ICleanupSystem))
            {
                EditorLayout.BeginSectionContent();
                if (this.drawSystemInfos(systems, SystemInterfaceFlags.ICleanupSystem) == 0)
                    EditorGUILayout.LabelField(string.Empty);
                EditorLayout.EndSectionContent();
            }
            DebugSystemsInspector._showTearDownSystems = EditorLayout.DrawSectionHeaderToggle("TearDown Systems", DebugSystemsInspector._showTearDownSystems);
            if (DebugSystemsInspector._showTearDownSystems && DebugSystemsInspector.shouldShowSystems(systems, SystemInterfaceFlags.ITearDownSystem))
            {
                EditorLayout.BeginSectionContent();
                if (this.drawSystemInfos(systems, SystemInterfaceFlags.ITearDownSystem) == 0)
                    EditorGUILayout.LabelField(string.Empty);
                EditorLayout.EndSectionContent();
            }
            EditorLayout.EndSectionContent();
        }

        private int drawSystemInfos(DebugSystems systems, SystemInterfaceFlags type)
        {
            IEnumerable<Entitas.VisualDebugging.Unity.SystemInfo> systemInfos = (IEnumerable<Entitas.VisualDebugging.Unity.SystemInfo>) null;
            switch (type)
            {
                case SystemInterfaceFlags.IInitializeSystem:
                    systemInfos = ((IEnumerable<Entitas.VisualDebugging.Unity.SystemInfo>) systems.initializeSystemInfos).Where<Entitas.VisualDebugging.Unity.SystemInfo>((Func<Entitas.VisualDebugging.Unity.SystemInfo, bool>) (systemInfo => systemInfo.initializationDuration >= (double) this._threshold));
                    break;
                case SystemInterfaceFlags.IExecuteSystem:
                    systemInfos = ((IEnumerable<Entitas.VisualDebugging.Unity.SystemInfo>) systems.executeSystemInfos).Where<Entitas.VisualDebugging.Unity.SystemInfo>((Func<Entitas.VisualDebugging.Unity.SystemInfo, bool>) (systemInfo => systemInfo.averageExecutionDuration >= (double) this._threshold));
                    break;
                case SystemInterfaceFlags.ICleanupSystem:
                    systemInfos = ((IEnumerable<Entitas.VisualDebugging.Unity.SystemInfo>) systems.cleanupSystemInfos).Where<Entitas.VisualDebugging.Unity.SystemInfo>((Func<Entitas.VisualDebugging.Unity.SystemInfo, bool>) (systemInfo => systemInfo.cleanupDuration >= (double) this._threshold));
                    break;
                case SystemInterfaceFlags.ITearDownSystem:
                    systemInfos = ((IEnumerable<Entitas.VisualDebugging.Unity.SystemInfo>) systems.tearDownSystemInfos).Where<Entitas.VisualDebugging.Unity.SystemInfo>((Func<Entitas.VisualDebugging.Unity.SystemInfo, bool>) (systemInfo => systemInfo.teardownDuration >= (double) this._threshold));
                    break;
            }
            IEnumerable<Entitas.VisualDebugging.Unity.SystemInfo> sortedSystemInfos = DebugSystemsInspector.getSortedSystemInfos(systemInfos, this._systemSortMethod);
            int num = 0;
            foreach (Entitas.VisualDebugging.Unity.SystemInfo systemInfo in sortedSystemInfos)
            {
                if (!(systemInfo.system is DebugSystems system2) || DebugSystemsInspector.shouldShowSystems(system2, type))
                {
                    if (EditorLayout.MatchesSearchString((systemInfo.systemName ?? string.Empty).ToLowerInvariant(), (DebugSystemsInspector._systemNameSearchString ?? string.Empty).ToLowerInvariant()))
                    {
                        EditorGUILayout.BeginHorizontal();
                        int indentLevel = EditorGUI.indentLevel;
                        EditorGUI.indentLevel = 0;
                        bool isActive = systemInfo.isActive;
                        if (systemInfo.areAllParentsActive)
                        {
                            systemInfo.isActive = (EditorGUILayout.Toggle((systemInfo.isActive ? 1 : 0) != 0, GUILayout.Width(20f)) ? 1 : 0) != 0;
                        }
                        else
                        {
                            EditorGUI.BeginDisabledGroup(true);
                            EditorGUILayout.Toggle(false, GUILayout.Width(20f));
                        }
                        EditorGUI.EndDisabledGroup();
                        EditorGUI.indentLevel = indentLevel;
                        if (systemInfo.isActive != isActive && systemInfo.system is IReactiveSystem system)
                        {
                            if (systemInfo.isActive)
                                system.Activate();
                            else
                                system.Deactivate();
                        }
                        switch (type)
                        {
                            case SystemInterfaceFlags.IInitializeSystem:
                                EditorGUILayout.LabelField(systemInfo.systemName, systemInfo.initializationDuration.ToString(), this.getSystemStyle(systemInfo, SystemInterfaceFlags.IInitializeSystem));
                                break;
                            case SystemInterfaceFlags.IExecuteSystem:
                                string str1 = $"Ø {systemInfo.averageExecutionDuration:00.000}".PadRight(12);
                                string str2 = $"▼ {systemInfo.minExecutionDuration:00.000}".PadRight(12);
                                string str3 = $"▲ {systemInfo.maxExecutionDuration:00.000}";
                                EditorGUILayout.LabelField(systemInfo.systemName, str1 + str2 + str3, this.getSystemStyle(systemInfo, SystemInterfaceFlags.IExecuteSystem));
                                break;
                            case SystemInterfaceFlags.ICleanupSystem:
                                string str4 = $"Ø {systemInfo.averageCleanupDuration:00.000}".PadRight(12);
                                string str5 = $"▼ {systemInfo.minCleanupDuration:00.000}".PadRight(12);
                                string str6 = $"▲ {systemInfo.maxCleanupDuration:00.000}";
                                EditorGUILayout.LabelField(systemInfo.systemName, str4 + str5 + str6, this.getSystemStyle(systemInfo, SystemInterfaceFlags.ICleanupSystem));
                                break;
                            case SystemInterfaceFlags.ITearDownSystem:
                                EditorGUILayout.LabelField(systemInfo.systemName, systemInfo.teardownDuration.ToString(), this.getSystemStyle(systemInfo, SystemInterfaceFlags.ITearDownSystem));
                                break;
                        }
                        EditorGUILayout.EndHorizontal();
                        ++num;
                    }
                    if (systemInfo.system is DebugSystems system1)
                    {
                        int indentLevel = EditorGUI.indentLevel;
                        ++EditorGUI.indentLevel;
                        num += this.drawSystemInfos(system1, type);
                        EditorGUI.indentLevel = indentLevel;
                    }
                }
            }
            return num;
        }

        private string safeSearchTextField(string value)
        {
            var style = EditorStyles.toolbarSearchField ?? EditorStyles.textField ?? GUI.skin?.textField;
            return EditorGUILayout.TextField(value ?? string.Empty, style);
        }

        private static IEnumerable<Entitas.VisualDebugging.Unity.SystemInfo> getSortedSystemInfos(
            IEnumerable<Entitas.VisualDebugging.Unity.SystemInfo> systemInfos,
            DebugSystemsInspector.SortMethod sortMethod)
        {
            IEnumerable<Entitas.VisualDebugging.Unity.SystemInfo> sortedSystemInfos;
            switch (sortMethod)
            {
                case DebugSystemsInspector.SortMethod.Name:
                    sortedSystemInfos = (IEnumerable<Entitas.VisualDebugging.Unity.SystemInfo>) systemInfos.OrderBy<Entitas.VisualDebugging.Unity.SystemInfo, string>((Func<Entitas.VisualDebugging.Unity.SystemInfo, string>) (systemInfo => systemInfo.systemName));
                    break;
                case DebugSystemsInspector.SortMethod.NameDescending:
                    sortedSystemInfos = (IEnumerable<Entitas.VisualDebugging.Unity.SystemInfo>) systemInfos.OrderByDescending<Entitas.VisualDebugging.Unity.SystemInfo, string>((Func<Entitas.VisualDebugging.Unity.SystemInfo, string>) (systemInfo => systemInfo.systemName));
                    break;
                case DebugSystemsInspector.SortMethod.ExecutionTime:
                    sortedSystemInfos = (IEnumerable<Entitas.VisualDebugging.Unity.SystemInfo>) systemInfos.OrderBy<Entitas.VisualDebugging.Unity.SystemInfo, double>((Func<Entitas.VisualDebugging.Unity.SystemInfo, double>) (systemInfo => systemInfo.averageExecutionDuration));
                    break;
                case DebugSystemsInspector.SortMethod.ExecutionTimeDescending:
                    sortedSystemInfos = (IEnumerable<Entitas.VisualDebugging.Unity.SystemInfo>) systemInfos.OrderByDescending<Entitas.VisualDebugging.Unity.SystemInfo, double>((Func<Entitas.VisualDebugging.Unity.SystemInfo, double>) (systemInfo => systemInfo.averageExecutionDuration));
                    break;
                default:
                    sortedSystemInfos = systemInfos;
                    break;
            }
            return sortedSystemInfos;
        }

        private static bool shouldShowSystems(DebugSystems systems, SystemInterfaceFlags type)
        {
            if (!DebugSystemsInspector._hideEmptySystems)
                return true;
            bool flag;
            switch (type)
            {
                case SystemInterfaceFlags.IInitializeSystem:
                    flag = systems.totalInitializeSystemsCount > 0;
                    break;
                case SystemInterfaceFlags.IExecuteSystem:
                    flag = systems.totalExecuteSystemsCount > 0;
                    break;
                case SystemInterfaceFlags.ICleanupSystem:
                    flag = systems.totalCleanupSystemsCount > 0;
                    break;
                case SystemInterfaceFlags.ITearDownSystem:
                    flag = systems.totalTearDownSystemsCount > 0;
                    break;
                default:
                    flag = true;
                    break;
            }
            return flag;
        }

        private GUIStyle getSystemStyle(Entitas.VisualDebugging.Unity.SystemInfo systemInfo, SystemInterfaceFlags systemFlag)
        {
            GUIStyle systemStyle = new GUIStyle(GUI.skin.label);
            Color color = !systemInfo.isReactiveSystems || !EditorGUIUtility.isProSkin ? systemStyle.normal.textColor : Color.white;
            if (systemFlag == SystemInterfaceFlags.IExecuteSystem && systemInfo.averageExecutionDuration >= (double) this._systemWarningThreshold)
                color = Color.red;
            if (systemFlag == SystemInterfaceFlags.ICleanupSystem && systemInfo.averageCleanupDuration >= (double) this._systemWarningThreshold)
                color = Color.red;
            systemStyle.normal.textColor = color;
            return systemStyle;
        }

        private void addDuration(float duration)
        {
            if (Time.renderedFrameCount == this._lastRenderedFrameCount)
                return;
            this._lastRenderedFrameCount = Time.renderedFrameCount;
            if (this._systemMonitorData.Count >= 60)
            {
                double num = (double) this._systemMonitorData.Dequeue();
            }
            this._systemMonitorData.Enqueue(duration);
        }

        private enum SortMethod
        {
            OrderOfOccurrence,
            Name,
            NameDescending,
            ExecutionTime,
            ExecutionTimeDescending,
        }
    }
}