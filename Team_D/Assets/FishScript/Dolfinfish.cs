using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using FishGame;

public class Dolfinfish : MonoBehaviour, IFish
{
    public NetScoreCalculator scoreCalculator { get; set; }
    public GameObject player;  // 移動対象
    public int speed = 6;      // 移動スピード
    private Vector3 movePosition; // 移動目標位置

    [Header("魚データ設定")]
    public string fishName = "Dolfinfish";  // 魚の種類名
    public float addRate = 0.8f;            // この魚1匹あたりの倍率加算値
    public int baseScore = 100;             // 基礎スコア

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

        // ランダム移動
        if (movePosition == player.transform.position)
        {
            movePosition = moveRandomPosition();
        }

        player.transform.position = Vector3.MoveTowards(player.transform.position, movePosition, speed * Time.deltaTime);

        // 向き反転
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

    // 網に当たったときの処理
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

            Destroy(gameObject); // 魚を削除
        }
    }

    private Vector3 moveRandomPosition()
    {
        return new Vector3(Random.Range(-4f, 10f), Random.Range(-5f, 5f), speed);
    }
}
