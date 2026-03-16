using System;
using System.Collections.Generic;
using System.Linq;
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
            UIManager.SetUIScreenInput -= InitHandler;
        }

        private void Update()
        {
            foreach (IInputHandler handler in inputHandlers)
                handler.Poll();
        }

        /// <summary>
        /// Initialises a new input handler for the given screen state, if one does not already exist.
        /// </summary>
        /// <param name="screenState">The screen state to initialise an input handler for.</param>
        public void InitHandler(UIScreenType screenState)
        {
            if(TryGetHandler(screenState) != null)
                return;

            IInputHandler newHandler = screenState switch
            {
                UIScreenType.GameBoard => gameObject.AddComponent<BoardInputHandler>(),
                _ => null
            };
            if (newHandler == null)
                return;

            newHandler.Init();
            newHandler.Enable();

            inputHandlers.Add(newHandler);
        }

        /// <summary>
        /// Destroys the input handler associated with the given screen state.
        /// </summary>
        /// <param name="screenState">The screen state associated with the input handler to destroy.</param>
        public void DestroyHandler(UIScreenType screenState)
        {
            IInputHandler handlerToDestroy = TryGetHandler(screenState);
            if(handlerToDestroy == null)
                return;
            
            handlerToDestroy.Disable();
            handlerToDestroy.Dispose();

            Destroy(screenState switch
            {
                UIScreenType.GameBoard => handlerToDestroy as BoardInputHandler,
                _ => null
            });

            inputHandlers.Remove(handlerToDestroy);
        }

        /// <summary>
        /// Attempts to retrieve an input handler associated with the given screen state.
        /// </summary>
        /// <param name="screenState">The screen state to retrieve an input handler for.</param>
        /// <returns>An input handler associated with the given screen state, if one exists; otherwise, null.</returns>
        private IInputHandler TryGetHandler(UIScreenType screenState)
        {
            Type inputHandlerType = screenState switch
            {
                UIScreenType.GameBoard => typeof(BoardInputHandler),
                _ => null
            };
            if(inputHandlerType == null)
                return null;

            return inputHandlers.FirstOrDefault(handler => handler.GetType() == inputHandlerType);
        }

        #endregion
    }
}