using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    #region Scene Settings
    [SerializeField] private string gameplaySceneName;
    [SerializeField] private string mainMenuSceneName;
    #endregion

    public void ContinueGame()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}