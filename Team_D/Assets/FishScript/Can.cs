using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using FishGame;

public class Can : MonoBehaviour, IFish
{
    public HeartUIController heartUI;
    public NetScoreCalculator scoreCalculator { get; set; } // スコア管理用
    public GameObject player;  // 移動対象
    public int speed = 3;      // 移動スピード
    private Vector3 movePosition; // 移動目標位置
    private bool damaged = false; // 一度だけダメージを入れるフラグ

    [Header("魚データ設定")]
    public string fishName = "Can";  // 魚の種類名
    public float addRate = -0.5f;    // この魚1匹あたりの倍率加算値
    public int baseScore = 0;        // 基礎スコア
    private bool isCaptured = false; // 捕獲済み判定

    void Start()
    {
        movePosition = moveRandomPosition();  // オブジェクトの目的地を設定

        // スコア管理コンポーネントを取得（警告なし）
        if (scoreCalculator == null)
        {
            scoreCalculator = Object.FindFirstObjectByType<NetScoreCalculator>();
        }
    }

    void Update()
    {
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
        if (isCaptured || damaged) return;

        if (other.CompareTag("BigNet"))
        {
            damaged = true;
            isCaptured = true;

            if (scoreCalculator != null)
            {
                scoreCalculator.AddCapturedFish(fishName, addRate, baseScore);
            }

            // PlayerController を取得してダメージ
            PlayerController playerHP = Object.FindFirstObjectByType<PlayerController>();
            if (playerHP != null)
            {
                playerHP.TakeDamage(1);
            }

            Destroy(gameObject); // 魚を削除
        }
        else if (other.CompareTag("Net"))
        {
            isCaptured = true;
            Destroy(gameObject);
        }
    }

    // ランダムな目的地を生成
    private Vector3 moveRandomPosition()
    {
        return new Vector3(Random.Range(-4f, 10f), Random.Range(-5f, 5f), speed);
    }
}
