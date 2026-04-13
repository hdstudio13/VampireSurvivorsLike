using System;

namespace HDAttributes.Scripts.Core
{
    public interface IAttributeContainer
    {
        public bool HasAny<T>();
        public bool Has(string id);
        public IReadOnlyAttribute<Y> GetReadOnly<T, Y>(string id) where T : GameAttribute<Y> where Y : struct, IComparable;
        public IReadOnlyAttribute<Y> GetReadOnly<Y>(string id) where Y : struct, IComparable;
    }
}
