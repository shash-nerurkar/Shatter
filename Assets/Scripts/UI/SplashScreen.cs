using System;
using UnityEngine;

namespace Scripts.UI
{
    public class SplashScreen : MonoBehaviour
    {
        #region Actions

        private Action _onHidden;

        #endregion


        #region Fields

        [SerializeField] private float displayDuration = 3.0f;

        #endregion Fields


        #region Methods

        public void Show(Action onHidden = null)
        {
            gameObject.SetActive(true);
            Invoke(nameof(Hide), displayDuration);

            _onHidden = onHidden;
        }

        private void Hide()
        {
            gameObject.SetActive(false);

            _onHidden?.Invoke();
            _onHidden = null;
        }

        #endregion Methods
    }
}