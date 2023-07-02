using UnityEngine;

namespace Assets.Game.Scripts.Model.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Settings/PlayerSettings")]
    internal class PlayerConfigurationSo : ScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        public GameObject Prefab => _prefab;

        [Space]
        [Header("Physics")]

        [SerializeField] private float _moveSpeed;
        public float MoveSpeed => _moveSpeed;

        [SerializeField] private float _rotationSpeed;
        public float RotationSpeed => _rotationSpeed;

        [SerializeField] private float _smoothTurningAngle;
        public float SmoothTurningAngle => _smoothTurningAngle;

        [Space]
        [Header("Animation")]

        [SerializeField] private string _moveXParameter;
        public string MoveXParameter => _moveXParameter;

        [SerializeField] private string _moveYParameter;
        public string MoveYParameter => _moveYParameter;

        [SerializeField] private string _isWeaponInHandParameter;
        public string IsWeaponInHandParameter => _isWeaponInHandParameter;

        [SerializeField] private string _isShootingParameter;
        public string IsShootingParameter => _isShootingParameter;

        [SerializeField] private string _shootParameter;
        public string ShootParameter => _shootParameter;
    }
}