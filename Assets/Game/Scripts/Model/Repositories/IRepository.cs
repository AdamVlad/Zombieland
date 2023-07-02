namespace Assets.Game.Scripts.Model.Repositories
{
    internal interface IRepository<out T>
    {
        T Get(int index);
        int Count();
    }
}