using System;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

namespace AssetManagement
{
    public interface IAssetProvider : IDisposable
    {
        TAsset GetAsset<TAsset>(string key) where TAsset : Object;
        UniTask<TAsset> GetAssetAsync<TAsset>(string key) where TAsset : Object;
    }
}