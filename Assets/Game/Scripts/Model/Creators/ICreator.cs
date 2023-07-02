namespace Assets.Game.Scripts.Model.Creators
{
    internal interface ICreator<out T>
    {
        public T CreateNext();
        bool CanCreate();
    }
}