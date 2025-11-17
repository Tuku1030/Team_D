using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static string lastStage;  // 死んだステージ名



    void Awake()
    {
        DontDestroyOnLoad(gameObject);  // シーンをまたいでも残す
    }
    public static void GameOver()
    {
        // 今のステージ名を保存
        lastStage = SceneManager.GetActiveScene().name;
        // ゲームオーバーシーンへ移動
        SceneManager.LoadScene("GameOver");
    }
    // ゲームオーバーシーンのボタンから呼ぶ用
    public void RestartGame()
    {
        // 保存していたステージへ戻る
        SceneManager.LoadScene(lastStage);
    }
}