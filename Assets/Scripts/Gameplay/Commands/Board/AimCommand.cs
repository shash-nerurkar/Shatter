using Scripts.Entity.Behaviours.Movement;
using UnityEngine;

namespace Scripts.Gameplay.Commands.Board
{
    /// <summary>
    /// A command to aim an entity's weapon.
    /// </summary>
    public class AimCommand : IGameCommand<IGameCommandContext>
    {
        public AimCommand()
        {
            
        }

        /// <summary>
        /// Executes the aim command with the given context.
        /// </summary>
        public void Execute(IMovable movable, AimCommandContext context)
        {
            
        }
    }

    /// <summary>
    /// The context for the <see cref="AimCommand"/>.
    /// </summary>
    public sealed class AimCommandContext : IGameCommandContext
    {
        /// <summary>
        /// The direction to aim in.
        /// </summary>
        public Vector2 Direction { get; }

        public AimCommandContext(Vector2 direction)
        {
            Direction = direction;
        }
    }
}