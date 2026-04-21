using Architecture.EntityViews.GameContext;
using AssetManagement;
using Debug;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Architecture.EntityViews
{
    public class GameEntityViewFactory : IGameEntityViewFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IObjectResolver _resolver;

        public GameEntityViewFactory(IAssetProvider assetProvider,IObjectResolver resolver)
        {
            _assetProvider = assetProvider;
            _resolver = resolver;
        }

        public GameEntityView Create(string path)
        {
            GameObject prefab = _assetProvider.GetAsset<GameObject>(path);
            if (prefab == null)
            {
                this.LogError($"Failed to load prefab at path: {path}");
                return null;
            }

            var gameObject = Object.Instantiate(prefab);
            GameEntityView viewObject = gameObject.GetComponent<GameEntityView>();
            if (viewObject == null)
            {
                Object.Destroy(gameObject);
                this.LogError($"Failed to instantiate prefab at path: {path}");
                return null;
            }
            
            _resolver.InjectGameObject(viewObject.gameObject);
            
            return viewObject;
        }

        public void Recycle(GameEntityView view)
        {
            view.RemoveEntity();
            Object.Destroy(view.gameObject);
        }
    }
}