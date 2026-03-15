using Scripts.Entity.Behaviours.Movement;
using UnityEngine;

namespace Scripts.Gameplay.Commands.Board
{
    /// <summary>
    /// A command to set an entity's aim.
    /// </summary>
    public class AimCommand : IGameCommand<IGameCommandContext>
    {
        private Vector3 aimAt;

        /// <summary>
        /// Executes the aim command with the given context.
        /// </summary>
        public void Execute(IAimable aimable, AimCommandContext context)
        {
            aimAt = context.IsScreenSpace ? Camera.main.ScreenToWorldPoint(context.AimAt) : context.AimAt;

            aimable.Aim(aimAt - aimable.CurrentPosition);
        }
    }

    /// <summary>
    /// The context for the <see cref="AimCommand"/>.
    /// </summary>
    public sealed class AimCommandContext : IGameCommandContext
    {
        /// <summary>
        /// The point to aim at in screen space.
        /// </summary>
        public Vector2 AimAt { get; }

        public bool IsScreenSpace { get;}

        public AimCommandContext(Vector2 aimAt, bool isScreenSpace)
        {
            AimAt = aimAt;
            IsScreenSpace = isScreenSpace;
        }
    }
}