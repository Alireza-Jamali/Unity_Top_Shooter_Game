namespace Core
{
    /// <summary>
    /// UI management contract
    /// </summary>
    public interface IUIManager
    {
        void ShowMainMenu();
        void HideMainMenu(); 
        void ShowGameOver();
        void HideGameOver();
        void ShowYouWon();
        void HideYouWon();
    }
}