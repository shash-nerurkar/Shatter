using Scripts.Constants;
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
            CreateNewBoardHud();
        }

        /// <summary>
        /// Instantiates a new <see cref="BoardHUD"/> and initializes it.
        /// </summary>
        private void CreateNewBoardHud()
        {
            _boardHUD = MiscUtils.InstantiatePrefab<BoardHUD>(
                path: FilePaths.BoardHudPrefab, 
                parent: transform, 
                name: "Board HUD"
            );

            if(_boardHUD != null)
                _boardHUD.Initialize();
        }

        #endregion
    }
}