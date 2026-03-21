using Scripts.Gameplay.Cameras;
using UnityEngine;

namespace Scripts.UI.WorldUI
{
    public class WorldUIHandler : MonoBehaviour, IUIHandler
    {
        #region Fields

        [SerializeField] private Canvas canvas;

        #endregion

        #region Methods

        private void Awake()
        {
            CameraManager.UpdateMainCamera += OnMainCameraChanged;
        }

        private void OnDestroy()
        {
            CameraManager.UpdateMainCamera -= OnMainCameraChanged;
        }

        private void OnMainCameraChanged(Camera mainCamera) => canvas.worldCamera = mainCamera;

        #endregion
    }
}