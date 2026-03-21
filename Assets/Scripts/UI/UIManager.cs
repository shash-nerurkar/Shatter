using System;
using Scripts.UI.GameUI;
using Scripts.UI.MenuUI;
using Scripts.UI.OverlayUI;
using Scripts.UI.SystemUI;
using Scripts.UI.WorldUI;
using UnityEngine;

namespace Scripts.UI
{
    /// <summary>
    /// The central manager for the app's UI.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        #region Actions

        public static event Action<UIScreenType> SetUIScreenInput;

        #endregion


        #region Fields

        [SerializeField] private WorldUIHandler worldUIHandler;

        [SerializeField] private GameUIHandler gameUIHandler;

        [SerializeField] private MenuUIHandler menuUIHandler;

        [SerializeField] private OverlayUIHandler overlayUIHandler;

        [SerializeField] private SystemUIHandler systemUIHandler;

        #endregion Fields


        #region Methods
        
        private void Awake()
        {
            Game.Instance.ShowSplashScreen += systemUIHandler.ShowSplash;
            Game.Instance.StartDummyLevel += OnLevelStart;
        }

        private void OnDestroy()
        {
            SetUIScreenInput = null;

            Game.Instance.ShowSplashScreen -= systemUIHandler.ShowSplash;
            Game.Instance.StartDummyLevel -= OnLevelStart;
        }

        private void OnLevelStart()
        {
            SetUIScreenInput?.Invoke(UIScreenType.GameBoard);
        }

        #endregion
    }

    [Serializable]
    public enum UIScreenType
    {
        Splash = 0,
        MainMenu = 1,
        GameBoard = 2
    }
}