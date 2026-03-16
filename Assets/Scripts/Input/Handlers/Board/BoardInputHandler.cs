using Scripts.Gameplay.Commands;
using Scripts.Gameplay.Commands.Board;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Scripts.Input.Handlers.Board
{
    /// <summary>
    /// Manages user input for the game board.
    /// </summary>
    public class BoardInputHandler : MonoBehaviour, IInputHandler
    {
        #region Fields

        private int _aimTouchId = -1;

        private int _moveTouchId = -1;

        #endregion

        #region Methods

        public void Enable()
        {
            EnhancedTouchSupport.Enable();
        }

        public void Disable()
        {
            EnhancedTouchSupport.Disable();
        }

        public void Init() {}

        public void Dispose() {}

        public void Poll()
        {
            foreach (ETouch touch in ETouch.activeTouches)
            {
                switch (touch.phase)
                {
                    case UnityEngine.InputSystem.TouchPhase.Began:
                        // TODO - this logic is placeholder, shall be replaced in the future
                        if(touch.startScreenPosition.y < Screen.height * 0.3f)
                            _moveTouchId = touch.touchId;
                        else
                            _aimTouchId = touch.touchId;
                        
                        FireCommand(touch);

                        break;

                    case UnityEngine.InputSystem.TouchPhase.Moved:
                        FireCommand(touch);

                        break;

                    case UnityEngine.InputSystem.TouchPhase.Ended:
                        if(touch.touchId == _moveTouchId)
                            _moveTouchId = -1;
                        else if(touch.touchId == _aimTouchId)
                            _aimTouchId = -1;

                        break;
                }
            }
        }

        /// <summary>
        /// Fires the relevant command based on the touch ID.
        /// </summary>
        /// <param name="touch">The touch to fire a command for.</param>
        private void FireCommand(ETouch touch)
        {
            if (touch.touchId == _moveTouchId)
                GameCommandBus.Publish(new MoveCommandContext(touch.screenPosition));
            else if (touch.touchId == _aimTouchId)
                GameCommandBus.Publish(new AimCommandContext(touch.screenPosition, isScreenSpace: true));
        }

        #endregion
    }
}