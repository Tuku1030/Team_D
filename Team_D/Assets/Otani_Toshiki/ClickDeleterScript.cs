using System.Collections.Generic;
using UnityEngine;

public class NetScoreCalculator : MonoBehaviour
{
    private const float baseScore = 1f;
    private float totalScoreMultiplier = 1f; // 現在の網スコア倍率

    // 魚を捕獲したときに呼ばれるメソッド
    public void CaptureFishes(List<Fish> fishes)
    {
        if (fishes == null || fishes.Count == 0) return;

        // 種類ごとに集計
        Dictionary<string, (int count, float addRate)> fishCounts = new();

        foreach (var fish in fishes)
        {
            if (!fishCounts.ContainsKey(fish.fishName))
            {
                fishCounts[fish.fishName] = (1, fish.addRate);
            }
            else
            {
                var current = fishCounts[fish.fishName];
                fishCounts[fish.fishName] = (current.count + 1, current.addRate);
            }
        }

        // 魚ごとの倍率加算を合計
        float addTotal = 0f;
        foreach (var kvp in fishCounts)
        {
            var (count, rate) = kvp.Value;
            if (count > 1)
            {
                float add = (count - 1) * rate;
                Debug.Log($"{kvp.Key} の倍率加算: {add}");
                addTotal += add;
            }
        }

        totalScoreMultiplier = baseScore + addTotal;
        Debug.Log($"最終スコア倍率: {totalScoreMultiplier}");

        // 捕獲した魚を削除
        foreach (var fish in fishes)
        {
            Destroy(fish.gameObject);
        }
    }

    public float GetCurrentScoreMultiplier()
    {
        return totalScoreMultiplier;
    }
}