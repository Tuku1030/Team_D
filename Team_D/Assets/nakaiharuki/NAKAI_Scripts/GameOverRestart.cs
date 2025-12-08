using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void OnRestart()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(GameOverManager.lastStage);
    }
}