using Scripts.Constants;
using Scripts.Entity.Playables;
using Scripts.Gameplay.Levels;
using Scripts.Utilities;
using UnityEngine;

namespace Scripts.UI.GameUI
{
    public class GameUIHandler : MonoBehaviour, IUIHandler
    {
        #region Fields

        private BoardHUD _boardHUD;

        #endregion
        
        #region Methods

        public void OnLevelStart()
        {
            _boardHUD = MiscUtils.InstantiatePrefab<BoardHUD>(
                path: FilePaths.BoardHudPrefab, 
                parent: transform, 
                name: "Board HUD"
            );
        }

        public void SetupPlayerHealthBar(PlayerData playerData) => _boardHUD.SetupPlayerHealthBar(playerData);

        public void SetupLevelProgressBar(LevelProgressData levelProgressData) => _boardHUD.SetupLevelProgressBar(levelProgressData);

        public void OnLevelEnd()
        {
            if(_boardHUD != null)
                Destroy(_boardHUD.gameObject);
        }

        #endregion
    }
}