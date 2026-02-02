using UnityEngine;
using TMPro; // TextMeshPro を使うとき

public class GameScoreManager : MonoBehaviour
{
    public static GameScoreManager Instance; // シングルトン



    private float totalScore = 0;
    [SerializeField] private TextMeshProUGUI ScoreText; // Inspector でドラッグ
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // ← これが絶対必要！！
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // スコア加算用
    public void AddScore(float score)
    {
        totalScore += score;
        // デバッグ
        Debug.Log("ゲーム全体スコア: " + totalScore);
        // TMP に表示
        if (ScoreText != null)
            ScoreText.text = $"Total: {totalScore:0000}";
    }
    // 現在の合計スコアを返す
    public float GetTotalScore()
    {
        return totalScore;
    }
}
