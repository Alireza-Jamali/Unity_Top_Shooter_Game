using UnityEngine;

namespace Core
{
    /// <summary>
    /// Is placed on UI Manager
    /// </summary>
    public class UIManager : IUIManager
    {
        private GameObject _menu;
        private GameObject _gameOverScreen;
        private GameObject _youWonScreen;
        public UIManager(GameObject menu, GameObject gameOverScreen, GameObject youWonScreen)
        {
            _menu = menu;
            _gameOverScreen = gameOverScreen;
            _youWonScreen = youWonScreen;
        }
        public void ShowMainMenu() => _menu.SetActive(true);
        public void HideMainMenu() => _menu.SetActive(false);
        public void ShowGameOver() => _gameOverScreen.SetActive(true);
        public void HideGameOver() => _gameOverScreen.SetActive(false);
        public void HideYouWon() => _youWonScreen.SetActive(false);
        public void ShowYouWon() => _youWonScreen.SetActive(true);
    }
}