namespace Scripts.Input.Handlers
{
    /// <summary>
    /// Handles user input for playable entities.
    /// </summary>
    public interface IInputHandler
    {
        #region Methods

        /// <summary>
        /// Sets up handled events.
        /// </summary>
        public void Init();

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

        #endregion
    }
}