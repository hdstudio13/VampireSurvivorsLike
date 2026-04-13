using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace WindowsSystem
{
    public abstract class Window<TModel> : MonoBehaviour, IWindow where TModel : WindowModel, new()
    {
        [SerializeField] protected RectTransform rt;
        
        protected TModel Model;

        public GameObject GameObject => gameObject;
        public event Action<IWindow> OnWindowShown;
        public event Action<IWindow> OnWindowHidden;
        public event Action<IWindow> OnWindowClosed;
        
        private void Reset()
        {
            gameObject.name = GetType().Name;
            rt = GetComponent<RectTransform>();
        }
        
        public void SetUp(TModel model)
        {
            Model = model;
            OnSetUp();
        }

        public async UniTask Show(bool moveOnTop = true)
        {
            if (Model.State == WindowState.Showing)
                throw new WindowAlreadyShowingException();
            if (moveOnTop)
                transform.SetSiblingIndex(transform.parent.childCount - 1);

            gameObject.SetActive(true);
            rt.localScale = Vector3.one;
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.sizeDelta = new Vector2(0,0);
            rt.anchoredPosition = new Vector2(0, 0);
            
            UniTaskCompletionSource completion = new UniTaskCompletionSource();

            Model.State = WindowState.Showing;
            OnShowing(completion);
            await completion.Task;
            Model.State = WindowState.Shown;
            OnShown();
            OnWindowShown?.Invoke(this);
        }

        public async UniTask Hide()
        {
            if (Model.State == WindowState.Hiding)
                throw new WindowAlreadyHidingException();

            UniTaskCompletionSource completion = new UniTaskCompletionSource();

            Model.State = WindowState.Hiding;
            OnHiding(completion);
            await completion.Task;
            Model.State = WindowState.Hidden;
            gameObject.SetActive(false);
            OnHidden();
            OnWindowHidden?.Invoke(this);
        }

        public async UniTask Close()
        {
            if (Model.State == WindowState.Closing)
                throw new WindowAlreadyClosingException();

            UniTaskCompletionSource completion = new UniTaskCompletionSource();

            Model.State = WindowState.Closing;
            OnClosing(completion);
            await completion.Task;
            Model.State = WindowState.None;
            OnClosed();
            OnWindowClosed?.Invoke(this);
        }

        protected virtual void OnSetUp() { }
        protected virtual void OnShowing(IResolvePromise completionSource)
        {
            completionSource.TrySetResult();
        }
        protected virtual void OnShown() { }
        protected virtual void OnHiding(IResolvePromise completionSource)
        {
            completionSource.TrySetResult();
        }
        protected virtual void OnHidden() { }
        protected virtual void OnClosing(IResolvePromise completionSource)
        {
            completionSource.TrySetResult();
        }
        protected virtual void OnClosed() {}

        public class WindowAlreadyShowingException : Exception { }
        public class WindowAlreadyClosingException : Exception { }
        public class WindowAlreadyHidingException : Exception { }
    }
}