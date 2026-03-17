using System;
using Scripts.Gameplay.Board;
using Scripts.Gameplay.Cameras;
using Scripts.Input;
using Scripts.UI;
using UnityEngine;

namespace Scripts
{
    /// <summary>
    /// The main class. Used from the very startup of the app.
    /// </summary>
    public class Game : MonoBehaviour
    {
        #region Actions
    
        public event Action ShowSplashScreen;

        #endregion


        #region Fields

        [SerializeField] private GameObject UIManagerObject;

        [SerializeField] private GameObject CameraManagerObject;

        public static Game Instance { get; private set; }

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
            
            LoadCentralManagers();
        }

        private void Start()
        {
            ShowSplashScreen?.Invoke();
        }

        /// <summary>
        /// Loads all central managers for the app.
        /// </summary>
        private void LoadCentralManagers()
        {
            UIManagerObject.AddComponent<UIManager>();

            GameObject newGameObject = new("Input Manager");
            newGameObject.transform.SetParent(transform);
            newGameObject.AddComponent<InputManager>();

            newGameObject = new("Board Manager");
            newGameObject.transform.SetParent(transform.parent);
            newGameObject.transform.SetSiblingIndex(gameObject.transform.GetSiblingIndex() + 1);
            newGameObject.AddComponent<BoardManager>();

            CameraManagerObject.AddComponent<CameraManager>();
        }

        #endregion Methods
    }
}