using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Components.Data.Player
{
    internal struct PlayerMoveComponent
    {
        public Vector2 MoveInputAxis;

        public float Speed;

        public bool IsMoving => MoveInputAxis.x != 0 || MoveInputAxis.y != 0;
    }
}