using System;
using Scripts.Constants;
using UnityEngine;
using Scripts.Utilities;

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

        // TODO - This will be moved elsewhere in a level-setup PR
        public readonly Vector2 BoardSizeMaxInGameTiles = new (10, 18);
        public readonly int BoardSafeAreaYInTiles = 1;

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
            ShowSplashScreen?.Invoke(
                // TODO - This will be moved elsewhere in a level-setup PR
                StartDummyLevel
            );
        }

        /// <summary>
        /// Loads all central managers for the app.
        /// </summary>
        private void LoadCentralManagers()
        {
            MiscUtils.InstantiatePrefab<GameObject>(
                path: FilePaths.UIManagerPrefab, 
                parent: transform.parent, 
                name: "UI", 
                siblingIndex: transform.GetSiblingIndex() + 1
            );

            MiscUtils.InstantiateEmpty(transform.parent, "----------------------------", siblingIndex: transform.GetSiblingIndex() + 1);
            
            MiscUtils.InstantiatePrefab<GameObject>(
                path: FilePaths.CameraManagerPrefab, 
                parent: transform.parent, 
                name: "Cameras", 
                siblingIndex: transform.GetSiblingIndex() + 1
            );

            MiscUtils.InstantiateEmpty(transform.parent, "----------------------------", siblingIndex: transform.GetSiblingIndex() + 1);
            
            MiscUtils.InstantiatePrefab<GameObject>(
                path: FilePaths.InputManagerPrefab, 
                parent: transform.parent, 
                name: "Input", 
                siblingIndex: transform.GetSiblingIndex() + 1
            );
            
            // TODO - This will be moved elsewhere in a level-setup PR
            MiscUtils.InstantiatePrefab<GameObject>(
                path: FilePaths.BoardManagerPrefab, 
                parent: transform.parent, 
                name: "Board", 
                siblingIndex: transform.GetSiblingIndex() + 1
            );
        }

        #endregion Methods
    }
}