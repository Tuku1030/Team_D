using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void OnRestart()
    {
        Time.timeScale = 1f;

        // スコアをリセット
        if (TotalScoreManager.Instance != null)
        {
            TotalScoreManager.Instance.ResetScore();
        }

        // フラグを初期化
        StageClearManager.isClear = false;
        GameOverManager.isGameOver = false;

        // ステージ再読み込み
        SceneManager.LoadScene(GameOverManager.lastStage);
    }
}
