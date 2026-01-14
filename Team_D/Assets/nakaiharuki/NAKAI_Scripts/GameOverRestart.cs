using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void OnRestart()
    {
        Time.timeScale = 1f;

        // ★ スコアをリセット
        if (TotalScoreManager.Instance != null)
        {
            TotalScoreManager.Instance.ResetScore();
        }

        // ステージ再読み込み
        SceneManager.LoadScene(GameOverManager.lastStage);
    }
}
