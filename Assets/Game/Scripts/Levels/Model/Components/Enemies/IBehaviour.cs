namespace Assets.Game.Scripts.Levels.Model.Components.Enemies
{
    internal interface IBehaviour
    {
        float Evaluate();
        void Behave();
    }
}