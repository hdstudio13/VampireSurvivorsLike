using System;
using Entitas;

namespace Common
{
    public static class EntityExtensions
    {
        public static T With<T>(this T entity, Action<T> action) where T : Entity
        {
            if (entity == null) return null;
            action?.Invoke(entity);
            return entity;
        }
    }
}