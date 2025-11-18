using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void OnRestart()
    {
        SceneManager.LoadScene(GameOverManager.lastStage);
    }
}