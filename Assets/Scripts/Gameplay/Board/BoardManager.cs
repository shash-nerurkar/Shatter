using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Scripts.Utilities;
using System.Collections.Generic;
using Scripts.Contracts;
using Scripts.Gameplay.Levels;

namespace Scripts.Gameplay.Board
{
    /// <summary>
    /// Manages a single board instance.
    /// </summary>
    public class BoardManager : MonoBehaviour, IManager
    {
        #region Actions

        public static event Action<BoardData> OnBoardDataGenerationComplete;

        #endregion

        #region Fields
        
        [SerializeField] private Grid grid;

        [SerializeField] private Tilemap tilemap;

        [SerializeField] private SerializableDictionary<BoardAreaType, TileBase> dummyAreaTileBases;

        [SerializeField] private SerializableDictionary<BoardAreaType, int> dummyAreaHeightInTiles;

        private BoardData _boardData;
        
        #endregion
        
        #region Methods

        public void Init() {}
        
        private void Awake()
        {
            LevelManager.GenerateBoard += GenerateBoard;
        }

        private void OnDestroy()
        {
            LevelManager.GenerateBoard -= GenerateBoard;

            OnBoardDataGenerationComplete = null;
        }

        /// <summary>
        /// Generates the board based on the given <see cref="BoardData"/>.
        /// </summary>
        /// <param name="boardData">The data used to generate the board.</param>
        private void GenerateBoard(BoardData boardData)
        {
            _boardData = boardData;

            // TODO - This will be rewritten in a board-generation PR
            _boardData.FeedData(
                areaHeightsInTiles: dummyAreaHeightInTiles,
                areaTileBases: dummyAreaTileBases,
                tileSizeInWorldUnits: grid.cellSize
            );

            _boardData.GenerateFields();

            OnBoardDataGenerationComplete?.Invoke(_boardData);

            SpawnBoard();
        }

        /// <summary>
        /// Spawns the entire board, based on the <see cref="_boardData"/>.
        /// </summary>
        public void SpawnBoard()
        {
            grid.transform.position = new Vector3(-_boardData.BoardSizeInTiles.x / 2, -_boardData.BoardSizeInTiles.y / 2, 0);

            SpawnFloorTiles();
        }

        /// <summary>
        /// Spawns floor tiles on the board, based on the <see cref="_boardData"/>.
        /// </summary>
        /// <remarks>
        /// The tiles are spawned in order of their <see cref="BoardAreaType"/> enum value.
        /// For each area, the tiles are spawned from y = 0 up to y = <see cref="_boardData.AreaHeightInTiles"/>[<see cref="BoardAreaType"/>].
        /// </remarks>
        private void SpawnFloorTiles()
        {
            TileBase currentTile;
            int ySpawnStart = 0, ySpawnEnd;

            for (BoardAreaType boardAreaToSpawn = BoardAreaType.BottomNoMansLand; boardAreaToSpawn < BoardAreaType.TopNoMansLand + 1; boardAreaToSpawn++)
            {
                currentTile = _boardData.AreaTileBases[boardAreaToSpawn];
                ySpawnEnd = ySpawnStart + _boardData.AreaHeightsInTiles[boardAreaToSpawn];

                for (int y = ySpawnStart; y < ySpawnEnd; y++)
                {
                    for (int x = 0; x < _boardData.BoardSizeInTiles.x; x++)
                    {
                        tilemap.SetTile(new Vector3Int(x, y, 0), currentTile);
                    }
                }

                ySpawnStart = ySpawnEnd;
            }
        }

        #endregion
    }

    /// <summary>
    /// Represents a data structure for board generation.
    /// </summary>
    public class BoardData
    {
        #region Fields

        private Vector2 _boardSizeInWorldUnits;

        /// <summary>
        /// The size of the board in tiles.
        /// </summary>
        public Vector2Int BoardSizeInTiles { get; private set; }

        /// <summary>
        /// A dictionary of <see cref="BoardAreaType"/> to area heights in tiles.
        /// </summary>
        public Dictionary<BoardAreaType, int> AreaHeightsInTiles { get; private set; }

        /// <summary>
        /// A dictionary of <see cref="BoardAreaType"/> to tile bases.
        /// </summary>
        public Dictionary<BoardAreaType, TileBase> AreaTileBases { get; private set; }

        /// <summary>
        /// The size of a tile in world units.
        /// </summary>
        public Vector2 TileSizeInWorldUnits { get; private set; }

        #endregion

        #region Methods

        public BoardData() {}

        public void FeedData(LevelData levelData)
        {
            _boardSizeInWorldUnits = levelData.BoardSizeInWorldUnits;
        }

        public void FeedData(Dictionary<BoardAreaType, int> areaHeightsInTiles, Dictionary<BoardAreaType, TileBase> areaTileBases, Vector2 tileSizeInWorldUnits)
        {
            AreaHeightsInTiles = areaHeightsInTiles;
            AreaTileBases = areaTileBases;
            TileSizeInWorldUnits = tileSizeInWorldUnits;
        }

        public void GenerateFields()
        {
            BoardSizeInTiles = new Vector2Int(
                Mathf.FloorToInt(_boardSizeInWorldUnits.x / TileSizeInWorldUnits.x),
                Mathf.FloorToInt(_boardSizeInWorldUnits.y / TileSizeInWorldUnits.y)
            );
        }

        #endregion
    }

    [Serializable]
    public enum BoardAreaType
    {
        None = 0,
        BottomNoMansLand = 1,
        PlayerSpawn = 2,
        BattleArena = 3,
        EnemySpawn = 4,
        TopNoMansLand = 5
    }
}