using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TotalScoreManager : MonoBehaviour
{
    public static TotalScoreManager Instance;



    private int totalScore = 0;
    private TextMeshProUGUI scoreText; // ゲーム中のスコア表示用
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ★ どのステージでも GameScoreText を探す
        GameObject obj = GameObject.Find("GameScoreText");
        if (obj != null)
            scoreText = obj.GetComponent<TextMeshProUGUI>();
        else
            scoreText = null; // 見つからない場合は null
    }
    // スコア加算
    public void AddScore(int score)
    {
        totalScore += score;
        Debug.Log("★TotalScoreに加算: " + totalScore);
        // 念のため再取得
        if (scoreText == null)
        {
            GameObject obj = GameObject.Find("GameScoreText");
            if (obj != null)
                scoreText = obj.GetComponent<TextMeshProUGUI>();
        }
        if (scoreText != null)
            scoreText.text = $"Score: {totalScore}";
    }
    // Result画面用
    public int GetTotalScore()
    {
        return totalScore;
    }
    public void ResetScore()
    {
        totalScore = 0;
    }
}