using System;
using Scripts.Constants;
using UnityEngine;
using Scripts.Utilities;
using Scripts.UI;
using Scripts.Gameplay.Cameras;
using Scripts.Input;
using Scripts.Gameplay.Levels;

namespace Scripts
{
    /// <summary>
    /// The main class. Used from the very startup of the app.
    /// </summary>
    public class Game : MonoBehaviour
    {
        #region Actions
    
        public event Action<Action> ShowSplashScreen;

        public event Action StartDummyLevel;

        #endregion

        #region Fields

        public static Game Instance { get; private set; }

        public Camera MainCamera { get; private set; }

        #endregion Fields

        #region Methods

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            
            Screen.orientation = ScreenOrientation.Portrait;
            Screen.autorotateToPortrait          = true;
            Screen.autorotateToPortraitUpsideDown = false;
            Screen.autorotateToLandscapeLeft     = false;
            Screen.autorotateToLandscapeRight    = false;
            
            LoadCentralManagers();
            
            CameraManager.UpdateMainCamera += UpdateMainCamera;
        }

        private void OnDestroy()
        {
            CameraManager.UpdateMainCamera -= UpdateMainCamera;

            ShowSplashScreen = null;
            StartDummyLevel = null;
        }

        private void Start()
        {
            ShowSplashScreen?.Invoke(
                // TODO - This will be rewritten in a UI-screens PR
                StartDummyLevel
            );
        }

        /// <summary>
        /// Loads all central managers for the app.
        /// </summary>
        private void LoadCentralManagers()
        {
            UIManager uiManager = MiscUtils.InstantiatePrefab<UIManager>(
                path: FilePaths.UIManagerPrefab, 
                parent: transform.parent, 
                name: "UI", 
                position: new Vector3(Screen.width, Screen.height, 0) / 2,
                rotation: Quaternion.identity,
                siblingIndex: transform.GetSiblingIndex() + 1
            );
            uiManager.Init();

            MiscUtils.InstantiateEmpty(transform.parent, Strings.SpacingObjectName, siblingIndex: transform.GetSiblingIndex() + 1);
            
            CameraManager cameraManager = MiscUtils.InstantiatePrefab<CameraManager>(
                path: FilePaths.CameraManagerPrefab, 
                parent: transform.parent, 
                name: "Cameras", 
                siblingIndex: transform.GetSiblingIndex() + 1
            );
            cameraManager.Init();

            MiscUtils.InstantiateEmpty(transform.parent, Strings.SpacingObjectName, siblingIndex: transform.GetSiblingIndex() + 1);
            
            InputManager inputManager = MiscUtils.InstantiatePrefab<InputManager>(
                path: FilePaths.InputManagerPrefab, 
                parent: transform.parent, 
                name: "Input", 
                siblingIndex: transform.GetSiblingIndex() + 1
            );
            inputManager.Init();

            MiscUtils.InstantiateEmpty(transform.parent, Strings.SpacingObjectName, siblingIndex: transform.GetSiblingIndex() + 1);

            LevelManager levelManager = MiscUtils.InstantiatePrefab<LevelManager>(
                path: FilePaths.LevelManagerPrefab, 
                parent: transform.parent, 
                name: "Levels", 
                siblingIndex: transform.GetSiblingIndex() + 1
            );
            levelManager.Init();

            MiscUtils.InstantiateEmpty(transform.parent, Strings.SpacingObjectName, siblingIndex: transform.GetSiblingIndex() + 1);
        }

        private void UpdateMainCamera(Camera camera) => MainCamera = camera;

        #endregion Methods
    }
}