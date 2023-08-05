using Assets.Game.Scripts.Levels.Model.Components.Data.Enemies;
using Assets.Game.Scripts.Levels.Model.Practices.Builders.Base;
using Assets.Game.Scripts.Levels.Model.Practices.Builders.Context;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Practices.Builders
{
    internal class EnemyBuilder : CharacterBuilder<Enemy>
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

        protected override void BuildInternal(DiContainer _container)
        {
            var enemy = _character.GetComponent<Enemy>();
            if (_withEnemy) _context.SetEnemy(enemy);
            if (_withHpBar) _context.SetEnemyHpBar(enemy);
            if (_withHealth) _context.SetHealth(enemy.Settings.MaxHealth);
            if (_withTag) _context.SetEnemyTag();
        }
    }
}
