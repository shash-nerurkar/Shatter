using System.Collections.Generic;
using Scripts.Input.Handlers;
using Scripts.Input.Handlers.Board;
using Scripts.UI;
using UnityEngine;

namespace Scripts.Input
{
    public class InputManager : MonoBehaviour
    {
        #region Fields

        private List<IInputHandler> inputHandlers;

        #endregion


        #region Methods

        private void Awake()
        {
            inputHandlers = new List<IInputHandler>();

            UIManager.SetUIScreenInput += InitHandler;
        }

        private void OnDestroy()
        {
            
        }

        public void InitHandler(UIScreenType screenState)
        {
            IInputHandler newHandler = screenState switch
            {
                UIScreenType.GameBoard => gameObject.AddComponent<BoardInputHandler>(),
                _ => null
            };
            newHandler.EnableInput();

            inputHandlers.Add(newHandler);
        }

        #endregion
    }
}