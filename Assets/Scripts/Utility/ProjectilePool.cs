using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Utility
{
    /// <summary>
    /// Manages projectile pooling and reuse
    /// Responsibilities:
    /// - Maintain pool of projectile objects
    /// - Handle projectile lifecycle
    /// - Optimize memory usage
    /// Collaborators:
    /// - WeaponSystem
    /// - EnemySystem
    /// </summary>
    public class ProjectilePool : MonoBehaviour
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private int _initialPoolSize = 20;
        [SerializeField] private Transform _spawnPoint;

        private Queue<Projectile> _pool = new();

        private void Start()
        {
            WarmPool();
        }

        private void WarmPool()
        {
            for (int i = 0; i < _initialPoolSize; i++)
            {
                Projectile projectile = CreateNew();
                projectile.gameObject.SetActive(false);
                _pool.Enqueue(projectile);
            }
        }

        public Projectile Get()
        {
            Projectile projectile = _pool.Count > 0 ? _pool.Dequeue() : CreateNew();

            projectile.transform.position = _spawnPoint.position;
            projectile.gameObject.SetActive(true);
            return projectile;
        }

        public void ReturnToPool(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);
            _pool.Enqueue(projectile);
        }

        private Projectile CreateNew()
        {
            Projectile newProjectile = Instantiate(_projectilePrefab);
            newProjectile.Initialize(this);
            return newProjectile;
        }
    }
}