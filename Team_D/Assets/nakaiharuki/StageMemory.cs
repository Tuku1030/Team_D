using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static string lastStage;  // 最後にいたステージ



    public static void GameOver()
    {
        // 今のステージ名を保存
        lastStage = SceneManager.GetActiveScene().name;
        // ゲームオーバーシーンへ
        SceneManager.LoadScene("GameOver");
    }
}