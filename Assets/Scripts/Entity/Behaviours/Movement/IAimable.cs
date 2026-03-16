using UnityEngine;

namespace Scripts.Entity.Behaviours.Movement
{
    /// <summary>
    /// An entity which can aim.
    /// </summary>
    public interface IAimable
    {
        /// <summary>
        /// The current position of the entity.
        /// </summary>
        public Vector3 CurrentPosition { get; }

        /// <summary>
        /// The direction the entity is currently aiming in.
        /// </summary>
        public Vector2 AimDirection { get; }

        /// <summary>
        /// Positions the entity's aim.
        /// </summary>
        /// <param name="direction">The direction to aim in.</param>
        public void Aim(Vector2 direction);
    }
}