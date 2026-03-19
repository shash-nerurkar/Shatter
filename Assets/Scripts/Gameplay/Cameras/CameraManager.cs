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
                    Game.Instance.BoardSafeAreaYInTiles
                )
            );
        }

        private void OnLevelStart(CameraData cameraData)
        {
            mainCamera.orthographic = true;
            mainCamera.orthographicSize = (cameraData.BoardSizeInTiles.y + cameraData.BoardSafeAreaYInTiles) / 2;
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
        /// The safe area of the board height in tiles.
        /// </summary>
        public int BoardSafeAreaYInTiles { get; }

        public CameraData(Vector2 boardSizeInTiles, int boardSafeAreaYInTiles)
        {
            BoardSizeInTiles = boardSizeInTiles;
            BoardSafeAreaYInTiles = boardSafeAreaYInTiles;
        }
    }
}