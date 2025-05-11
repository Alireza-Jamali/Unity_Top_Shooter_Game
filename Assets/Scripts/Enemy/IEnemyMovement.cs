using UnityEngine;

namespace Enemy
{
    /// <summary>
    /// Movement strategy interface for different behaviors
    /// </summary>
    public interface IEnemyMovement
    {
        void Move(Transform transform);
    }
}