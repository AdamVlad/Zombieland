namespace Assets.Game.Scripts.Levels.Model.Practices.Creators
{
    internal interface ICreator<out T>
    {
        public T CreateNext();
        bool CanCreate();
    }
}