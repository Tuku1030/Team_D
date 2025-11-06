using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using FishGame;

public interface IFish
{
    NetScoreCalculator scoreCalculator { get; set; }
}

public class JellyFish : MonoBehaviour
{
    public GameObject player;  // 移動対象
    public int speed = 3;      // 移動スピード
    Vector3 movePosition;      // 移動目標位置

    [Header("魚データ設定")]
    public string fishName = "JellyFish";
    public float addRate = -0.2f;
    public int baseScore = 0;

    private bool isCaptured = false;

    [Header("HP設定")]
    public int maxHP = 3;        // 最大体力
    public int currentHP;        // 現在体力

    [Header("ハートUI")]
    public HeartUI heartUI;      // ハートUIスクリプトをアタッチ

    void Start()
    {
        movePosition = moveRandomPosition();
        currentHP = maxHP;
        if (heartUI != null)
        {
            heartUI.currentHealth = currentHP;
            heartUI.UpdateHearts();
        }
    }

    void Update()
    {
        if (movePosition == player.transform.position)
        {
            movePosition = moveRandomPosition();
        }

        this.player.transform.position = Vector3.MoveTowards(player.transform.position, movePosition, speed * Time.deltaTime);

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (player.transform.position.x < movePosition.x)
            spriteRenderer.flipX = true;
        else if (player.transform.position.x > movePosition.x)
            spriteRenderer.flipX = false;
    }

    // HPを減らす関数
    public void TakeDamage(int damage)
    {
        if (isCaptured) return;

        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;

        if (heartUI != null)
        {
            heartUI.currentHealth = currentHP;
            heartUI.UpdateHearts();
        }

        if (currentHP == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isCaptured = true;

        // スコア計算
        NetScoreCalculator scoreCalculator = FindObjectOfType<NetScoreCalculator>();
        if (scoreCalculator != null)
        {
            scoreCalculator.AddCapturedFish(fishName, addRate, baseScore);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCaptured) return;

        if (other.CompareTag("BigNet"))
        {
            TakeDamage(1); // HPを1減らす
            isCaptured = true;
            Destroy(gameObject); // 即削除
        }

        if (other.CompareTag("Net"))
        {
            isCaptured = true;
            Destroy(gameObject); // 即削除
        }
    }

    private Vector3 moveRandomPosition()
    {
        return new Vector3(Random.Range(-4, 10), Random.Range(-5, 5), 1);
    }
}
