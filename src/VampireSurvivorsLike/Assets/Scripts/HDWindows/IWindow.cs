using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace WindowsSystem
{
    public interface IWindow
    {
        public GameObject GameObject { get; }
        UniTask Show(bool moveOnTop = true);
        UniTask Hide();
        UniTask Close();
        event Action<IWindow> OnWindowShown;
        event Action<IWindow> OnWindowHidden;
        event Action<IWindow> OnWindowClosed;
    }
}