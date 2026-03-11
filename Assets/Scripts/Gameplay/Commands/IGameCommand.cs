namespace Scripts.Gameplay.Commands
{
    /// <summary>
    /// Represents a game command.
    /// </summary>
    public interface IGameCommand<in TContext> where TContext : IGameCommandContext
    {
        
    }

    /// <summary>
    /// Represents the context passed into a game command.
    /// </summary>
    public interface IGameCommandContext
    {
        
    }
}