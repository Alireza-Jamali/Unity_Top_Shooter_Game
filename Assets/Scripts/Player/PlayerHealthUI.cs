using System;
using Core;
using Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    /// <summary>
    /// Is placed on Player Health (observer pattern)
    /// </summary>
    public class PlayerHealthUI : MonoBehaviour
    {
        [SerializeField] Image healthBar;

        void Awake()
        {
            PlayerHealthManager.OnPlayerHit += OnPlayerHit;
            GameManager.OnRestart += RestartHealth;
        }
        void RestartHealth()
        {
            healthBar.fillAmount = 1;
        }
        void OnPlayerHit(IEnemy _, float playerHealth, float maxHealth)
        {
            healthBar.fillAmount = playerHealth / maxHealth;
        }
    }
}