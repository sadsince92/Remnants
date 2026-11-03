using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    #region UI References
    [SerializeField] private GameObject optionsPanel;
#endregion
    public void StartGame()
    {
        SceneManager.LoadScene("Level_01");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }
    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }
}
