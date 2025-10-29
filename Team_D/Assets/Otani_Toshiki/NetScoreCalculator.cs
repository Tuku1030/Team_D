using System.Collections.Generic;
using UnityEngine;

public class NetScoreCalculator : MonoBehaviour
{
    private Dictionary<string, (int count, float rate, int baseScore)> fishData = new();

    private float _Score; // 🟢 累計スコア

    public void AddCapturedFish(string fishName, float addRate, int baseScore)
    {
        // 魚データの登録または更新
        if (!fishData.ContainsKey(fishName))
        {
            fishData[fishName] = (1, addRate, baseScore);
        }
        else
        {
            var current = fishData[fishName];
            fishData[fishName] = (current.count + 1, current.rate, current.baseScore);
        }

        // 🟢 今回の魚で得られるスコアを計算
        float addedScore = CalculateAddedScore(fishName);

        // 🟢 トータルスコアに加算
        _Score += addedScore;

        // 🟢 ログ表示
        Debug.Log($"🐟 捕獲: {fishName}, 今回のスコア: +{addedScore:F2}, 累計: {_Score:F2}");
    }

    // 魚1匹ごとの追加スコアを計算
    private float CalculateAddedScore(string fishName)
    {
        var (count, rate, baseScore) = fishData[fishName];

        // 🟢 基礎スコア × （1 + (捕獲数 - 1) × 加算率）
        float fishScore = baseScore * (1 + rate * (count - 1));

        return fishScore;
    }

    // 🟢 外部（UIなど）から参照できるようにプロパティを追加
    public float GetTotalScore()
    {
        return _Score;
    }
}
