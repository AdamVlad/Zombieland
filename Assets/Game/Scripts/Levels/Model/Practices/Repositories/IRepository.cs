namespace Assets.Game.Scripts.Levels.Model.Practices.Repositories
{
    internal interface IRepository<out T>
    {
        T Get(int index);
        int Count();
    }
}