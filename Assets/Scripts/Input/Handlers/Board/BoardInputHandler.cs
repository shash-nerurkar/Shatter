using Scripts.Gameplay.Commands;
using Scripts.Gameplay.Commands.Board;
using UnityEngine;

namespace Scripts.Input.Handlers.Board
{
    /// <summary>
    /// Manages user input for the game board.
    /// </summary>
    public class BoardInputHandler : MonoBehaviour, IInputHandler
    {
        #region Methods

        public void EnableInput()
        {
            
        }

        public void DisableInput()
        {
            
        }

        public void SetInputs()
        {
            
        }

        public void PollInputs()
        {
            GameCommandBus.Publish(new AimCommand(), new AimCommandContext(direction: Vector2.up));
        }

        #endregion
    }
}