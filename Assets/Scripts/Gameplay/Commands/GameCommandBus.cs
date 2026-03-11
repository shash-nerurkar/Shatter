using System;


namespace Scripts.Gameplay.Commands
{
    /// <summary>
    /// A bus for raising new game commands.
    /// </summary>
    public static class GameCommandBus
    {
        /// <summary>
        /// The event fired when a new command is published.
        /// </summary>
        public static event Action<IGameCommand<IGameCommandContext>, IGameCommandContext> OnCommandPublished;

        /// <summary>
        /// Publishes a new game command.
        /// </summary>
        public static void Publish(IGameCommand<IGameCommandContext> command, IGameCommandContext context) => OnCommandPublished?.Invoke(command, context);
    }
}