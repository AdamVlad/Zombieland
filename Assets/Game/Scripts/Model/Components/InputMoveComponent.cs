using UnityEngine;

namespace Assets.Game.Scripts.Model.Components
{
    internal struct InputMoveComponent
    {
        public Vector2 MoveInput;
        public bool IsMoveInputStarted => MoveInput.x != 0 || MoveInput.y != 0;
    }
}