using UnityEngine;

namespace Architecture.Configs
{
    public interface IConfigProvider
    {
        TConfig Get<TConfig>() where TConfig : ScriptableObject;
    }
}