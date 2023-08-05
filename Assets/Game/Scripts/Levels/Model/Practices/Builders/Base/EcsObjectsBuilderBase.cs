using Assets.Game.Scripts.Levels.Model.Practices.Builders.Context;

namespace Assets.Game.Scripts.Levels.Model.Practices.Builders.Base
{
    internal abstract class EcsObjectsBuilderBase
    {
        protected EcsObjectsBuilderBase(EcsContext context)
        {
            Context = context;
        }

        protected readonly EcsContext Context;
    }
}