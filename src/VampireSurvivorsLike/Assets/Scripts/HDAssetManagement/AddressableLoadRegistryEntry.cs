using System;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AssetManagement
{
    public class AddressableLoadRegistryEntry : IDisposable
    {
        private int _usingsCount;
        private readonly Action _onBecameUnused;
        private AsyncOperationHandle _handle;

        public int UsingsCount => _usingsCount;

        public AddressableLoadRegistryEntry(Action onBecameUnused, AsyncOperationHandle handle)
        {
            _onBecameUnused = onBecameUnused;
            _handle = handle;
        }
        
        public AddressableLoadedAssetWrapper GetAsset()
        {
            _usingsCount++;
            return new AddressableLoadedAssetWrapper(OnAssetReleased, _handle.Result);
        }

        public void Dispose()
        {
            _usingsCount = 0;
            _handle.Release();
        }
        
        private void OnAssetReleased()
        {
            _usingsCount--;
            if (_usingsCount <= 0)
            {
                _onBecameUnused?.Invoke();
            }
        }
    }
}