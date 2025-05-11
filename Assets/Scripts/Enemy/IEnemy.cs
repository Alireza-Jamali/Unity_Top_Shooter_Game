using Core;

namespace Enemy
{
    public interface IEnemy
    {
        void Initialize(EnemyType enemyType);
        EnemyType GetEnemyType();
        void TakeDamage(int amount);
    }
}