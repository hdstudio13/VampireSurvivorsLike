using System;
using System.Collections.Generic;
using System.Linq;
using DesperateDevs.Serialization;
using DesperateDevs.Unity.Editor;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebuggingFix.Editor
{
    public class VisualDebuggingPreferencesDrawer : AbstractPreferencesDrawer
    {
        private const string ENTITAS_DISABLE_VISUAL_DEBUGGING = "ENTITAS_DISABLE_VISUAL_DEBUGGING";
        private const string ENTITAS_DISABLE_DEEP_PROFILING = "ENTITAS_DISABLE_DEEP_PROFILING";
        private VisualDebuggingConfig _visualDebuggingConfig;
        private ScriptingDefineSymbols _scriptingDefineSymbols;
        private bool _enableVisualDebugging;
        private bool _enableDeviceDeepProfiling;

        public override string Title => "Visual Debugging";

        public override void Initialize(Preferences preferences)
        {
            this._visualDebuggingConfig = preferences.CreateAndConfigure<VisualDebuggingConfig>();
            preferences.Properties.AddProperties(this._visualDebuggingConfig.DefaultProperties, false);
            preferences.Save();
            preferences.Reload();
            this._scriptingDefineSymbols = new ScriptingDefineSymbols();
            this._enableVisualDebugging = !((IEnumerable<BuildTargetGroup>) ScriptingDefineSymbols.BuildTargetGroups).All<BuildTargetGroup>((Func<BuildTargetGroup, bool>) (buildTarget => PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTarget).Contains("ENTITAS_DISABLE_VISUAL_DEBUGGING")));
            this._enableDeviceDeepProfiling = !((IEnumerable<BuildTargetGroup>) ScriptingDefineSymbols.BuildTargetGroups).All<BuildTargetGroup>((Func<BuildTargetGroup, bool>) (buildTarget => PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTarget).Contains("ENTITAS_DISABLE_DEEP_PROFILING")));
        }

        public override void DrawHeader(Preferences preferences)
        {
        }

        protected override void OnDrawContent(Preferences preferences)
        {
            EditorGUILayout.BeginHorizontal();
            this.drawVisualDebugging();
            if (GUILayout.Button("Show Stats", EditorStyles.miniButton))
                EntitasStats.ShowStats();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
            this._visualDebuggingConfig.systemWarningThreshold = EditorGUILayout.IntField("System Warning Threshold", this._visualDebuggingConfig.systemWarningThreshold);
            EditorGUILayout.Space();
            this.drawDefaultInstanceCreator();
            this.drawTypeDrawerFolder();
        }

        private void drawVisualDebugging()
        {
            EditorGUILayout.BeginVertical();
            EditorGUI.BeginChangeCheck();
            this._enableVisualDebugging = EditorGUILayout.Toggle("Enable Visual Debugging", this._enableVisualDebugging);
            if (EditorGUI.EndChangeCheck())
            {
                if (this._enableVisualDebugging)
                    this._scriptingDefineSymbols.RemoveForAll("ENTITAS_DISABLE_VISUAL_DEBUGGING");
                else
                    this._scriptingDefineSymbols.AddForAll("ENTITAS_DISABLE_VISUAL_DEBUGGING");
            }
            EditorGUI.BeginChangeCheck();
            this._enableDeviceDeepProfiling = EditorGUILayout.Toggle("Enable Device Profiling", this._enableDeviceDeepProfiling);
            if (EditorGUI.EndChangeCheck())
            {
                if (this._enableDeviceDeepProfiling)
                    this._scriptingDefineSymbols.RemoveForAll("ENTITAS_DISABLE_DEEP_PROFILING");
                else
                    this._scriptingDefineSymbols.AddForAll("ENTITAS_DISABLE_DEEP_PROFILING");
            }
            EditorGUILayout.EndVertical();
        }

        private void drawDefaultInstanceCreator()
        {
            EditorGUILayout.BeginHorizontal();
            string str = EditorLayout.ObjectFieldOpenFolderPanel("Default Instance Creators", this._visualDebuggingConfig.defaultInstanceCreatorFolderPath, this._visualDebuggingConfig.defaultInstanceCreatorFolderPath);
            if (!string.IsNullOrEmpty(str))
                this._visualDebuggingConfig.defaultInstanceCreatorFolderPath = str;
            if (EditorLayout.MiniButton("New"))
                EntityDrawer.GenerateIDefaultInstanceCreator("MyType");
            EditorGUILayout.EndHorizontal();
        }

        private void drawTypeDrawerFolder()
        {
            EditorGUILayout.BeginHorizontal();
            string str = EditorLayout.ObjectFieldOpenFolderPanel("Type Drawers", this._visualDebuggingConfig.typeDrawerFolderPath, this._visualDebuggingConfig.typeDrawerFolderPath);
            if (!string.IsNullOrEmpty(str))
                this._visualDebuggingConfig.typeDrawerFolderPath = str;
            if (EditorLayout.MiniButton("New"))
                EntityDrawer.GenerateITypeDrawer("MyType");
            EditorGUILayout.EndHorizontal();
        }
    }
}