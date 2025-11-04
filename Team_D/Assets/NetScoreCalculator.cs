using System.Collections.Generic;
using UnityEngine;
using TMPro; // ← TextMeshPro用

public class NetScoreCalculator : MonoBehaviour
{
    private Dictionary<string, (int count, float rate, int baseScore)> fishData = new();
    private float _Score = 0;

    [SerializeField] private TextMeshProUGUI ScoreText; // TMP用

    void Start()
    {
        if (ScoreText != null)
            ScoreText.text = "Score: 0000";
    }

    public void AddCapturedFish(string fishName, float addRate, int baseScore)
    {
        if (!fishData.ContainsKey(fishName))
            fishData[fishName] = (1, addRate, baseScore);
        else
        {
            var current = fishData[fishName];
            fishData[fishName] = (current.count + 1, current.rate, current.baseScore);
        }

        float addedScore = CalculateAddedScore(fishName);
        _Score += addedScore;

        Debug.Log("Score total: " + _Score);

        UpdateScoreText();
    }

    private float CalculateAddedScore(string fishName)
    {
        var (count, rate, baseScore) = fishData[fishName];
        return baseScore * (1 + rate * (count - 1));
    }

    private void UpdateScoreText()
    {
        if (ScoreText != null)
            ScoreText.text = $"Score: {_Score:0000}";
    }
}
