using Scripts.Contracts;
using Scripts.Gameplay.Cameras;
using Scripts.Gameplay.Levels;
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
    public class UIManager : MonoBehaviour, IManager
    {
        #region Fields

        [SerializeField] private WorldUIHandler worldUIHandler;

        [SerializeField] private GameUIHandler gameUIHandler;

        [SerializeField] private MenuUIHandler menuUIHandler;

        [SerializeField] private OverlayUIHandler overlayUIHandler;

        [SerializeField] private SystemUIHandler systemUIHandler;

        #endregion Fields

        #region Methods

        public void Init()
        {
            worldUIHandler.RectTransform.anchoredPosition = -transform.position;
        }
        
        private void Awake()
        {
            Game.Instance.ShowSplashScreen += systemUIHandler.ShowSplash;

            CameraManager.UpdateMainCamera += worldUIHandler.OnMainCameraChanged;
            
            LevelManager.InitLevelUI += InitLevelUI;
            LevelManager.OnPlayerDataUpdated += gameUIHandler.SetupPlayerHealthBar;
            LevelManager.OnLevelProgressUpdated += gameUIHandler.SetupLevelProgressBar;
        }

        private void OnDestroy()
        {
            Game.Instance.ShowSplashScreen -= systemUIHandler.ShowSplash;

            CameraManager.UpdateMainCamera -= worldUIHandler.OnMainCameraChanged;
            
            LevelManager.InitLevelUI -= InitLevelUI;
            LevelManager.OnPlayerDataUpdated -= gameUIHandler.SetupPlayerHealthBar;
            LevelManager.OnLevelProgressUpdated -= gameUIHandler.SetupLevelProgressBar;
        }

        private void InitLevelUI()
        {
            gameUIHandler.OnLevelStart();
            worldUIHandler.OnLevelStart();
        }

        #endregion
    }
}