using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using Services;
using UnityEngine;

namespace Core
{
    public class WaveManager : IWaveManager
    {
        public event Action<int, List<WaveConfiguration>> OnWaveStarted;
        public event Action OnWaveCompleted;

        private List<WaveConfiguration> _waveConfigurations;
        private int _currentWave;
        public bool IsInitialized;

        public WaveManager()
        {
            
            InitializeAsync().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    Debug.LogError($"WaveManager initialization failed: {task.Exception}");
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async Task InitializeAsync()
        {
            try
            {
                _waveConfigurations = await ServiceLocator.Get<IWaveDataProvider>().GetWaveConfigurationsAsync();
                IsInitialized = true;
                Debug.Log("Wave configurations loaded successfully");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load wave configurations: {ex.Message}");
                _waveConfigurations = new List<WaveConfiguration>();
            }
        }

        public void StartNextWave()
        {
            if (!IsInitialized)
            {
                Debug.LogWarning("Wave manager not initialized yet");
                return;
            }
            
            OnWaveStarted?.Invoke(_currentWave++, _waveConfigurations);
        }

        public void StopWave()
        {
            OnWaveCompleted?.Invoke();
        }
        
        public void Restart()
        {
            _currentWave = 0;
            StartNextWave();
        }
    }
}