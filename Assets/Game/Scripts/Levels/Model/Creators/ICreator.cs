namespace Assets.Game.Scripts.Levels.Model.Creators
{
    internal interface ICreator<out T>
    {
        public T CreateNext();
        bool CanCreate();
    }
}