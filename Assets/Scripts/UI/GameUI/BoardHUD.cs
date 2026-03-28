using UnityEngine;

namespace Scripts.UI.GameUI
{
    public class BoardHUD : MonoBehaviour
    {
        #region Fields

        [SerializeField] private PlayerHealthBar playerHealthBar;
        [SerializeField] private LevelProgressBar levelProgressBar;
        
        #endregion

        #region Methods

        public void Initialize()
        {
            // TODO - This will be rewritten in player init PR
            playerHealthBar.Setup(max: 100, current: 100);
            // TODO - This will be rewritten in level-generation setup PR
            levelProgressBar.Setup(max: 5, current: 5);
        }

        #endregion
    }
}
