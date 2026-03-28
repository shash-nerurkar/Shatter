using UnityEngine;

namespace Scripts.UI.WorldUI.Enemy.Health
{
    public class EnemyHealthBar : BaseProgressBar
    {
        #region Fields

        private RectTransform RectTransform
        {
            get => transform as RectTransform;
        }

        #endregion

        #region Methods

        public void RePosition(Vector3 newPosition)
        {
            RectTransform.anchoredPosition = newPosition;
        }

        #endregion
    }
}