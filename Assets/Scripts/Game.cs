using System;
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

        public static Game Instance { get; private set; }

        [SerializeField] private GameObject UIManagerObject;

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
        }

        #endregion Methods
    }
}