namespace Assets.Game.Scripts.Model.Components.Items
{
    internal interface IHittable
    {
        public float Damage { get; set; }
    }

    internal interface IDischargeable
    {
        public float TotalCharge { get; set; }
        public float CurrentCharge { get; set; }
    }

    internal interface IWeapon : IHittable, IDischargeable
    {
    }
}