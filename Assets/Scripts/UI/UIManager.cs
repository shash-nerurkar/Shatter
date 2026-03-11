using System;
using UnityEngine;

namespace Scripts.UI
{
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
            Game.Instance.ShowSplashScreen += ShowSplash;

            _splashScreen = Instantiate(
                Resources.Load<SplashScreen>(Constants.FilePaths.SplashScreenPrefab),
                transform
            );
        }

        private void OnDestroy()
        {
            SetUIScreenInput = null;

            Game.Instance.ShowSplashScreen -= ShowSplash;
        }

        private void ShowSplash() => ShowScreen(UIScreenType.Splash);

        private void ShowScreen(UIScreenType newScreenState)
        {
            switch(newScreenState)
            {
                case UIScreenType.Splash:
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