using UnityEngine;

namespace Scripts.UI
{
    /// <summary>
    /// Manages the splash screen UI.
    /// </summary>
    public class SplashScreen : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float displayDuration = 3.0f;

        #endregion Fields


        #region Methods

        /// <summary>
        /// Shows the splash screen, and hides it after the specified duration.
        /// </summary>
        public void Show()
        {
            gameObject.SetActive(true);
            
            CancelInvoke(nameof(Hide));
            Invoke(nameof(Hide), displayDuration);
        }


        /// <summary>
        /// Hides the splash screen.
        /// </summary>
        private void Hide()
        {
            gameObject.SetActive(false);
        }

        #endregion Methods
    }
}