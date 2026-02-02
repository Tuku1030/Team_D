using UnityEngine;
using FishGame;
using System.Collections.Generic;

public class NetScoreCalculator : MonoBehaviour
{
    private Dictionary<string, (int count, float rate, int baseScore)> fishData
    = new Dictionary<string, (int, float, int)>();
    public float Netrate;

    // 捕獲内容カウント
    private int fishCount = 0;
    private int trashCount = 0;

    // 結果SEを鳴らす相手
    private PlayerUnit player;


    private float _Score = 0; // 網専用（内部計算）

    void Start()
    {
        player = FindFirstObjectByType<PlayerUnit>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 魚
        IFish fish = other.GetComponent<IFish>();
        if (fish != null)
        {
            fishCount++;

            AddCapturedFish(other.gameObject.name, 0.1f, 10);
            Destroy(other.gameObject);
            return;
        }

        // ゴミ
        if (other.CompareTag("Trash"))
        {
            trashCount++;
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
        _Score += addedScore;
        // ★ TotalScoreManager に加算
        if (TotalScoreManager.Instance != null)
        {
            TotalScoreManager.Instance.AddScore((int)addedScore);
        }
        else
        {
            Debug.LogWarning(" TotalScoreManager.Instance が NULL です！");
        }
        // ★ 網に「音鳴らして〜」をお願い！
        PlayerSoundController playerSound = GetComponent<PlayerSoundController>();
        if (playerSound != null)
        {
            playerSound.TryPlayCaptureSound();  // 1フレーム1回だけ鳴る！
        }
        Debug.Log($"魚: {fishName}, 網スコア: {_Score}, 加算スコア: {addedScore}");
    }
    private float CalculateAddedScore(string fishName)
    {
        var (count, rate, baseScore) = fishData[fishName];
        Netrate = (1 + rate * (count - 1));
        return baseScore * Netrate;
    }
    private void OnDestroy()
    {
        if (player != null)
        {
            player.PlayNetResultSE(fishCount, trashCount);
        }
    }

}
