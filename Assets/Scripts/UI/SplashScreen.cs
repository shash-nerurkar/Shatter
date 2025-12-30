using UnityEngine;

namespace Scripts.UI
{
    public class SplashScreen : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float displayDuration = 3.0f;

        #endregion Fields


        #region Methods

        public void Show()
        {
            gameObject.SetActive(true);
            Invoke(nameof(Hide), displayDuration);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        #endregion Methods
    }
}