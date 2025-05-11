using System;
using System.Collections.Generic;
using Data;

namespace Services
{
    /// <summary>
    /// Wave management contract using Strategy pattern
    /// </summary>
    public interface IWaveManager
    {
        void StartNextWave();
        void StopWave();
        void Restart();
        event Action<int, List<WaveConfiguration>> OnWaveStarted;
        event Action OnWaveCompleted;
    }
}