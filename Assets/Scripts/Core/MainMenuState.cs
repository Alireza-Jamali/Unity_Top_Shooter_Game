using Services;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// Main menu state implementation
    /// </summary>
    public class MainMenuState : IGameState
    {
        public void Enter()
        {
            EventSystem.Publish(new GameStateChangedEvent(false));
            ServiceLocator.Get<IUIManager>().ShowMainMenu();
            Time.timeScale = 0f;
        }

        public void Exit()
        {
            ServiceLocator.Get<IUIManager>().HideMainMenu();
            Time.timeScale = 1f;
        }
    }
}