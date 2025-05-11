namespace Core
{
    /// <summary>
    /// Game state changed event using Observer pattern
    /// </summary>
    public class GameStateChangedEvent
    {
        public bool IsPlaying { get; }

        public GameStateChangedEvent(bool isPlaying)
        {
            IsPlaying = isPlaying;
        }
    }
}