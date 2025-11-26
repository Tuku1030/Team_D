using UnityEngine;
using TMPro;

public class GameScoreManager : MonoBehaviour
{
    public static GameScoreManager Instance;



    private float totalScore = 0;
    [SerializeField] private TextMeshProUGUI ScoreText;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    void OnEnable()
    {
        // シーンが変わったらもう一度 UI を探す
        FindScoreText();
    }
    void OnSceneLoaded()
    {
        // これが必要な場合もある
        FindScoreText();
    }
    // UI を探す関数
    private void FindScoreText()
    {
        if (ScoreText == null)
            ScoreText = GameObject.FindWithTag("TotalScoreText")?.GetComponent<TextMeshProUGUI>();
    }
    public void AddScore(float score)
    {
        totalScore += score;
        Debug.Log("ゲーム全体スコア: " + totalScore);
        if (ScoreText != null)
            ScoreText.text = $"Total: {totalScore:0000}";
    }
    public float GetTotalScore()
    {
        return totalScore;
    }
}