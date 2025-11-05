using UnityEngine;
using FishGame; // IFish 名前空間
using System.Collections.Generic;

public class NetScoreCalculator : MonoBehaviour
{
    private Dictionary<string, (int count, float rate, int baseScore)> fishData = new();
    private float _Score = 0; // 網ごとの内部スコア（UIには表示しない）

    void OnTriggerEnter(Collider other)
    {
        // 魚に触れたら
        IFish fish = other.GetComponent<IFish>();
        if (fish != null)
        {
            AddCapturedFish(other.gameObject.name, 0.1f, 10);

            // 魚を消す処理
            Destroy(other.gameObject);
        }
    }

    public void AddCapturedFish(string fishName, float addRate, int baseScore)
    {
        // 網ごとの累計
        if (!fishData.ContainsKey(fishName))
            fishData[fishName] = (1, addRate, baseScore);
        else
        {
            var current = fishData[fishName];
            fishData[fishName] = (current.count + 1, current.rate, current.baseScore);
        }

        float addedScore = CalculateAddedScore(fishName);
        _Score += addedScore; // 網専用（内部計算だけ）

        // ゲーム全体スコアに加算
        if (GameScoreManager.Instance != null)
            GameScoreManager.Instance.AddScore(addedScore);

        // デバッグ用
        Debug.Log($"魚: {fishName}, 網スコア: {_Score}, 加算スコア: {addedScore}");
    }

    private float CalculateAddedScore(string fishName)
    {
        var (count, rate, baseScore) = fishData[fishName];
        return baseScore * (1 + rate * (count - 1));
    }
}
