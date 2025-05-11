using Core;
using Enemy;
using UnityEngine;

namespace DesignPatterns
{
    /// <summary>
    /// Abstract Factory implementation with Strategy pattern
    /// </summary>
    public interface IEnemyFactory
    {
        IEnemy Create(EnemyType enemyType, Vector3 position);
    }
}