using UnityEngine;

namespace WindowsSystem
{
    [CreateAssetMenu(fileName = nameof(WindowsConfig), menuName = "ScriptableObjects/Configs/WindowsConfig")]
    public class WindowsConfig : ScriptableObject
    {
        [SerializeField] private string windowsFolderPath = "Windows";
        
        public string WindowsPath => windowsFolderPath;
    }
}