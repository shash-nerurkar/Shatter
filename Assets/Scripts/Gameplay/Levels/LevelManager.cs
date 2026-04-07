using System;
using Scripts.Constants;
using Scripts.Contracts;
using Scripts.Entity.Playables;
using Scripts.Gameplay.Board;
using Scripts.Gameplay.Cameras;
using Scripts.Input;
using Scripts.Input.Handlers;
using Scripts.Input.Handlers.Board;
using Scripts.Utilities;
using UnityEngine;

namespace Scripts.Gameplay.Levels
{
    public class LevelManager : MonoBehaviour, IManager
    {
        #region Actions

        public static event Action<BoardData> GenerateBoard;

        public static event Action<CameraData> SetupCamera;

        public static event Action<InputHandlerType, IInputHandlerContext> InitLevelInput;

        public static event Action InitLevelUI;

        public static event Action SpawnPlayer;

        public static event Action StartEnemySpawning;

        public static event Action<PlayerData> OnPlayerDataUpdated;

        public static event Action<LevelProgressData> OnLevelProgressUpdated;
        
        public static event Action DestroyBoard;

        public static event Action DestroyLevelInput;

        public static event Action DestroyLevelUI;

        public static event Action DestroyPlayer;

        #endregion

        #region Fields

        private readonly Vector2 BoardSizeMaxInGameTiles = new (10, 18);

        private readonly Vector2 BoardMaxSafeAreaSizeInTiles = new (5, 5);

        private LevelData _currentLevelData;

        private BoardData _currentBoardData;

        private CameraData _currentCameraData;

        #endregion

        #region Methods

        public void Init()
        {
            
        }

        private void Awake()
        {
            Game.Instance.StartDummyLevel += StartNewLevel;

            BoardManager.OnBoardDataGenerationComplete += OnBoardDataGenerationComplete;

            CameraManager.OnCameraSetupComplete += OnCameraSetupComplete;
        }

        private void OnDestroy()
        {
            Game.Instance.StartDummyLevel -= StartNewLevel;

            BoardManager.OnBoardDataGenerationComplete -= OnBoardDataGenerationComplete;

            CameraManager.OnCameraSetupComplete -= OnCameraSetupComplete;

            GenerateBoard = null;
            SetupCamera = null;
            InitLevelInput = null;
            InitLevelUI = null;
            SpawnPlayer = null;
            StartEnemySpawning = null;
            OnPlayerDataUpdated = null;
            OnLevelProgressUpdated = null;

            DestroyBoard = null;
            DestroyLevelInput = null;
            DestroyLevelUI = null;
            DestroyPlayer = null;
        }

        #region Start Level

        private void StartNewLevel()
        {
            // TODO - This will be rewritten in a level-generation PR
            _currentLevelData = new LevelData(
                boardSizeInWorldUnits: new Vector2(10, 18), 
                boardSafeAreaSizeInWorldUnits: new Vector2(1, 1)
            );
            
            BoardManager boardManager = MiscUtils.InstantiatePrefab<BoardManager>(
                path: FilePaths.BoardManagerPrefab, 
                parent: transform.parent, 
                name: "Board", 
                siblingIndex: transform.GetSiblingIndex() + 1
            );
            boardManager.Init();

            var newBoardData = new BoardData();
            newBoardData.FeedData(_currentLevelData);
            GenerateBoard?.Invoke(newBoardData);
        }

        private void OnBoardDataGenerationComplete(BoardData boardData)
        {
            _currentBoardData = boardData;

            var newCameraData = new CameraData();
            newCameraData.FeedData(_currentLevelData);
            SetupCamera?.Invoke(newCameraData);

            var boardInputHandlerContext = new BoardInputHandlerContext(
                boardSizeInTiles: _currentBoardData.BoardSizeInTiles, 
                tileSizeInWorldUnits: _currentBoardData.TileSizeInWorldUnits, 
                areaHeightsInTiles: _currentBoardData.AreaHeightsInTiles,
                safeAreaSizeInWorldUnits: _currentLevelData.BoardSafeAreaSizeInWorldUnits
            );
            InitLevelInput?.Invoke(InputHandlerType.Board, boardInputHandlerContext);
            
            InitLevelUI?.Invoke();

            SpawnPlayer?.Invoke();

            StartEnemySpawning?.Invoke();
        }

        private void OnCameraSetupComplete(CameraData cameraData)
        {
            _currentCameraData = cameraData;
        }

        private void OnPlayerSpawnDataGenerationComplete(PlayerData playerData)
        {
            OnPlayerDataUpdated?.Invoke(playerData);
        }

        // TODO - This will be wired up in an enemy-spawning PR
        private void OnEnemySpawnDataGenerationComplete()
        {
            LevelProgressData levelProgressData = new (maxProgress: 0, currentProgress: 0);

            OnLevelProgressUpdated?.Invoke(levelProgressData);
        }

        #endregion

        #region End Level

        private void OnAllEnemiesSpawnedAndDestroyed()
        {
            EndLevel();
        }

        private void EndLevel()
        {
            DestroyBoard?.Invoke();
            
            DestroyLevelInput?.Invoke();
            
            DestroyLevelUI?.Invoke();
            
            DestroyPlayer?.Invoke();
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// The collective data for a single level.
    /// </summary>
    public class LevelData
    {
        /// <summary>
        /// The size of the board in tiles.
        /// </summary>
        public Vector2 BoardSizeInWorldUnits { get; }
        
        /// <summary>
        /// The size of the board's safe area in tiles.
        /// </summary>
        public Vector2 BoardSafeAreaSizeInWorldUnits { get; }

        public LevelData(Vector2 boardSizeInWorldUnits, Vector2 boardSafeAreaSizeInWorldUnits)
        {
            BoardSizeInWorldUnits = boardSizeInWorldUnits;
            BoardSafeAreaSizeInWorldUnits = boardSafeAreaSizeInWorldUnits;
        }
    }

    /// <summary>
    /// Data used to represent progress made in the level so far.
    /// </summary>
    public class LevelProgressData
    {
        /// <summary>
        /// The maximum level progress.
        /// </summary>
        public int MaxProgress { get; private set; }

        /// <summary>
        /// The current level progress.
        /// </summary>
        public int CurrentProgress { get; private set; }

        public LevelProgressData(int currentProgress, int maxProgress)
        {
            CurrentProgress = currentProgress;
            MaxProgress = maxProgress;
        }
    }
}