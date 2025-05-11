using Core;
using Enemy;

namespace DesignPatterns
{
    /// <summary>
    /// Bridge interface for enemy behavior variations
    /// </summary>
    public interface IEnemyBehaviorProvider
    {
        IEnemyMovement GetMovementStrategy(EnemyType type);
        //IEnemyAttack GetAttackStrategy(EnemyType type);
    }
}