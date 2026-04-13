using AssetManagement;
using UnityEngine;

namespace WindowsSystem
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly Transform _windowsRoot;
        private readonly string _windowsPath;

        public WindowFactory(IAssetProvider assetProvider, Transform windowsRoot, string windowsPath)
        {
            _assetProvider = assetProvider;
            _windowsRoot = windowsRoot;
            _windowsPath = windowsPath;
        }
        
        public T Create<T, Y>(Y model) where T : Window<Y> where Y : WindowModel, new()
        {
            var windowPrefab = _assetProvider.GetAsset<GameObject>($"{_windowsPath}/{typeof(T).Name}.prefab");
            
            var window = Object.Instantiate(windowPrefab).GetComponent<T>();
            window.transform.SetParent(_windowsRoot);
            window.gameObject.SetActive(false);
            window.SetUp(model);
            window.OnWindowClosed += OnWindowClosed;
            return window;
        }

        public T Create<T>() where T : SimpleWindow
        {
            var windowPrefab = _assetProvider.GetAsset<GameObject>($"{_windowsPath}/{typeof(T).Name}.prefab");
            
            var window = Object.Instantiate(windowPrefab).GetComponent<T>();
            window.transform.SetParent(_windowsRoot);
            window.gameObject.SetActive(false);
            window.OnWindowClosed += OnWindowClosed;
            return window;
        }
        
        private void OnWindowClosed(IWindow window)
        {
            // some additional logic...
        }
    }
}