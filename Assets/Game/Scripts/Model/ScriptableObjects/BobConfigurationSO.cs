using UnityEngine;

namespace Assets.Game.Scripts.Model.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BobSettings", menuName = "CharactersSettings/BobSettings")]
    internal class BobConfigurationSO : ScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        public GameObject Prefab => _prefab;

        [SerializeField] private float _moveSpeed;
        public float MoveSpeed => _moveSpeed;

        [SerializeField] private float _rotationSpeed;
        public float RotationSpeed => _rotationSpeed;

        [SerializeField] private float _smoothTurningAngle;
        public float SmoothTurningAngle => _smoothTurningAngle;
    }
}