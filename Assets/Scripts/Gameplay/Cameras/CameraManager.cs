using System;
using Scripts.Contracts;
using Scripts.Gameplay.Levels;
using UnityEngine;

namespace Scripts.Gameplay.Cameras
{
    public class CameraManager : MonoBehaviour, IManager
    {
        #region Actions

        public static event Action<Camera> UpdateMainCamera;

        public static event Action<CameraData> OnCameraSetupComplete;

        #endregion

        #region Fields
        
        [SerializeField] private Camera mainCamera;
        
        #endregion
        
        #region Methods

        public void Init() {}
        
        private void Awake()
        {
            LevelManager.SetupCamera += SetupLevelCamera;
        }

        private void OnDestroy()
        {
            LevelManager.SetupCamera -= SetupLevelCamera;
            
            UpdateMainCamera = null;
            OnCameraSetupComplete = null;
        }

        private void Start()
        {
            UpdateMainCamera?.Invoke(mainCamera);
        }

        private void SetupLevelCamera(CameraData cameraData)
        {
            mainCamera.orthographic = true;

            float requiredHalfHeight = (cameraData.BoardSizeInWorldUnits.y + cameraData.BoardSafeAreaSizeInWorldUnits.y) / 2f;
            float requiredHalfWidthAsHeight = (cameraData.BoardSizeInWorldUnits.x  + cameraData.BoardSafeAreaSizeInWorldUnits.y) / (2f * mainCamera.aspect);
            mainCamera.orthographicSize = Mathf.Max(requiredHalfHeight, requiredHalfWidthAsHeight);

            OnCameraSetupComplete?.Invoke(cameraData);
        }

        #endregion
    }

    /// <summary>
    /// Represents a data structure for camera setup.
    /// </summary>
    public class CameraData
    {
        /// <summary>
        /// The size of the board in tiles.
        /// </summary>
        public Vector2 BoardSizeInWorldUnits { get; private set; }

        /// <summary>
        /// The size of the board's safe area in tiles.
        /// </summary>
        public Vector2 BoardSafeAreaSizeInWorldUnits { get; private set; }

        public CameraData() {}

        public void FeedData(LevelData levelData)
        {
            BoardSizeInWorldUnits = levelData.BoardSizeInWorldUnits;
            BoardSafeAreaSizeInWorldUnits = levelData.BoardSafeAreaSizeInWorldUnits;
        }
    }
}