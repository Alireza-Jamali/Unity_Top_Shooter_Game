using System;
using Core;
using Enemy;
using Services;
using UnityEngine;
using Utility;

namespace Player
{
    /// <summary>
    /// is placed on Player Health Manager GameObject
    /// </summary>
    public class PlayerHealthManager : MonoBehaviour
    {
        [SerializeField] float maxHealth = 100;

        private float _health;
        public static Action<IEnemy, float, float> OnPlayerHit;
        public static Action OnGameOver;

        void Awake()
        {
            _health = maxHealth;
            GameManager.OnRestart += RestartHealth;
        }
        void RestartHealth()
        {
            _health = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                OnGameOver?.Invoke();
                ServiceLocator.Get<IUIManager>().ShowGameOver();
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Enemy))
            {
                var enemy = other.GetComponent<IEnemy>();
                TakeDamage(enemy.GetEnemyType().Damage);
                OnPlayerHit?.Invoke(other.GetComponent<IEnemy>(), _health, maxHealth);
                Destroy(other.gameObject);
            }
        }
    }
}