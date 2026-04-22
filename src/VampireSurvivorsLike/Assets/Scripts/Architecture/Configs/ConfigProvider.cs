using AssetManagement;
using UnityEngine;

namespace Architecture.Configs
{
    public class ConfigProvider : IConfigProvider
    {
        private readonly IAssetProvider _assetProvider;

        public ConfigProvider(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public TConfig Get<TConfig>() where TConfig : ScriptableObject
        {
            return _assetProvider.GetAsset<TConfig>($"Configs/{typeof(TConfig).Name}.asset");
        }
    }
}