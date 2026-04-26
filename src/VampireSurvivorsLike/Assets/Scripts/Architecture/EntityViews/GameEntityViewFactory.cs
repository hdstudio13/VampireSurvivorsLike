using System.Collections.Generic;
using Architecture.EntityViews.GameContext;
using AssetManagement;
using Debug;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

namespace Architecture.EntityViews
{
    public class GameEntityViewFactory : IGameEntityViewFactory
    {
        private const string IN_POOL_LABEL = " [InPool] ";
        private readonly IAssetProvider _assetProvider;
        private readonly IObjectResolver _resolver;
        private readonly Dictionary<string, ObjectPool<GameEntityView>> _pools = new();
        private readonly Dictionary<GameEntityView, string> _views = new();
        private readonly Vector3 _farAway = new Vector3(9999,9999,9999);

        private readonly Transform _activeViewsParent;
        private readonly Transform _pooledViewsParent;
        
        public GameEntityViewFactory(IAssetProvider assetProvider,IObjectResolver resolver)
        {
            _assetProvider = assetProvider;
            _resolver = resolver;

            var obj = new GameObject("[EntityViews]");
            _activeViewsParent = new GameObject("[Active]").transform;
            _activeViewsParent.SetParent(obj.transform);
            _pooledViewsParent = new GameObject("[Pooled]").transform;
            _pooledViewsParent.SetParent(obj.transform);
        }

        public GameEntityView Create(string path)
        {
            GetPool(path).Get(out var view);
            return view;
        }

        public void Recycle(GameEntityView view)
        {
            if (!_views.TryGetValue(view, out var assetPath))
            {
                this.LogError($"Failed to recycle game entity view: {view}, it was created outside of this factory");
                return;
            }
            
            GetPool(assetPath).Release(view);
        }

        private ObjectPool<GameEntityView> GetPool(string assetPath)
        {
            if (_pools.TryGetValue(assetPath, out var pool))
                return pool;
            return CreateNewPool(assetPath);
        }
        
        private ObjectPool<GameEntityView> CreateNewPool(string assetPath)
        {
            ObjectPool<GameEntityView> pool = new ObjectPool<GameEntityView>(
                createFunc: () => CreateGameEntityView(assetPath),
                actionOnGet: v => OnGetView(assetPath, v),
                actionOnRelease: OnReleaseView,
                actionOnDestroy: OnDestroyView);
            _pools[assetPath] = pool;
            return pool;
        }

        private void OnDestroyView(GameEntityView view)
        {
            _views.Remove(view);
            view.RemoveEntity();
            Object.Destroy(view.gameObject);
        }
        
        private void OnReleaseView(GameEntityView view)
        {
            _views.Remove(view);
            view.gameObject.name = $"{view.gameObject.name}{IN_POOL_LABEL}";
            view.gameObject.SetActive(false);
            view.gameObject.transform.position = _farAway;
            view.transform.SetParent(_pooledViewsParent);
            view.RemoveEntity();
        }
        
        private void OnGetView(string assetPath, GameEntityView view)
        {
            view.gameObject.name = view.gameObject.name.Replace(IN_POOL_LABEL, "");
            view.gameObject.SetActive(true);
            view.transform.SetParent(_activeViewsParent);
            _views[view] = assetPath;
        }
        
        private GameEntityView CreateGameEntityView(string path)
        {
            GameObject prefab = _assetProvider.GetAsset<GameObject>(path);
            if (prefab == null)
            {
                this.LogError($"Failed to load prefab at path: {path}");
                return null;
            }
            
            var gameObject = Object.Instantiate(prefab, _farAway, Quaternion.identity);
            GameEntityView viewObject = gameObject.GetComponent<GameEntityView>();
            if (viewObject == null)
            {
                Object.Destroy(gameObject);
                this.LogError($"Failed to instantiate prefab at path: {path}");
                return null;
            }
            
            _resolver.InjectGameObject(viewObject.gameObject);
            _views[viewObject] = path;
            
            return viewObject;
        }
    }
}