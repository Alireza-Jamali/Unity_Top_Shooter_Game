using System;
using Data;
using Player;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core
{
    /// <summary>
    /// Is placed on Game Manager Game Object
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] GameObject menu;
        [SerializeField] GameObject gameOverUI;
        [SerializeField] GameObject youWon;
        [SerializeField] Toggle mouseInputToggle;
        [SerializeField] Toggle touchInputToggle;
        private IGameState _currentState;

        public static Action OnRestart;

        protected override void Awake()
        {
            base.Awake();
            mouseInputToggle.onValueChanged.AddListener(val => touchInputToggle.isOn = !val);
            touchInputToggle.onValueChanged.AddListener(val => mouseInputToggle.isOn = !val);
            InitializeServices();
        }

        private void InitializeServices()
        {
            ServiceLocator.Register<IWaveDataProvider>(new WaveDataService());
            ServiceLocator.Register<IWaveManager>(new WaveManager());
            ServiceLocator.Register<IUIManager>(new UIManager(menu, gameOverUI, youWon));
        }

        /// <summary>
        /// Is placed on Start Game Button
        /// </summary>
        public void StartGame()
        {
            SetState(new PlayingState());
            menu.SetActive(false);
        }

        public void ReturnToMenu()
        {
            SetState(new MainMenuState());
        }

        public void SetState(IGameState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
        public void RestartGame()
        {
            ServiceLocator.Get<IUIManager>().HideGameOver();
            ServiceLocator.Get<IUIManager>().HideYouWon();
            OnRestart?.Invoke();
            ServiceLocator.Get<IWaveManager>().Restart();
        }
    }
}