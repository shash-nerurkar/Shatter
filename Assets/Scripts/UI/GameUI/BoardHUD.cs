using Scripts.Entity.Playables;
using Scripts.Gameplay.Levels;
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

        public void SetupLevelProgressBar(LevelProgressData levelProgressData)
        {
            levelProgressBar.Setup(max: levelProgressData.MaxProgress, current: 0);
        }

        public void SetupPlayerHealthBar(PlayerData playerData)
        {
            playerHealthBar.Setup(max: playerData.MaxHealth);
            playerHealthBar.AnimateProgress(playerData.CurrentHealth);
        }

        #endregion
    }
}
