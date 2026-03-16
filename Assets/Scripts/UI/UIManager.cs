using UnityEngine;

namespace Scripts.UI
{
    /// <summary>
    /// The central manager for the app's UI.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
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

        /// <summary>
        /// Shows the splash screen.
        /// </summary>
        private void ShowSplash()
        {
            if(_splashScreen == null)
                return;

            _splashScreen.Show();
        }

        #endregion Methods
    }
}