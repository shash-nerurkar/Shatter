using System;
using UnityEngine;

namespace Scripts.UI.SystemUI
{
    public class SystemUIHandler : MonoBehaviour, IUIHandler
    {
        #region Fields

        [SerializeField] private SplashScreen splashScreen;

        #endregion

        #region Methods

        public void ShowSplash(Action onHidden) => splashScreen.Show(onHidden);

        #endregion
    }
}