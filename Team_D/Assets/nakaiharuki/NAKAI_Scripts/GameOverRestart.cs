using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void OnRestart()
    {
        Time.timeScale = 1f;

        StageClearManager.isClear = false;
        GameOverManager.isGameOver = false;


        SceneManager.LoadScene(GameOverManager.lastStage);
    }
}