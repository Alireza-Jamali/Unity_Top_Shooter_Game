using System;
using Core;
using Player;
using UnityEngine;

namespace Enemy
{
    /// <summary>
    /// Enemy controller using Strategy pattern for movement
    /// Is placed on Enemy GameObject Prefab
    /// </summary>
    public class EnemyAIController : MonoBehaviour, IEnemy
    {
        // Strategy pattern for different movement types
        private IEnemyMovement _movementStrategy;
        private int _health;
        EnemyType _enemyType;
        

        /// <summary>
        /// Initialize enemy with configuration
        /// </summary>
        public void Initialize(EnemyType enemyType)
        {
            _enemyType = enemyType;
            _health = enemyType.Health;
            _movementStrategy = enemyType.MovementStrategy;
            GetComponent<Renderer>().material.color = enemyType.Color;
        }
        public EnemyType GetEnemyType()
        {
            return _enemyType;
        }

        void Update() => _movementStrategy.Move(transform);

        public void TakeDamage(int amount)
        {
            _health -= amount;
            if (_health <= 0) Destroy(gameObject);
        }
    }
}