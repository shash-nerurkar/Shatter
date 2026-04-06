namespace Scripts.Input.Handlers
{
    public interface IInputHandler
    {
        /// <summary>
        /// Sets up handled events.
        /// </summary>
        public void Init(IInputHandlerContext context);

        /// <summary>
        /// Removes all handled events.
        /// </summary>
        public void Dispose();

        /// <summary>
        /// Handles events that need polling.
        /// </summary>
        public void Poll();

        /// <summary>
        /// Enables handling of events.
        /// </summary>
        public void Enable();

        /// <summary>
        /// Disables handling of events.
        /// </summary>
        public void Disable();
    }

    /// <summary>
    /// Represents the context passed into a input handler.
    /// </summary>
    public interface IInputHandlerContext
    {
        
    }
}