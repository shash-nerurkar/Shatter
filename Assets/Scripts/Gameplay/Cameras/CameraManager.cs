using System;
using Scripts.Contracts;
using UnityEngine;

namespace Scripts.Gameplay.Cameras
{
    public class CameraManager : MonoBehaviour, IManager
    {
        #region Actions

        public static event Action<Camera> UpdateMainCamera;

        #endregion

        #region Fields
        
        [SerializeField] private Camera mainCamera;
        
        #endregion
        
        #region Methods

        public void Init() {}
        
        private void Awake()
        {
            Game.Instance.StartDummyLevel += OnDummyLevelStart;
        }

        private void OnDestroy()
        {
            UpdateMainCamera = null;

            Game.Instance.StartDummyLevel -= OnDummyLevelStart;
        }

        private void Start()
        {
            UpdateMainCamera?.Invoke(mainCamera);
        }

        // TODO - Will be removed in level-setup PR
        private void OnDummyLevelStart()
        {
            OnLevelStart(
                new CameraData(
                    Game.Instance.BoardSizeMaxInGameTiles, 
                    Game.Instance.BoardSafeAreaSizeInTiles
                )
            );
        }

        private void OnLevelStart(CameraData cameraData)
        {
            mainCamera.orthographic = true;

            float requiredHalfHeight = (cameraData.BoardSizeInTiles.y + cameraData.BoardSafeAreaSizeInTiles.y) / 2f;
            float requiredHalfWidthAsHeight = (cameraData.BoardSizeInTiles.x  + cameraData.BoardSafeAreaSizeInTiles.y) / (2f * mainCamera.aspect);
            mainCamera.orthographicSize = Mathf.Max(requiredHalfHeight, requiredHalfWidthAsHeight);
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
        public Vector2 BoardSizeInTiles { get; }

        /// <summary>
        /// The size of the board's safe area in tiles.
        /// </summary>
        public Vector2 BoardSafeAreaSizeInTiles { get; }

        public CameraData(Vector2 boardSizeInTiles, Vector2 boardSafeAreaSizeInTiles)
        {
            BoardSizeInTiles = boardSizeInTiles;
            BoardSafeAreaSizeInTiles = boardSafeAreaSizeInTiles;
        }
    }
}