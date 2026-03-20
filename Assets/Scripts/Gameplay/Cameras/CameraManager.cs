using UnityEngine;

namespace Scripts.Gameplay.Cameras
{
    public class CameraManager : MonoBehaviour
    {
        #region Fields
        
        [SerializeField] private Camera mainCamera;
        
        #endregion

        
        #region Methods
        
        private void Awake()
        {
            Game.Instance.StartDummyLevel += OnDummyLevelStart;
        }

        private void OnDestroy()
        {
            Game.Instance.StartDummyLevel -= OnDummyLevelStart;
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