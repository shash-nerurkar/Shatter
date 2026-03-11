using Scripts.Entity.Behaviours.Movement;
using UnityEngine;

namespace Scripts.Gameplay.Commands.Board
{
    /// <summary>
    /// A command to move an entity in a specified direction.
    /// </summary>
    public class MoveCommand : IGameCommand<MoveCommandContext>
    {
        public MoveCommand()
        {
            
        }

        /// <summary>
        /// Executes the move command with the given context.
        /// </summary>
        public void Execute(IMovable movable, MoveCommandContext context)
        {
            movable.Move(context.Direction);
        }
    }

    /// <summary>
    /// The context for the <see cref="MoveCommand"/>.
    /// </summary>
    public sealed class MoveCommandContext : IGameCommandContext
    {
        /// <summary>
        /// The direction to move.
        /// </summary>
        public Vector2 Direction { get; }
        
        public MoveCommandContext(Vector2 direction)
        {
            Direction = direction;
        }
    }
}