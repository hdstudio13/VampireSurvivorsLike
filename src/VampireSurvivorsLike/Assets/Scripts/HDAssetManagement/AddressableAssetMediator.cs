using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Debug;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace AssetManagement
{
    public class AddressableAssetMediator
    {
        private readonly Dictionary<string, AddressableLoadRegistryEntry> _assets = new();
        
        public AddressableLoadedAssetWrapper Load<TAsset>(string key) where TAsset : Object 
        {
            if (_assets.TryGetValue(key, out var entry))
            {
                return entry.GetAsset();
            }
            
            var handle = Addressables.LoadAssetAsync<TAsset>(key);
            
            handle.WaitForCompletion();

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                entry = new AddressableLoadRegistryEntry(() => OnAssetBecameUnused(key), handle);
                _assets[key] = entry;
                return entry.GetAsset();
            }
            
            this.LogError("Failed to load asset with key: " + key);
            return default;
        }

        public async UniTask<AddressableLoadedAssetWrapper> LoadAsync<TAsset>(string key, CancellationToken cancellationToken = default) where TAsset : UnityEngine.Object
        {
            if (_assets.TryGetValue(key, out var entry))
            {
                return entry.GetAsset();
            }
            
            var handle = Addressables.LoadAssetAsync<TAsset>(key);

            try
            {
                await handle.WithCancellation(cancellationToken);
                
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    entry = new AddressableLoadRegistryEntry(() => OnAssetBecameUnused(key), handle);
                    _assets[key] = entry;
                    return entry.GetAsset();
                }

                this.LogError("Failed to load asset with key: " + key);
                return default;
            }
            catch (OperationCanceledException)
            {
                this.Log("Loading was cancelled for asset with key: " + key);
                handle.Release();
                return default;
            }
        }
        
        private void OnAssetBecameUnused(string key)
        {
            if (_assets.TryGetValue(key, out var entry))
            {
                entry.Dispose();
                _assets.Remove(key);
            }
        }
    }
}