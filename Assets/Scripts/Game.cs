using System;
using Scripts.UI;
using UnityEngine;

namespace Scripts
{
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
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            UIManagerObject.AddComponent<UIManager>();
        }

        private void Start()
        {
            ShowSplashScreen?.Invoke();
        }

        #endregion Methods
    }
}