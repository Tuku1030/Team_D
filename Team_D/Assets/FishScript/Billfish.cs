using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using FishGame;

public class BillFish : MonoBehaviour, IFish
{
    public NetScoreCalculator scoreCalculator { get; set; } // スコア管理用
    public GameObject player;  // 移動対象
    public int speed = 5;      // 移動スピード
    private Vector3 movePosition; // 移動目標位置

    [Header("魚データ設定")]
    public string fishName = "BillFish";  // 魚の種類名
    public float addRate = 0.8f;          // この魚1匹あたりの倍率加算値
    public int baseScore = 100;           // 基礎スコア

    private bool isCaptured = false; // 捕獲済み判定

    void Start()
    {
        movePosition = moveRandomPosition();  // 目的地を設定

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
            Debug.Log("魚に触れたよ〜: " + other.gameObject.name);

        if (isCaptured) return;

        if (other.CompareTag("BigNet"))
        {
            isCaptured = true;

            if (scoreCalculator != null)
            {
                scoreCalculator.AddCapturedFish(fishName, addRate, baseScore);
            }

            Destroy(gameObject); // 魚を削除
        }
    }

    // ランダムな目的地を生成
    private Vector3 moveRandomPosition()
    {
        return new Vector3(Random.Range(-4f, 10f), Random.Range(-5f, 5f), speed);
    }
}
