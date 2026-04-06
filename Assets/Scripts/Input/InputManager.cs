using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Contracts;
using Scripts.Gameplay.Levels;
using Scripts.Input.Handlers;
using Scripts.Input.Handlers.Board;
using Scripts.UI;
using UnityEngine;

namespace Scripts.Input
{
    public class InputManager : MonoBehaviour, IManager
    {
        #region Fields

        private List<IInputHandler> inputHandlers;

        #endregion

        #region Methods

        public void Init() {}

        private void Awake()
        {
            inputHandlers = new List<IInputHandler>();

            LevelManager.InitLevelInput += InitHandler;
        }

        private void OnDestroy()
        {
            LevelManager.InitLevelInput -= InitHandler;
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
        public void InitHandler(InputHandlerType handlerType, IInputHandlerContext context)
        {
            if(TryGetHandler(handlerType) != null)
                return;

            IInputHandler newHandler = handlerType switch
            {
                InputHandlerType.Board => gameObject.AddComponent<BoardInputHandler>(),
                _ => null
            };
            if (newHandler == null)
                return;

            newHandler.Init(context);
            newHandler.Enable();

            inputHandlers.Add(newHandler);
        }

        /// <summary>
        /// Destroys the input handler associated with the given screen state.
        /// </summary>
        /// <param name="screenState">The screen state associated with the input handler to destroy.</param>
        public void DestroyHandler(InputHandlerType handlerType)
        {
            IInputHandler handlerToDestroy = TryGetHandler(handlerType);
            if(handlerToDestroy == null)
                return;
            
            handlerToDestroy.Disable();
            handlerToDestroy.Dispose();

            Destroy(handlerType switch
            {
                InputHandlerType.Board => handlerToDestroy as BoardInputHandler,
                _ => null
            });

            inputHandlers.Remove(handlerToDestroy);
        }

        /// <summary>
        /// Attempts to retrieve an input handler associated with the given screen state.
        /// </summary>
        /// <param name="screenState">The screen state to retrieve an input handler for.</param>
        /// <returns>An input handler associated with the given screen state, if one exists; otherwise, null.</returns>
        private IInputHandler TryGetHandler(InputHandlerType handlerType)
        {
            Type inputHandlerType = handlerType switch
            {
                InputHandlerType.Board => typeof(BoardInputHandler),
                _ => null
            };
            if(inputHandlerType == null)
                return null;

            return inputHandlers.FirstOrDefault(handler => handler.GetType() == inputHandlerType);
        }

        #endregion
    }

    /// <summary>
    /// The type of input handler to create.
    /// </summary>
    public enum InputHandlerType
    {
        Board = 0
    }
}