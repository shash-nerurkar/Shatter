using UnityEngine;

namespace Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
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
            Game.Instance.ShowSplashScreen -= ShowSplash;
        }

        private void ShowSplash() => _splashScreen.Show();

        #endregion Methods
    }
}