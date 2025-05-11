using DesignPatterns;
using UnityEngine;

namespace Implementations
{
    /// <summary>
    /// Play state implementation
    /// </summary>
    public class PlayState : IGameState
    {
        public void Enter() => Time.timeScale = 1;
        public void Exit() => Time.timeScale = 0;
    }
}