using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DesperateDevs.Extensions;
using DesperateDevs.Reflection;
using Entitas.CodeGeneration.Attributes;
using Entitas.VisualDebugging.Unity;
using UnityEditor;

namespace Entitas.VisualDebuggingFix.Editor
{
  public static class EntitasStats
    {
        [MenuItem("Tools/Entitas/Show Stats", false, 200)]
        public static void ShowStats()
        {
            string message = string.Join("\n", EntitasStats.GetStats().Select<KeyValuePair<string, int>, string>((Func<KeyValuePair<string, int>, string>) (kv => $"{kv.Key}: {kv.Value}")));
            UnityEngine.Debug.Log((object) message);
            EditorUtility.DisplayDialog("Entitas Stats", message, "Close");
        }

        public static Dictionary<string, int> GetStats()
        {
            IEnumerable<System.Type> allTypes = AppDomain.CurrentDomain.GetAllTypes();
            System.Type[] array1 = allTypes.Where<System.Type>((Func<System.Type, bool>) (type => type.ImplementsInterface<IComponent>())).ToArray<System.Type>();
            System.Type[] array2 = allTypes.Where<System.Type>(new Func<System.Type, bool>(EntitasStats.isSystem)).ToArray<System.Type>();
            Dictionary<string, int> contexts = EntitasStats.getContexts(array1);
            Dictionary<string, int> stats = new Dictionary<string, int>()
            {
                {
                    "Total Components",
                    array1.Length
                },
                {
                    "Systems",
                    array2.Length
                }
            };
            foreach (KeyValuePair<string, int> keyValuePair in contexts)
                stats.Add("Components in " + keyValuePair.Key, keyValuePair.Value);
            return stats;
        }

        private static Dictionary<string, int> getContexts(System.Type[] components)
        {
            return ((IEnumerable<System.Type>) components).Aggregate<System.Type, Dictionary<string, int>>(new Dictionary<string, int>(), (Func<Dictionary<string, int>, System.Type, Dictionary<string, int>>) ((contexts, type) =>
            {
                foreach (string key in EntitasStats.getContextNamesOrDefault(type))
                {
                    if (!contexts.ContainsKey(key))
                        contexts.Add(key, 0);
                    ++contexts[key];
                }
                return contexts;
            }));
        }

        private static string[] getContextNames(System.Type type)
        {
            return Attribute.GetCustomAttributes((MemberInfo) type).OfType<ContextAttribute>().Select<ContextAttribute, string>((Func<ContextAttribute, string>) (attr => attr.contextName)).ToArray<string>();
        }

        private static string[] getContextNamesOrDefault(System.Type type)
        {
            string[] contextNamesOrDefault = EntitasStats.getContextNames(type);
            if (contextNamesOrDefault.Length == 0)
                contextNamesOrDefault = new string[1]{ "Default" };
            return contextNamesOrDefault;
        }

        private static bool isSystem(System.Type type)
        {
            return type.ImplementsInterface<ISystem>() && type != typeof (ReactiveSystem<>) && type != typeof (MultiReactiveSystem<,>) && type != typeof (Systems) && type != typeof (DebugSystems) && type != typeof (JobSystem<>) && type.FullName != "Feature";
        }
    }
}