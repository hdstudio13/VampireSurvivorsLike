using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DesperateDevs.Extensions;
using DesperateDevs.Reflection;
using DesperateDevs.Serialization;
using DesperateDevs.Unity.Editor;
using Entitas.VisualDebugging.Unity;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebuggingFix.Editor
{
    public static class EntityDrawer
    {
        private const string DEFAULT_INSTANCE_CREATOR_TEMPLATE_FORMAT = "using System;\nusing Entitas.VisualDebugging.Unity.Editor;\n\npublic class Default${ShortType}InstanceCreator : IDefaultInstanceCreator {\n\n    public bool HandlesType(Type type) {\n        return type == typeof(${Type});\n    }\n\n    public object CreateDefault(Type type) {\n        // TODO return an instance of type ${Type}\n        throw new NotImplementedException();\n    }\n}\n";
        private const string TYPE_DRAWER_TEMPLATE_FORMAT = "using System;\nusing Entitas;\nusing Entitas.VisualDebugging.Unity.Editor;\n\npublic class ${ShortType}TypeDrawer : ITypeDrawer {\n\n    public bool HandlesType(Type type) {\n        return type == typeof(${Type});\n    }\n\n    public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target) {\n        // TODO draw the type ${Type}\n        throw new NotImplementedException();\n    }\n}\n";
        private static Dictionary<string, bool[]> _contextToUnfoldedComponents;
        private static Dictionary<string, string[]> _contextToComponentMemberSearch;
        private static Dictionary<string, GUIStyle[]> _contextToColoredBoxStyles;
        private static Dictionary<string, EntityDrawer.ComponentInfo[]> _contextToComponentInfos;
        private static GUIStyle _foldoutStyle;
        private static string _componentNameSearchString;
        public static readonly IDefaultInstanceCreator[] _defaultInstanceCreators = AppDomain.CurrentDomain.GetInstancesOf<IDefaultInstanceCreator>().ToArray<IDefaultInstanceCreator>();
        public static readonly ITypeDrawer[] _typeDrawers = AppDomain.CurrentDomain.GetInstancesOf<ITypeDrawer>().ToArray<ITypeDrawer>();
        public static readonly IComponentDrawer[] _componentDrawers = AppDomain.CurrentDomain.GetInstancesOf<IComponentDrawer>().ToArray<IComponentDrawer>();

        public static void DrawEntity(IEntity entity)
        {
            Color backgroundColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Destroy Entity"))
                entity.Destroy();
            GUI.backgroundColor = backgroundColor;
            EntityDrawer.DrawComponents(entity);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField($"Retained by ({entity.retainCount})", EditorStyles.boldLabel);
            if (!(entity.aerc is SafeAERC aerc))
                return;
            EditorLayout.BeginVerticalBox();
            foreach (object owner in (IEnumerable<object>) aerc.owners.OrderBy<object, string>((Func<object, string>) (o => o.GetType().Name)))
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(owner.ToString());
                if (EditorLayout.MiniButton("Release"))
                    entity.Release(owner);
                EditorGUILayout.EndHorizontal();
            }
            EditorLayout.EndVerticalBox();
        }

        public static void DrawMultipleEntities(IEntity[] entities)
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            IEntity entity1 = entities[0];
            int index = EntityDrawer.drawAddComponentMenu(entity1);
            if (index >= 0)
            {
                System.Type componentType = entity1.contextInfo.componentTypes[index];
                foreach (IEntity entity2 in entities)
                {
                    IComponent component = entity2.CreateComponent(index, componentType);
                    entity2.AddComponent(index, component);
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
            Color backgroundColor1 = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Destroy selected entities"))
            {
                foreach (IEntity entity3 in entities)
                    entity3.Destroy();
            }
            GUI.backgroundColor = backgroundColor1;
            EditorGUILayout.Space();
            foreach (IEntity entity4 in entities)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(entity4.ToString());
                Color backgroundColor2 = GUI.backgroundColor;
                GUI.backgroundColor = Color.red;
                if (EditorLayout.MiniButton("Destroy Entity"))
                    entity4.Destroy();
                GUI.backgroundColor = backgroundColor2;
                EditorGUILayout.EndHorizontal();
            }
        }

        public static void DrawComponents(IEntity entity)
        {
            bool[] unfoldedComponents = EntityDrawer.getUnfoldedComponents(entity);
            string[] componentMemberSearch = EntityDrawer.getComponentMemberSearch(entity);
            EditorLayout.BeginVerticalBox();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Components ({entity.GetComponents().Length})", EditorStyles.boldLabel);
            if (EditorLayout.MiniButtonLeft("▸"))
            {
                for (int index = 0; index < unfoldedComponents.Length; ++index)
                    unfoldedComponents[index] = false;
            }
            if (EditorLayout.MiniButtonRight("▾"))
            {
                for (int index = 0; index < unfoldedComponents.Length; ++index)
                    unfoldedComponents[index] = true;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
            int index1 = EntityDrawer.drawAddComponentMenu(entity);
            if (index1 >= 0)
            {
                System.Type componentType = entity.contextInfo.componentTypes[index1];
                IComponent component = entity.CreateComponent(index1, componentType);
                entity.AddComponent(index1, component);
            }
            EditorGUILayout.Space();
            EntityDrawer.componentNameSearchString = SafeSearchTextField(EntityDrawer.componentNameSearchString);
            EditorGUILayout.Space();
            int[] componentIndices = entity.GetComponentIndices();
            IComponent[] components = entity.GetComponents();
            for (int index2 = 0; index2 < components.Length; ++index2)
                EntityDrawer.DrawComponent(unfoldedComponents, componentMemberSearch, entity, componentIndices[index2], components[index2]);
            EditorLayout.EndVerticalBox();
        }

        public static void DrawComponent(
            bool[] unfoldedComponents,
            string[] componentMemberSearch,
            IEntity entity,
            int index,
            IComponent component)
        {
            System.Type type = component.GetType();
            string str = type.Name.RemoveComponentSuffix();
            if (!EditorLayout.MatchesSearchString(str.ToLowerInvariant(), (EntityDrawer.componentNameSearchString ?? string.Empty).ToLowerInvariant()))
                return;
            EditorGUILayout.BeginVertical(EntityDrawer.getColoredBoxStyle(entity, index));
            if (!Attribute.IsDefined((MemberInfo) type, typeof (DontDrawComponentAttribute)))
            {
                PublicMemberInfo[] publicMemberInfos = type.GetPublicMemberInfos();
                EditorGUILayout.BeginHorizontal();
                if (publicMemberInfos.Length == 0)
                {
                    EditorGUILayout.LabelField(str, EditorStyles.boldLabel);
                }
                else
                {
                    unfoldedComponents[index] = EditorLayout.Foldout(unfoldedComponents[index], str, EntityDrawer.foldoutStyle);
                    if (unfoldedComponents[index])
                        componentMemberSearch[index] = publicMemberInfos.Length > 5
                            ? SafeSearchTextField(componentMemberSearch[index])
                            : string.Empty;
                }
                if (EditorLayout.MiniButton("-"))
                    entity.RemoveComponent(index);
                EditorGUILayout.EndHorizontal();
                if (unfoldedComponents[index])
                {
                    IComponent component1 = entity.CreateComponent(index, type);
                    component.CopyPublicMemberValues((object) component1);
                    bool flag = false;
                    IComponentDrawer componentDrawer = EntityDrawer.getComponentDrawer(type);
                    if (componentDrawer != null)
                    {
                        EditorGUI.BeginChangeCheck();
                        componentDrawer.DrawComponent(component1);
                        flag = EditorGUI.EndChangeCheck();
                    }
                    else
                    {
                        foreach (PublicMemberInfo publicMemberInfo in publicMemberInfos)
                        {
                            if (EditorLayout.MatchesSearchString(
                                    publicMemberInfo.Name.ToLowerInvariant(),
                                    (componentMemberSearch[index] ?? string.Empty).ToLowerInvariant()))
                            {
                                object obj = publicMemberInfo.GetValue((object) component1);
                                if (EntityDrawer.DrawObjectMember(obj == null ? publicMemberInfo.Type : obj.GetType(), publicMemberInfo.Name, obj, (object) component1, new Action<object, object>(publicMemberInfo.SetValue)))
                                    flag = true;
                            }
                        }
                    }
                    if (flag)
                        entity.ReplaceComponent(index, component1);
                    else
                        entity.GetComponentPool(index).Push(component1);
                }
            }
            else
                EditorGUILayout.LabelField(str, "[DontDrawComponent]", EditorStyles.boldLabel);
            EditorLayout.EndVerticalBox();
        }
        
        private static string SafeSearchTextField(string value)
        {
            var style = EditorStyles.toolbarSearchField ?? EditorStyles.textField ?? GUI.skin?.textField;
            return EditorGUILayout.TextField(value ?? string.Empty, style);
        }
        
        public static bool DrawObjectMember(
            System.Type memberType,
            string memberName,
            object value,
            object target,
            Action<object, object> setValue)
        {
            if (value == null)
            {
                EditorGUI.BeginChangeCheck();
                int num = memberType == typeof (UnityEngine.Object) ? 1 : (memberType.IsSubclassOf(typeof (UnityEngine.Object)) ? 1 : 0);
                EditorGUILayout.BeginHorizontal();
                if (num != 0)
                    setValue(target, (object) EditorGUILayout.ObjectField(memberName, (UnityEngine.Object) value, memberType, true));
                else
                    EditorGUILayout.LabelField(memberName, "null");
                object defaultValue;
                if (EditorLayout.MiniButton("new " + memberType.ToCompilableString().ShortTypeName()) && EntityDrawer.CreateDefault(memberType, out defaultValue))
                    setValue(target, defaultValue);
                EditorGUILayout.EndHorizontal();
                return EditorGUI.EndChangeCheck();
            }
            if (!memberType.IsValueType)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
            }
            EditorGUI.BeginChangeCheck();
            ITypeDrawer typeDrawer = EntityDrawer.getTypeDrawer(memberType);
            if (typeDrawer != null)
            {
                object newValue = typeDrawer.DrawAndGetNewValue(memberType, memberName, value, target);
                setValue(target, newValue);
            }
            else
            {
                System.Type type = target.GetType();
                if ((!type.ImplementsInterface<IComponent>() ? 1 : (!Attribute.IsDefined((MemberInfo) type, typeof (DontDrawComponentAttribute)) ? 1 : 0)) != 0)
                {
                    EditorGUILayout.LabelField(memberName, value.ToString());
                    int indentLevel = EditorGUI.indentLevel;
                    ++EditorGUI.indentLevel;
                    EditorGUILayout.BeginVertical();
                    foreach (PublicMemberInfo publicMemberInfo in memberType.GetPublicMemberInfos())
                    {
                        object obj = publicMemberInfo.GetValue(value);
                        EntityDrawer.DrawObjectMember(obj == null ? publicMemberInfo.Type : obj.GetType(), publicMemberInfo.Name, obj, value, new Action<object, object>(publicMemberInfo.SetValue));
                        if (memberType.IsValueType)
                            setValue(target, value);
                    }
                    EditorGUILayout.EndVertical();
                    EditorGUI.indentLevel = indentLevel;
                }
                else
                    EntityDrawer.drawUnsupportedType(memberType, memberName, value);
            }
            if (!memberType.IsValueType)
            {
                EditorGUILayout.EndVertical();
                if (EditorLayout.MiniButton("×"))
                    setValue(target, (object) null);
                EditorGUILayout.EndHorizontal();
            }
            return EditorGUI.EndChangeCheck();
        }

        public static bool CreateDefault(System.Type type, out object defaultValue)
        {
            try
            {
                defaultValue = Activator.CreateInstance(type);
                return true;
            }
            catch (Exception ex)
            {
                foreach (IDefaultInstanceCreator defaultInstanceCreator in EntityDrawer._defaultInstanceCreators)
                {
                    if (defaultInstanceCreator.HandlesType(type))
                    {
                        defaultValue = defaultInstanceCreator.CreateDefault(type);
                        return true;
                    }
                }
            }
            string compilableString = type.ToCompilableString();
            if (EditorUtility.DisplayDialog("No IDefaultInstanceCreator found", $"There's no IDefaultInstanceCreator implementation to handle the type '{compilableString}'.\nProviding an IDefaultInstanceCreator enables you to create instances for that type.\n\nDo you want to generate an IDefaultInstanceCreator implementation for '{compilableString}'?\n", "Generate", "Cancel"))
                EntityDrawer.GenerateIDefaultInstanceCreator(compilableString);
            defaultValue = (object) null;
            return false;
        }

        private static int drawAddComponentMenu(IEntity entity)
        {
            EntityDrawer.ComponentInfo[] array = ((IEnumerable<EntityDrawer.ComponentInfo>) EntityDrawer.getComponentInfos(entity)).Where<EntityDrawer.ComponentInfo>((Func<EntityDrawer.ComponentInfo, bool>) (info => !entity.HasComponent(info.index))).ToArray<EntityDrawer.ComponentInfo>();
            int index = EditorGUILayout.Popup("Add Component", -1, ((IEnumerable<EntityDrawer.ComponentInfo>) array).Select<EntityDrawer.ComponentInfo, string>((Func<EntityDrawer.ComponentInfo, string>) (info => info.name)).ToArray<string>());
            return index < 0 ? -1 : array[index].index;
        }

        private static void drawUnsupportedType(System.Type memberType, string memberName, object value)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(memberName, value.ToString());
            if (EditorLayout.MiniButton("Missing ITypeDrawer"))
            {
                string compilableString = memberType.ToCompilableString();
                if (EditorUtility.DisplayDialog("No ITypeDrawer found", $"There's no ITypeDrawer implementation to handle the type '{compilableString}'.\nProviding an ITypeDrawer enables you draw instances for that type.\n\nDo you want to generate an ITypeDrawer implementation for '{compilableString}'?\n", "Generate", "Cancel"))
                    EntityDrawer.GenerateITypeDrawer(compilableString);
            }
            EditorGUILayout.EndHorizontal();
        }

        public static void GenerateIDefaultInstanceCreator(string typeName)
        {
            string creatorFolderPath = new Preferences("Entitas.properties", Environment.UserName + ".userproperties").CreateAndConfigure<VisualDebuggingConfig>().defaultInstanceCreatorFolderPath;
            string filePath = $"{creatorFolderPath}{Path.DirectorySeparatorChar.ToString()}Default{typeName.ShortTypeName()}InstanceCreator.cs";
            string template = "using System;\nusing Entitas.VisualDebugging.Unity.Editor;\n\npublic class Default${ShortType}InstanceCreator : IDefaultInstanceCreator {\n\n    public bool HandlesType(Type type) {\n        return type == typeof(${Type});\n    }\n\n    public object CreateDefault(Type type) {\n        // TODO return an instance of type ${Type}\n        throw new NotImplementedException();\n    }\n}\n".Replace("${Type}", typeName).Replace("${ShortType}", typeName.ShortTypeName());
            EntityDrawer.generateTemplate(creatorFolderPath, filePath, template);
        }

        public static void GenerateITypeDrawer(string typeName)
        {
            string drawerFolderPath = new Preferences("Entitas.properties", Environment.UserName + ".userproperties").CreateAndConfigure<VisualDebuggingConfig>().typeDrawerFolderPath;
            EntityDrawer.generateTemplate(drawerFolderPath, $"{drawerFolderPath}{Path.DirectorySeparatorChar.ToString()}{typeName.ShortTypeName()}TypeDrawer.cs", "using System;\nusing Entitas;\nusing Entitas.VisualDebugging.Unity.Editor;\n\npublic class ${ShortType}TypeDrawer : ITypeDrawer {\n\n    public bool HandlesType(Type type) {\n        return type == typeof(${Type});\n    }\n\n    public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target) {\n        // TODO draw the type ${Type}\n        throw new NotImplementedException();\n    }\n}\n".Replace("${Type}", typeName).Replace("${ShortType}", typeName.ShortTypeName()));
        }

        private static void generateTemplate(string folder, string filePath, string template)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            File.WriteAllText(filePath, template);
            EditorApplication.isPlaying = false;
            AssetDatabase.Refresh();
            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(filePath);
        }

        public static Dictionary<string, bool[]> contextToUnfoldedComponents
        {
            get
            {
                return EntityDrawer._contextToUnfoldedComponents ?? (EntityDrawer._contextToUnfoldedComponents = new Dictionary<string, bool[]>());
            }
        }

        public static Dictionary<string, string[]> contextToComponentMemberSearch
        {
            get
            {
                return EntityDrawer._contextToComponentMemberSearch ?? (EntityDrawer._contextToComponentMemberSearch = new Dictionary<string, string[]>());
            }
        }

        public static Dictionary<string, GUIStyle[]> contextToColoredBoxStyles
        {
            get
            {
                return EntityDrawer._contextToColoredBoxStyles ?? (EntityDrawer._contextToColoredBoxStyles = new Dictionary<string, GUIStyle[]>());
            }
        }

        public static Dictionary<string, EntityDrawer.ComponentInfo[]> contextToComponentInfos
        {
            get
            {
                return EntityDrawer._contextToComponentInfos ?? (EntityDrawer._contextToComponentInfos = new Dictionary<string, EntityDrawer.ComponentInfo[]>());
            }
        }

        public static GUIStyle foldoutStyle
        {
            get
            {
                if (EntityDrawer._foldoutStyle == null)
                {
                    EntityDrawer._foldoutStyle = new GUIStyle(EditorStyles.foldout);
                    EntityDrawer._foldoutStyle.fontStyle = FontStyle.Bold;
                }
                return EntityDrawer._foldoutStyle;
            }
        }

        public static string componentNameSearchString
        {
            get
            {
                return EntityDrawer._componentNameSearchString ?? (EntityDrawer._componentNameSearchString = string.Empty);
            }
            set => EntityDrawer._componentNameSearchString = value ?? string.Empty;
        }

        private static bool[] getUnfoldedComponents(IEntity entity)
        {
            bool[] unfoldedComponents;
            if (!EntityDrawer.contextToUnfoldedComponents.TryGetValue(entity.contextInfo.name, out unfoldedComponents))
            {
                unfoldedComponents = new bool[entity.totalComponents];
                for (int index = 0; index < unfoldedComponents.Length; ++index)
                    unfoldedComponents[index] = true;
                EntityDrawer.contextToUnfoldedComponents.Add(entity.contextInfo.name, unfoldedComponents);
            }
            return unfoldedComponents;
        }

        private static string[] getComponentMemberSearch(IEntity entity)
        {
            string[] componentMemberSearch;
            if (!EntityDrawer.contextToComponentMemberSearch.TryGetValue(entity.contextInfo.name, out componentMemberSearch))
            {
                componentMemberSearch = new string[entity.totalComponents];
                for (int index = 0; index < componentMemberSearch.Length; ++index)
                    componentMemberSearch[index] = string.Empty;
                EntityDrawer.contextToComponentMemberSearch.Add(entity.contextInfo.name, componentMemberSearch);
            }
            return componentMemberSearch;
        }

        private static EntityDrawer.ComponentInfo[] getComponentInfos(IEntity entity)
        {
            EntityDrawer.ComponentInfo[] array;
            if (!EntityDrawer.contextToComponentInfos.TryGetValue(entity.contextInfo.name, out array))
            {
                ContextInfo contextInfo = entity.contextInfo;
                List<EntityDrawer.ComponentInfo> componentInfoList = new List<EntityDrawer.ComponentInfo>(contextInfo.componentTypes.Length);
                for (int index = 0; index < contextInfo.componentTypes.Length; ++index)
                    componentInfoList.Add(new EntityDrawer.ComponentInfo()
                    {
                        index = index,
                        name = contextInfo.componentNames[index],
                        type = contextInfo.componentTypes[index]
                    });
                array = componentInfoList.ToArray();
                EntityDrawer.contextToComponentInfos.Add(entity.contextInfo.name, array);
            }
            return array;
        }

        private static GUIStyle getColoredBoxStyle(IEntity entity, int index)
        {
            GUIStyle[] guiStyleArray;
            if (!EntityDrawer.contextToColoredBoxStyles.TryGetValue(entity.contextInfo.name, out guiStyleArray))
            {
                guiStyleArray = new GUIStyle[entity.totalComponents];
                for (int index1 = 0; index1 < guiStyleArray.Length; ++index1)
                {
                    var color = Color.HSVToRGB((float) index1 / (float) entity.totalComponents, 0.7f, 1f);
                    color.a = 0.15f;
                    Color rgb = color;
                    guiStyleArray[index1] = new GUIStyle(GUI.skin.box)
                    {
                        normal = {
                            background = EntityDrawer.createTexture(2, 2, rgb)
                        }
                    };
                }
                EntityDrawer.contextToColoredBoxStyles.Add(entity.contextInfo.name, guiStyleArray);
            }
            return guiStyleArray[index];
        }

        private static Texture2D createTexture(int width, int height, Color color)
        {
            Color[] colors = new Color[width * height];
            for (int index = 0; index < colors.Length; ++index)
                colors[index] = color;
            Texture2D texture = new Texture2D(width, height);
            texture.SetPixels(colors);
            texture.Apply();
            return texture;
        }

        private static IComponentDrawer getComponentDrawer(System.Type type)
        {
            foreach (IComponentDrawer componentDrawer in EntityDrawer._componentDrawers)
            {
                if (componentDrawer.HandlesType(type))
                    return componentDrawer;
            }
            return (IComponentDrawer) null;
        }

        private static ITypeDrawer getTypeDrawer(System.Type type)
        {
            foreach (ITypeDrawer typeDrawer in EntityDrawer._typeDrawers)
            {
                if (typeDrawer.HandlesType(type))
                    return typeDrawer;
            }
            return (ITypeDrawer) null;
        }

        public struct ComponentInfo
        {
            public int index;
            public string name;
            public System.Type type;
        }
    }
}