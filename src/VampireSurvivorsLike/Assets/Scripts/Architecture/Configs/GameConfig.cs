using UnityEngine;

namespace Architecture.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private GameplayLifetimeScope gameplayScopePrefab;

        public GameplayLifetimeScope GameplayScopePrefab => gameplayScopePrefab;
    }
}