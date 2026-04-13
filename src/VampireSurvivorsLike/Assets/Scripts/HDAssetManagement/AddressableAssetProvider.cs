using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

namespace AssetManagement
{
    public class AddressableAssetProvider : IAssetProvider
    {
        private readonly CancellationTokenSource _cancellationToken;
        private readonly AddressableAssetMediator _mediator;
        private readonly Dictionary<string, AddressableLoadedAssetWrapper> _cachedAssets;
        
        public AddressableAssetProvider(AddressableAssetMediator mediator)
        {
            _cancellationToken = new CancellationTokenSource();
            _mediator = mediator;
            _cachedAssets = new();
        }

        public TAsset GetAsset<TAsset>(string key) where TAsset : Object
        {
            if (_cachedAssets.TryGetValue(key, out var result))
            {
                return result.GetAsset<TAsset>();
            }
            
            var wrapper = _mediator.Load<TAsset>(key);
            _cachedAssets[key] = wrapper;
            
            return wrapper.GetAsset<TAsset>();
        }
        
        public async UniTask<TAsset> GetAssetAsync<TAsset>(string key) where TAsset : Object
        {
            if (_cachedAssets.TryGetValue(key, out var result))
            {
                return result.GetAsset<TAsset>();
            }
            
            var wrapper = await _mediator.LoadAsync<TAsset>(key, _cancellationToken.Token);
            
            _cachedAssets[key] = wrapper;
            return wrapper.GetAsset<TAsset>();
        }

        public void Dispose()
        {
            _cancellationToken.Cancel();
            
            foreach (var wrapper in _cachedAssets.Values)
            {
                wrapper.Dispose();
            }
            _cachedAssets.Clear();
        }
    }
}