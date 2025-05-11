using Services;

namespace Core
{
    /// <summary>
    /// Concrete state implementation
    /// </summary>
    public class PlayingState : IGameState
    {
        public void Enter()
        {
            var waveManager = ServiceLocator.Get<IWaveManager>();
            if (!((WaveManager)waveManager).IsInitialized) return;
            waveManager.StartNextWave();
            EventSystem.Publish(new GameStateChangedEvent(true));
        }

        public void Exit()
        {
            ServiceLocator.Get<IWaveManager>().StopWave();
        }
    }
}