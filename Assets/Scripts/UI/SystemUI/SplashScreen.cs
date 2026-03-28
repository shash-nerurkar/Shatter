using System;
using UnityEngine;

namespace Scripts.UI
{
    /// <summary>
    /// Manages the splash screen UI.
    /// </summary>
    public class SplashScreen : MonoBehaviour, IScreen
    {
        #region Actions

        private Action _onHidden;

        #endregion


        #region Fields

        [SerializeField] private float displayDuration = 3.0f;

        #endregion Fields


        #region Methods

        /// <summary>
        /// Shows the splash screen, and hides it after the specified duration.
        /// </summary>
        public void Show(Action onHidden = null)
        {
            gameObject.SetActive(true);
            
            CancelInvoke(nameof(Hide));
            Invoke(nameof(Hide), displayDuration);

            _onHidden = onHidden;
        }


        /// <summary>
        /// Hides the splash screen.
        /// </summary>
        public void Hide()
        {
            gameObject.SetActive(false);

            Action onHidden = _onHidden;
            onHidden?.Invoke();
            _onHidden = null;
        }

        #endregion Methods
    }
}