using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void OnRestart()
    {
        Time.timeScale = 1f;

<<<<<<< HEAD
        // ★ スコアをリセット
        if (TotalScoreManager.Instance != null)
        {
            TotalScoreManager.Instance.ResetScore();
        }

        // ステージ再読み込み
=======
        StageClearManager.isClear = false;
        GameOverManager.isGameOver = false;


>>>>>>> cfc0235e32c887b6e578ab11fd63d5d1616d9444
        SceneManager.LoadScene(GameOverManager.lastStage);
    }
}
