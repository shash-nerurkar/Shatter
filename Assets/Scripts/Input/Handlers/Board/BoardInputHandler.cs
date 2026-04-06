using System;
using System.Collections.Generic;
using Scripts.Gameplay.Board;
using Scripts.Gameplay.Commands;
using Scripts.Gameplay.Commands.Board;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Scripts.Input.Handlers.Board
{
    /// <summary>
    /// Manages user input for the game board.
    /// </summary>
    public class BoardInputHandler : MonoBehaviour, IInputHandler
    {
        #region Fields

        private int _aimTouchId = -1;

        private int _moveTouchId = -1;

        private BoardInputHandlerContext _handlerContext;

        #endregion

        #region Methods

        public void Enable()
        {
            EnhancedTouchSupport.Enable();
        }

        public void Disable()
        {
            EnhancedTouchSupport.Disable();
        }

        public void Init(IInputHandlerContext context)
        {
            if (context is not BoardInputHandlerContext boardContext)
                return;

            _handlerContext = boardContext;
        }

        public void Dispose() {}

        public void Poll()
        {
            foreach (ETouch touch in ETouch.activeTouches)
            {
                switch (touch.phase)
                {
                    case UnityEngine.InputSystem.TouchPhase.Began:
                        var areaClicked = _handlerContext.GetClickedAreaType(
                            Game.Instance.MainCamera.ScreenToWorldPoint(
                                new Vector3(
                                    touch.screenPosition.x, 
                                    touch.screenPosition.y, 
                                    Game.Instance.MainCamera.nearClipPlane
                                )
                            ) 
                        );

                        switch (areaClicked)
                        {
                            case BoardClickableAreaType.PlayerMove:
                                _moveTouchId = touch.touchId;
                                break;

                            case BoardClickableAreaType.PlayerAim:
                                _aimTouchId = touch.touchId;
                                break;
                            
                            default:
                                return;
                        }
                        
                        PublishCommand(touch);

                        break;

                    case UnityEngine.InputSystem.TouchPhase.Moved:
                        PublishCommand(touch);

                        break;

                    case UnityEngine.InputSystem.TouchPhase.Ended:
                        if(touch.touchId == _moveTouchId)
                            _moveTouchId = -1;
                        else if(touch.touchId == _aimTouchId)
                            _aimTouchId = -1;

                        break;
                }
            }
        }

        /// <summary>
        /// Publishes the relevant command based on the touch ID.
        /// </summary>
        /// <param name="touch">The touch to fire a command for.</param>
        private void PublishCommand(ETouch touch)
        {
            if (touch.touchId == _moveTouchId)
            {
                GameCommandBus.Publish(new MoveCommandContext(touch.screenPosition));

                Debug.Log($"Move: {touch.screenPosition}");
            }
            else if (touch.touchId == _aimTouchId)
            {
                GameCommandBus.Publish(new AimCommandContext(touch.screenPosition, isScreenSpace: true));

                Debug.Log($"Aim: {touch.screenPosition}");
            }
                
        }

        #endregion
    }

    /// <summary>
    /// Represents the context passed into a <see cref="BoardInputHandler"/>.
    /// </summary>
    public class BoardInputHandlerContext : IInputHandlerContext
    {
        /// <summary>
        /// The top-right screen positions of each <see cref="BoardClickableAreaType"/>.
        /// </summary>
        private readonly Dictionary<BoardClickableAreaType, Vector2> _boardClickableAreasTopRightWorldPositions;

        public BoardInputHandlerContext(Vector2Int boardSizeInTiles, Vector2 tileSizeInWorldUnits, Dictionary<BoardAreaType, int> areaHeightsInTiles, Vector2 safeAreaSizeInWorldUnits)
        {
            _boardClickableAreasTopRightWorldPositions = new Dictionary<BoardClickableAreaType, Vector2>();

            var cameraOffset = new Vector2(
                Game.Instance.MainCamera.orthographicSize * Game.Instance.MainCamera.aspect, 
                Game.Instance.MainCamera.orthographicSize
            );

            var boardWidthInWorldUnits = boardSizeInTiles.x * tileSizeInWorldUnits.x;
            var prevBoardAreaTopWorldPositionY = 0f;
            for (BoardAreaType boardAreaType = BoardAreaType.BottomNoMansLand; boardAreaType < BoardAreaType.TopNoMansLand + 1; boardAreaType++)
            {
                var boardAreaTopWorldPositionOffset = new Vector2(
                    boardWidthInWorldUnits + (safeAreaSizeInWorldUnits.x / 2),
                    (areaHeightsInTiles[boardAreaType] * tileSizeInWorldUnits.y) + prevBoardAreaTopWorldPositionY + (safeAreaSizeInWorldUnits.y / 2)
                );

                KeyValuePair<BoardClickableAreaType, Vector2> kvp = new(
                    boardAreaType switch
                    {
                        BoardAreaType.BottomNoMansLand => BoardClickableAreaType.PlayerMove,
                        BoardAreaType.PlayerSpawn => BoardClickableAreaType.PlayerMove,
                        BoardAreaType.BattleArena => BoardClickableAreaType.PlayerAim,
                        BoardAreaType.EnemySpawn => BoardClickableAreaType.PlayerAim,
                        BoardAreaType.TopNoMansLand => BoardClickableAreaType.PlayerAim,
                        _ => throw new ArgumentOutOfRangeException(nameof(boardAreaType), boardAreaType, null)
                    },
                    boardAreaTopWorldPositionOffset - cameraOffset
                );

                if(_boardClickableAreasTopRightWorldPositions.ContainsKey(kvp.Key))
                    _boardClickableAreasTopRightWorldPositions[kvp.Key] = kvp.Value;
                else
                    _boardClickableAreasTopRightWorldPositions.Add(kvp.Key, kvp.Value);

                prevBoardAreaTopWorldPositionY = boardAreaTopWorldPositionOffset.y;
            }
        }

        /// <summary>
        /// Returns the <see cref="BoardClickableAreaType"/> that was clicked at the given world position.
        /// </summary>
        /// <param name="touchWorldPosition">The world position to check.</param>
        /// <returns></returns>
        public BoardClickableAreaType GetClickedAreaType(Vector3 touchWorldPosition)
        {
            if(_boardClickableAreasTopRightWorldPositions.ContainsKey(BoardClickableAreaType.PlayerMove))
            {
                if(touchWorldPosition.y > _boardClickableAreasTopRightWorldPositions[BoardClickableAreaType.PlayerMove].y)
                    return BoardClickableAreaType.PlayerAim;
                else
                    return BoardClickableAreaType.PlayerMove;
            }
            else if(_boardClickableAreasTopRightWorldPositions.ContainsKey(BoardClickableAreaType.PlayerAim))
            {
                return BoardClickableAreaType.PlayerAim;
            }
            else
                return BoardClickableAreaType.None;
        }
    }

    /// <summary>
    /// Represents the types of clickable areas on the board.
    /// </summary>
    public enum BoardClickableAreaType
    {
        None = 0,
        PlayerMove = 1,
        PlayerAim = 2
    }
}