using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishGame;

public class Sardine : MonoBehaviour, IFish
{
    public NetScoreCalculator scoreCalculator { get; set; } // スコア管理用
    [Header("魚データ設定")]
    public string fishName = "Sardine";  // 魚の種類名
    public float addRate = 0.1f;               // この魚1匹あたりの倍率加算値
    public int baseScore = 10;                 // 基礎スコア

    private bool isCaptured = false; // 捕獲済み判定

    void Start()
    {
        // スコア管理コンポーネントを取得（警告なし）
        if (scoreCalculator == null)
        {
            scoreCalculator = Object.FindFirstObjectByType<NetScoreCalculator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCaptured) return;

        if (other.CompareTag("BigNet"))
        {
            isCaptured = true;

            // ★ スコア加算
            if (scoreCalculator != null)
            {
                scoreCalculator.AddCapturedFish(fishName, addRate, baseScore);
            }

            Debug.Log("a");
           Destroy(gameObject); // オブジェクトを削除
        }
    }
}
