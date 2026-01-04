using UnityEngine;

namespace Scripts.Entity.Behaviours.Movement
{
    /// <summary>
    /// An entity which moves.
    /// </summary>
    public interface IMovable
    {
        /// <summary>
        /// The speed at which the entity moves.
        /// </summary>
        public float Speed { get; }
        
        /// <summary>
        /// The direction in which the entity moves.
        /// </summary>
        public Vector2 MoveDirection { get; }

        /// <summary>
        /// Moves the entity.
        /// </summary>
        /// <param name="direction">The direction to move in.</param>
        public void Move(Vector2 direction);
    }
}