using System.Collections.Generic;
using Scripts.Utilities;
using UnityEngine;

namespace Scripts.UI.WorldUI.Enemy.Health
{
    public class EnemyHealthBarSpawner : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject enemyHealthBarPrefab;

        private List<EnemyHealthBar> _spawnedEnemyHealthBars;

        #endregion

        #region Methods

        public void Init()
        {
            _spawnedEnemyHealthBars = new List<EnemyHealthBar>();
        }

        // TODO - This will be changed in an enemy-spawning PR
        public void Spawn()
        {
            if(_spawnedEnemyHealthBars == null)
                return;

            EnemyHealthBar healthBar = MiscUtils.InstantiatePrefab<GameObject, EnemyHealthBar>(
                prefab: enemyHealthBarPrefab, 
                parent: transform, 
                name: "Enemy Health Bar"
            );

            if(healthBar != null)
            {
                _spawnedEnemyHealthBars.Add(healthBar);
                
                healthBar.Setup(max: 10, current: 10);
                healthBar.RePosition(new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-5f, 5f)));
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}
