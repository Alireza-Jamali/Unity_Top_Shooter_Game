using System.Collections.Generic;

namespace Data
{
    /// <summary>
    /// Represents configuration for a single enemy wave
    /// Immutable data structure for thread safety
    /// </summary>
    public sealed class WaveConfiguration
    {
        public IReadOnlyList<EnemyWaveData> Enemies { get; }

        public WaveConfiguration(IEnumerable<EnemyWaveData> enemies)
        {
            Enemies = new List<EnemyWaveData>(enemies).AsReadOnly();
        }
    }
}