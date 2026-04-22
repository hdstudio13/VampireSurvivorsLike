using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gameplay.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private AssetReferenceGameObject playerPrefab;
        [SerializeField] private float moveSpeed = 5;
        [SerializeField] private float rotationSpeed = 20;
        [SerializeField] private float acceleration = 5;
        [SerializeField] private float deacceleration = 5;
        
        public AssetReferenceGameObject PlayerPrefab => playerPrefab;
        public float MoveSpeed => moveSpeed;
        public float RotationSpeed => rotationSpeed;
        public float Acceleration => acceleration;
        public float Deacceleration => deacceleration;
    }
}