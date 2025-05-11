using Core;

namespace Data
{
    /// <summary>
    /// Data transfer object for individual enemy wave entries
    /// Pattern: Immutable DTO
    /// </summary>
    public readonly struct EnemyWaveData
    {
        public readonly EnemyType Type;
        public readonly int Count;

        public EnemyWaveData(EnemyType type, int count)
        {
            Type = type;
            Count = count;
        }
    }
}