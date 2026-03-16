using System;
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
        }

        private void OnDestroy()
        {
            SetUIScreenInput = null;

            Game.Instance.ShowSplashScreen -= ShowSplash;
        }

        /// <summary>
        /// Loads and instantiates the splash screen.
        /// </summary>
        private void LoadSplashScreen()
        {
            SplashScreen splashScreenPrefab = Resources.Load<SplashScreen>(Constants.FilePaths.SplashScreenPrefab);
            if(splashScreenPrefab == null)
                return;
            
            _splashScreen = Instantiate(splashScreenPrefab, transform);
        }
        
        private void ShowSplash() => ShowScreen(UIScreenType.Splash);

        private void ShowScreen(UIScreenType newScreenState)
        {
            switch(newScreenState)
            {
                case UIScreenType.Splash:
                    if(_splashScreen == null)
                        return;

                    // TODO - The logic to show the game board is temporary, it will be removed later
                    _splashScreen.Show(onHidden: () => ShowScreen(UIScreenType.GameBoard));
                    break;
                    
                case UIScreenType.GameBoard:
                    SetUIScreenInput?.Invoke(newScreenState);

                    break;
            }
        }

        #endregion Methods
    }

    [Serializable]
    public enum UIScreenType
    {
        Splash = 0,
        MainMenu = 1,
        GameBoard = 2
    }
}