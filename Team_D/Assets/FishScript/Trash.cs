using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using FishGame;

public class Trash : MonoBehaviour, IFish
{
    public NetScoreCalculator scoreCalculator { get; set; } // スコア管理用
    public GameObject player;  // 移動対象
    public int speed = 3;      // 移動スピード
    private Vector3 movePosition; // 移動目標位置

    [Header("魚データ設定")]
    public string fishName = "Trash";  // 魚の種類名
    public float addRate = -0.2f;      // この魚1匹あたりの倍率加算値
    public int baseScore = 0;           // 基礎スコア

    private bool isCaptured = false; // 捕獲済み判定

    void Start()
    {
        movePosition = moveRandomPosition();

        // スコア管理コンポーネントを取得（警告なし）
        if (scoreCalculator == null)
        {
            scoreCalculator = Object.FindFirstObjectByType<NetScoreCalculator>();
        }
    }

    void Update()
    {
        if (isCaptured) return; // 捕獲済みなら動かさない

        // 目的地に到達したら新しい目的地を設定
        if (movePosition == player.transform.position)
        {
            movePosition = moveRandomPosition();
        }

        // プレイヤーオブジェクトを目的地に向かって移動
        player.transform.position = Vector3.MoveTowards(player.transform.position, movePosition, speed * Time.deltaTime);

        // Sprite の反転処理
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (player.transform.position.x < movePosition.x && !spriteRenderer.flipX)
        {
            spriteRenderer.flipX = true;
        }
        else if (player.transform.position.x > movePosition.x && spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCaptured) return;

        if (other.CompareTag("BigNet") )
        {
            isCaptured = true;

            if (scoreCalculator != null)
            {
                scoreCalculator.AddCapturedFish(fishName, addRate, baseScore);
            }
            // ★ BigNetからPlayerControllerを探す（ヒットしたNetの親などにいる想定）
            PlayerController player = other.GetComponentInParent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(1);   // ← 1ダメージ与える
            }
            Destroy(gameObject); // オブジェクトを削除
        }
        else
        {
            isCaptured = true;
            Destroy(gameObject);
        }
    }

    // ランダムな目的地を生成
    private Vector3 moveRandomPosition()
    {
        return new Vector3(Random.Range(-4f, 10f), Random.Range(-5f, 5f), 1f);
    }
}
