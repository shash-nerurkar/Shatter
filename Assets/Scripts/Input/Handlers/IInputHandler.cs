namespace Scripts.Input.Handlers
{
    /// <summary>
    /// Manages user input for playable entities.
    /// </summary>
    public interface IInputHandler
    {
        #region Methods

        /// <summary>
        /// Enables user input for the entity.
        /// </summary>
        public void EnableInput();

        /// <summary>
        /// Disables user input for the entity.
        /// </summary>
        public void DisableInput();

        /// <summary>
        /// Sets input events for the entity.
        /// </summary>
        public void SetInputs();

        /// <summary>
        /// Polls for user inputs.
        /// </summary>
        public void PollInputs();

        #endregion
    }
}