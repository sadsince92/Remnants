using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathEvent : MonoBehaviour
{
    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}