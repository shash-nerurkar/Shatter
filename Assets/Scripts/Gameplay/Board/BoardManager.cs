using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Scripts.Utilities;
using System.Collections.Generic;

namespace Scripts.Gameplay.Board
{
    public class BoardManager : MonoBehaviour
    {
        #region Fields
        
        [SerializeField] private Grid grid;

        [SerializeField] private Tilemap tilemap;

        [SerializeField] private SerializableDictionary<BoardArea, TileBase> dummyAreaTileBases;

        [SerializeField] private SerializableDictionary<BoardArea, int> dummyAreaHeightInTiles;

        private BoardData _boardData;
        
        #endregion

        
        #region Methods
        
        private void Awake()
        {
            Game.Instance.StartDummyLevel += GenerateDummyBoard;
        }

        private void OnDestroy()
        {
            Game.Instance.StartDummyLevel -= GenerateDummyBoard;
        }

        // TODO - Will be removed in level-setup PR
        private void GenerateDummyBoard()
        {
            GenerateBoard(
                new BoardData(
                    Game.Instance.BoardSizeMaxInGameTiles, 
                    dummyAreaTileBases, 
                    dummyAreaHeightInTiles
                )
            );
        }

        /// <summary>
        /// Generates the board based on the given <see cref="BoardData"/>.
        /// </summary>
        /// <param name="boardData">The data used to generate the board.</param>
        private void GenerateBoard(BoardData boardData)
        {
            _boardData = boardData;

            grid.transform.position = new Vector3(-_boardData.BoardSizeInTiles.x / 2, -_boardData.BoardSizeInTiles.y / 2, 0);

            SpawnFloorTiles();
        }

        /// <summary>
        /// Spawns floor tiles on the board, based on the <see cref="_boardData"/>.
        /// </summary>
        /// <remarks>
        /// The tiles are spawned in order of their <see cref="BoardArea"/> enum value.
        /// For each area, the tiles are spawned from y = 0 up to y = <see cref="_boardData.AreaHeightInTiles"/>[<see cref="BoardArea"/>].
        /// </remarks>
        private void SpawnFloorTiles()
        {
            TileBase currentTile;
            int ySpawnStart = 0, ySpawnEnd;

            for (BoardArea boardAreaToSpawn = (BoardArea) 1; (int) boardAreaToSpawn < Enum.GetNames(typeof(BoardArea)).Length; boardAreaToSpawn++)
            {
                currentTile = _boardData.AreaTileBases[boardAreaToSpawn];
                ySpawnEnd = ySpawnStart + _boardData.AreaHeightInTiles[boardAreaToSpawn];

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
        /// <summary>
        /// The size of the board in tiles.
        /// </summary>
        public Vector2 BoardSizeInTiles { get; }

        /// <summary>
        /// A dictionary of <see cref="BoardArea"/> to tile bases.
        /// </summary>
        public Dictionary<BoardArea, TileBase> AreaTileBases { get; }

        /// <summary>
        /// A dictionary of <see cref="BoardArea"/> to area heights in tiles.
        /// </summary>
        public Dictionary<BoardArea, int> AreaHeightInTiles { get; }

        public BoardData(Vector2 boardSizeInTiles, Dictionary<BoardArea, TileBase> areaTileBases, Dictionary<BoardArea, int> areaHeightInTiles)
        {
            BoardSizeInTiles = boardSizeInTiles;
            AreaTileBases = areaTileBases;
            AreaHeightInTiles = areaHeightInTiles;
        }
    }

    [Serializable]
    public enum BoardArea
    {
        None = 0,
        BottomNoMansLand = 1,
        PlayerSpawn = 2,
        BattleArena = 3,
        EnemySpawn = 4,
        TopNoMansLand = 5
    }
}