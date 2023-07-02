namespace Assets.Game.Scripts.Model.Services
{
    internal interface ICreator<out T>
    {
        public T CreateNext();
        bool CanCreate();
    }
}