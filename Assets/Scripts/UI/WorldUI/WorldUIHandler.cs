using Scripts.Constants;
using Scripts.UI.WorldUI.Enemy.Health;
using Scripts.Utilities;
using UnityEngine;

namespace Scripts.UI.WorldUI
{
    public class WorldUIHandler : MonoBehaviour, IUIHandler
    {
        #region Fields

        [SerializeField] private Canvas canvas;

        private EnemyHealthBarSpawner _enemyHealthBarSpawner;

        public RectTransform RectTransform
        {
            get => transform as RectTransform;
        }

        #endregion

        #region Methods

        public void OnMainCameraChanged(Camera mainCamera) => canvas.worldCamera = mainCamera;

        public void OnLevelStart()
        {
            _enemyHealthBarSpawner = MiscUtils.InstantiatePrefab<EnemyHealthBarSpawner>(
                path: FilePaths.EnemyHealthBarSpawnerPrefab, 
                parent: transform, 
                name: "Enemy Health Bars"
            );

            if(_enemyHealthBarSpawner != null)
            {
                _enemyHealthBarSpawner.Init();

                // TODO - This will be changed in an enemy-spawning PR
                int spawnCount = Random.Range(2, 5);
                for (int i = 0; i < spawnCount; i++) 
                    _enemyHealthBarSpawner.Spawn();
            }
        }

        #endregion
    }
}