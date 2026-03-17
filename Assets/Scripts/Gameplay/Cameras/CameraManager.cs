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

        private void OnDummyLevelStart()
        {
            mainCamera.orthographic = true;
            mainCamera.orthographicSize = Game.Instance.BoardSizeMaxInGameTiles.y / 2;
        }

        #endregion
    }
}