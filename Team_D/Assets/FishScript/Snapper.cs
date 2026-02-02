using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using FishGame;

public class Snapper : MonoBehaviour, IFish
{
    public NetScoreCalculator scoreCalculator { get; set; } // スコア管理用
    [Header("魚データ設定")]
    public string fishName = "Snapper";  // 魚の種類名
    public float addRate = 0.2f;         // この魚1匹あたりの倍率加算値
    public int baseScore = 30;           // 基礎スコア

    private bool isCaptured = false;
    private CaptureMoveEffect captureEffect;
    private Transform playerTransform;

    void Awake()
    {
        captureEffect = GetComponent<CaptureMoveEffect>();
    }

    void Start()
    {
        if (scoreCalculator == null)
        {
            scoreCalculator = Object.FindFirstObjectByType<NetScoreCalculator>();
        }

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCaptured) return;

        if (other.CompareTag("BigNet"))
        {
            isCaptured = true;

            if (scoreCalculator != null)
            {
                scoreCalculator.AddCapturedFish(fishName, addRate, baseScore);
            }

            if (captureEffect != null && playerTransform != null)
            {
                captureEffect.Play(playerTransform);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
