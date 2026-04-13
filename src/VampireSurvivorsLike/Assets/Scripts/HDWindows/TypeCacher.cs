using System;
using System.Collections.Generic;
using Debug;

namespace WindowsSystem
{
    public class TypeCacher<T> 
    {
        private readonly Dictionary<Type, T> _cache = new Dictionary<Type, T>();
        
        public void Add<Y>(Y value) where Y : T
        {
            if (value == null)
                return;
            
            var type = typeof(Y);
            if (_cache.ContainsKey(type))
            {
                if (!_cache[type].Equals(null))
                    this.LogError($"There was already a value of type {type} in cache. It will be replaced with new one.");
                _cache[type] = value;
            }
            _cache[type] = value;
        }

        public Y Get<Y>() where Y : T
        {
            _cache.TryGetValue(typeof(Y), out var value);
            return (Y)value;
        }

        public void Remove<Y>() where Y : T
        {
            Remove(typeof(Y));
        }

        public void Remove(Type type)
        {
            _cache.Remove(type);
        }
    }
}