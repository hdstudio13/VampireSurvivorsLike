using System;
using System.Collections.Generic;
using System.Linq;
using VContainer;

namespace Common
{
    public interface IInstantiator
    {
        T Instantiate<T>();
        T Instantiate<T>(params object[] additiveArgs);
    }

    public class Instantiator : IInstantiator
    {
        private readonly IObjectResolver _resolver;

        public Instantiator(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public T Instantiate<T>()
        {
            return Instantiate<T>(Array.Empty<object>());
        }

        public T Instantiate<T>(params object[] additiveArgs)
        {
            var type = typeof(T);
            var constructors = type.GetConstructors()
                .OrderByDescending(c => c.GetParameters().Length);

            Exception lastError = null;

            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters();
                var args = new object[parameters.Length];

                var remainingAdditive = new List<object>(additiveArgs ?? Array.Empty<object>());

                try
                {
                    // 1) Fill ctor parameters from additive args first
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        var paramType = parameters[i].ParameterType;
                        int matchIndex = FindAssignableArgIndex(remainingAdditive, paramType);

                        if (matchIndex >= 0)
                        {
                            args[i] = remainingAdditive[matchIndex];
                            remainingAdditive.RemoveAt(matchIndex);
                        }
                    }

                    // 2) Resolve all still-missing parameters from container
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        if (args[i] == null)
                        {
                            args[i] = _resolver.Resolve(parameters[i].ParameterType);
                        }
                    }

                    // 3) Create instance
                    return (T)constructor.Invoke(args);
                }
                catch (Exception ex)
                {
                    // Try next constructor if this one cannot be satisfied
                    lastError = ex;
                }
            }

            throw new InvalidOperationException(
                $"Could not instantiate type {type.FullName} with provided additive arguments.",
                lastError);
        }

        private static int FindAssignableArgIndex(List<object> args, Type targetType)
        {
            for (int i = 0; i < args.Count; i++)
            {
                var arg = args[i];
                if (arg == null)
                {
                    // null can satisfy reference types or nullable value types
                    if (!targetType.IsValueType || Nullable.GetUnderlyingType(targetType) != null)
                        return i;
                    continue;
                }

                if (targetType.IsInstanceOfType(arg))
                    return i;
            }

            return -1;
        }
    }
}
