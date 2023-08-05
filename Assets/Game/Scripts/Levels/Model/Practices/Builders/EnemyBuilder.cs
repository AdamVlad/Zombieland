using Assets.Game.Scripts.Levels.Model.Components.Data.Enemies;
using Assets.Game.Scripts.Levels.Model.Practices.Builders.Base;
using Assets.Game.Scripts.Levels.Model.Practices.Builders.Context;

namespace Assets.Game.Scripts.Levels.Model.Practices.Builders
{
    internal class EnemyBuilder : EcsObjectsBuilder<Enemy>
    {
        public EnemyBuilder(EcsContext context) : base(context)
        {
        }

        private bool _withEnemy;
        private bool _withTag;
        private bool _withHpBar;
        private bool _withHealth;

        public EnemyBuilder WithEnemy()
        {
            _withEnemy = true;
            return this;
        }

        public EnemyBuilder WithTag()
        {
            _withTag = true;
            return this;
        }

        public EnemyBuilder WithHpBar()
        {
            _withHpBar = true;
            return this;
        }

        public EnemyBuilder WithHealth()
        {
            _withHealth = true;
            return this;
        }

        protected override void BuildInternal()
        {
            var enemy = ObjectGo.GetComponent<Enemy>();
            if (_withEnemy) Context.SetEnemy(enemy);
            if (_withHpBar) Context.SetEnemyHpBar(enemy);
            if (_withHealth) Context.SetHealth(enemy.Settings.MaxHealth);
            if (_withTag) Context.SetEnemyTag();
        }

        protected override void ResetInternal()
        {
            _withEnemy = false;
            _withHpBar = false;
            _withHealth = false;
            _withTag = false;
        }
    }
}