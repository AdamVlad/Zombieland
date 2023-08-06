using Assets.Game.Scripts.Levels.Model.Components.Data.Player;
using Assets.Game.Scripts.Levels.Model.Practices.Builders.Base;
using Assets.Game.Scripts.Levels.Model.Practices.Builders.Context;
using Assets.Game.Scripts.Levels.Model.ScriptableObjects;

using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Practices.Builders
{
    internal class PlayerBuilder : EcsObjectsBuilder<Player>
    {
        [Inject] private readonly GameConfigurationSo _gameSettings;

        public PlayerBuilder(EcsContext context) : base(context)
        {
        }

        private bool _withInput;
        private bool _withHealth;
        private bool _withMove;
        private bool _withRotation;
        private bool _withBackpack;
        private bool _withTag;
        private bool _withPlayer;

        public PlayerBuilder WithInput()
        {
            _withInput = true;
            return this;
        }

        public PlayerBuilder WithHealth()
        {
            _withHealth = true;
            return this;
        }

        public PlayerBuilder WithMove()
        {
            _withMove = true;
            return this;
        }

        public PlayerBuilder WithRotation()
        {
            _withRotation = true;
            return this;
        }

        public PlayerBuilder WithBackpack()
        {
            _withBackpack = true;
            return this;
        }

        public PlayerBuilder WithTag()
        {
            _withTag = true;
            return this;
        }

        public PlayerBuilder WithPlayer()
        {
            _withPlayer = true;
            return this;
        }

        protected override void BuildInternal()
        {
            var player = ObjectGo.GetComponent<Player>();
            if (_withInput) Context.SetInput();
            if (_withHealth) Context.SetHealth(player.Settings.MaxHealth);
            if (_withMove) Context.SetPlayerMove(player.Settings.MoveSpeed / _gameSettings.PlayerMoveSpeedDivider);
            if (_withRotation) Context.SetPlayerRotation(
                player.Settings.RotationSpeed / _gameSettings.RotationSpeedDivider, 
                player.Settings.SmoothTurningAngle);
            if (_withBackpack) Context.SetBackpack(-1, player.WeaponHolderPoint);
            if (_withTag) Context.SetPlayerTag();
            if (_withPlayer) Context.SetPlayer(player);
        }

        protected override void ResetInternal()
        {
            _withInput = false;
            _withHealth = false;
            _withMove = false;
            _withRotation = false;
            _withBackpack = false;
            _withTag = false;
            _withPlayer = false;
        }
    }
}