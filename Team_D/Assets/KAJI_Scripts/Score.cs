using UnityEngine;
using UnityEngine.UI;

// 🎯 こちらはスコア表示用
public class Score : MonoBehaviour
{
    private int _Score = 0;              // 得点の変数
    [SerializeField] private Text scoreText; // Inspectorでドラッグ可能

    void Start()
    {
        _Score = 0;
        UpdateScoreText();
    }

    void Update()
    {
        // 毎フレームスコアを更新
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        _Score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {_Score:0000}";
    }
}
