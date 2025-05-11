namespace DesignPatterns
{
    /// <summary>
    /// State interface for game states
    /// </summary>
    public interface IGameState
    {
        void Enter();
        void Exit();
    }
}