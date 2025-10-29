using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sardine : MonoBehaviour
{
    public GameObject player;  // 移動対象
    public int speed = 3;      // 移動スピード
    Vector3 movePosition;      // 移動目標位置

    [Header("魚データ設定")]
    public string fishName = "Sardine";  // 魚の種類名（例：アジ）
    public float addRate = 0.2f;               // この魚1匹あたりの倍率加算値
    public int baseScore = 10;                 // 🔹基礎スコアを追加

    private bool isCaptured = false; // 捕獲済み判定

    void Start()
    {
        movePosition = moveRandomPosition();
    }

    void Update()
    {
        if (isCaptured) return; // 捕獲済みなら動かさない

        // ランダム移動
        if (movePosition == player.transform.position)
        {
            movePosition = moveRandomPosition();
        }

        player.transform.position = Vector3.MoveTowards(player.transform.position, movePosition, speed * Time.deltaTime);

        // 向き反転
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (player.transform.position.x < movePosition.x)
        {
            spriteRenderer.flipX = true;
        }
        else if (player.transform.position.x > movePosition.x)
        {
            spriteRenderer.flipX = false;
        }
    }

    // 網に当たったときの処理
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCaptured) return;

        if (other.CompareTag("BigNet")) // 網オブジェクトのタグを"Net"に設定しておく
        {
            isCaptured = true;

            // 捕獲されたことをスコア管理へ通知
            NetScoreCalculator scoreCalculator = FindObjectOfType<NetScoreCalculator>();
            if (scoreCalculator != null)
            {
                // 🔹基礎スコアも一緒に渡すように変更
                scoreCalculator.AddCapturedFish(fishName, addRate, baseScore);
            }

            // 捕獲演出などを入れたい場合はここにアニメーション等を追加
            Destroy(gameObject); // 魚を削除
        }
    }

    private Vector3 moveRandomPosition()
    {
        return new Vector3(Random.Range(-7, 7), Random.Range(-4, 4), 1);
    }
}