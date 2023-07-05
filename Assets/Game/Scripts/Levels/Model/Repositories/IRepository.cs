namespace Assets.Game.Scripts.Levels.Model.Repositories
{
    internal interface IRepository<out T>
    {
        T Get(int index);
        int Count();
    }
}