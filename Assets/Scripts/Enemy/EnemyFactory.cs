using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Data;
using DesignPatterns;
using Player;
using Services;
using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

namespace Enemy
{
    /// <summary>
    /// Enemy factory implementation using Bridge pattern and Type-Safe Enum
    /// Is placed on Enemy Factory Game Object
    /// </summary>
    public class EnemyFactory : MonoBehaviour, IEnemyFactory
    {
        [SerializeField] Transform enemySpawnPointYZ;
        [SerializeField] float enemySpawnPointXLimit;

        // Prefab references should be set in inspector or loaded dynamically
        [SerializeField] private GameObject _enemyPrefab;

        bool _gameOvered;

        void Start()
        {
            ServiceLocator.Get<IWaveManager>().OnWaveStarted += HandleOnWaveStarted;
            ServiceLocator.Get<IWaveManager>().OnWaveCompleted += HandleOnWaveCompleted;
            PlayerHealthManager.OnGameOver += OnGameOver;
            GameManager.OnRestart += OnRestart;
        }

        private void HandleOnWaveStarted(int currentWave, List<WaveConfiguration> waveConfig)
        {
            if (_gameOvered || currentWave == waveConfig.Count)
            {
                StartCoroutine(CheckForEnemies());
                return;
            }

            foreach (var enemy in waveConfig.ToArray()[currentWave].Enemies)
            {
                for (int i = 0; i < enemy.Count; i++)
                {
                    var enemySpawnPosition = new Vector3(Random.Range(enemySpawnPointXLimit, -enemySpawnPointXLimit),
                        enemySpawnPointYZ.position.y, enemySpawnPointYZ.position.z);
                    Create(enemy.Type, enemySpawnPosition);
                }
            }

            StartCoroutine(WaveFinished());
        }

        IEnumerator CheckForEnemies()
        {
            if (!GameObject.FindWithTag(Tags.Enemy))
            {
                ServiceLocator.Get<IUIManager>().ShowYouWon();
            }
            else yield return new WaitForSeconds(1);
        }

        void HandleOnWaveCompleted()
        {
            ServiceLocator.Get<IWaveManager>().StartNextWave();
        }

        IEnumerator WaveFinished()
        {
            while (GameObject.FindWithTag(Tags.Enemy))
            {
                yield return new WaitForSeconds(2);
            }

            ServiceLocator.Get<IWaveManager>().StopWave();
        }

        public IEnemy Create(EnemyType enemyType, Vector3 position)
        {
            // Instantiate from prefab
            var enemyObj = Instantiate(_enemyPrefab, position, Quaternion.identity);

            // Configure enemy
            var enemy = enemyObj.GetComponent<IEnemy>();
            if (enemy == null)
            {
                throw new MissingComponentException($"Enemy prefab missing IEnemy component");
            }

            // Initialize with type-specific properties and behavior
            enemy.Initialize(enemyType);

            return enemy;
        }
        void OnRestart()
        {
            _gameOvered = false;
        }
        void OnGameOver()
        {
            _gameOvered = true;
            StopAllCoroutines();
            foreach (var enemy in FindObjectsOfType<EnemyAIController>())
            {
                Destroy(enemy.gameObject);
            }
        }
    }
}