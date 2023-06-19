using UnityEngine;

namespace Assets.Game.Scripts.Model.Components
{
    internal struct MoveComponent
    {
        public Vector2 MoveInputAxis;

        public float Speed;

        public bool IsMoving => MoveInputAxis.x != 0 || MoveInputAxis.y != 0;
    }
}