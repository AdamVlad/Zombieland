namespace Assets.Game.Scripts.Levels.Model.Components.Enemies.Behaviours
{
    internal interface IBehaviour
    {
        float Evaluate();
        void Behave();
    }
}