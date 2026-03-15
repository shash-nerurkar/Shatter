using Scripts.Entity.Behaviours.Movement;
using UnityEngine;

namespace Scripts.Gameplay.Commands.Board
{
    /// <summary>
    /// A command to move an entity.
    /// </summary>
    public class MoveCommand : IGameCommand<MoveCommandContext>
    {
        private Vector3 moveTo;
        
        /// <summary>
        /// Executes the move command with the given context.
        /// </summary>
        public void Execute(IMovable movable, MoveCommandContext context)
        {
            moveTo = Camera.main.ScreenToWorldPoint(context.MoveTo);

            movable.Move(moveTo - movable.CurrentPosition);
        }
    }

    /// <summary>
    /// The context for the <see cref="MoveCommand"/>.
    /// </summary>
    public sealed class MoveCommandContext : IGameCommandContext
    {
        /// <summary>
        /// The position to move to.
        /// </summary>
        public Vector2 MoveTo { get; }
        
        public MoveCommandContext(Vector2 moveTo)
        {
            MoveTo = moveTo;
        }
    }
}