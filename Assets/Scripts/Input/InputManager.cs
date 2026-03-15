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

        public void InitHandler(UIScreenType screenState)
        {
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

        public void DestroyHandler(UIScreenType screenState)
        {
            Type inputHandlerType = screenState switch
            {
                UIScreenType.GameBoard => typeof(BoardInputHandler),
                _ => null
            };
            if(inputHandlerType == null)
                return;

            IInputHandler handlerToDestroy = inputHandlers.FirstOrDefault(handler => handler.GetType() == inputHandlerType);
            if(handlerToDestroy == null)
                return;
            
            handlerToDestroy.Dispose();
            handlerToDestroy.Disable();

            Destroy(screenState switch
            {
                UIScreenType.GameBoard => handlerToDestroy as BoardInputHandler,
                _ => null
            });

            inputHandlers.Remove(handlerToDestroy);
        }

        #endregion
    }
}