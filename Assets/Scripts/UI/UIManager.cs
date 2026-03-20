using System;
using Scripts.Constants;
using Scripts.Utilities;
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

        private SplashScreen _splashScreen;

        #endregion Fields


        #region Methods
        
        private void Awake()
        {
            LoadSplashScreen();

            Game.Instance.ShowSplashScreen += ShowSplash;
            Game.Instance.StartDummyLevel += OnDummyLevelStart;
        }

        private void OnDestroy()
        {
            SetUIScreenInput = null;

            Game.Instance.ShowSplashScreen -= ShowSplash;
            Game.Instance.StartDummyLevel -= OnDummyLevelStart;
        }

        /// <summary>
        /// Loads and instantiates the splash screen.
        /// </summary>
        private void LoadSplashScreen()
        {
            _splashScreen = MiscUtils.InstantiatePrefab<SplashScreen>(
                path: FilePaths.SplashScreenPrefab, 
                parent: transform, 
                name: "Splash Screen"
            );
        }
        
        private void ShowSplash(Action onHidden) => _splashScreen.Show(onHidden);

        private void OnDummyLevelStart() => SetUIScreenInput?.Invoke(UIScreenType.GameBoard);

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