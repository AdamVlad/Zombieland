using Assets.Game.Scripts.Levels.Model.Practices.Builders.Context;

namespace Assets.Game.Scripts.Levels.Model.Practices.Builders.Base
{
    internal abstract class CharacterBuilderBase
    {
        public CharacterBuilderBase(EcsContext context)
        {
            _context = context;
        }

        protected EcsContext _context;
    }
}