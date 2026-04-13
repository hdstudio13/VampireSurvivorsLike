using System;

namespace AssetManagement
{
    public struct AddressableLoadedAssetWrapper : IDisposable
    {
        private Action _onDispose;
        private Object _asset;
        
        public AddressableLoadedAssetWrapper(Action onDispose, Object asset)
        {
            _onDispose = onDispose;
            _asset = asset;
        }
        
        public TAsset GetAsset<TAsset>() where TAsset : UnityEngine.Object
        {
            return _asset as TAsset;
        }
        
        public void Dispose()
        {
            _onDispose?.Invoke();
            _asset = null;
            _onDispose = null;
        }
    }
}