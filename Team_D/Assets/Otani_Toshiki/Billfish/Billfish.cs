using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BillFish : MonoBehaviour
{
    public GameObject player;  // 移動対象
    public int speed = 5;      // 移動スピード
    Vector3 movePosition;      // 移動目標位置

    [Header("魚データ設定")]
    public string fishName = "BillFish";  // 魚の種類名（例：アジ）
    public float addRate = 0.8f;               // この魚1匹あたりの倍率加算値
    public int baseScore = 100;                 // 🔹基礎スコアを追加

    private bool isCaptured = false; // 捕獲済み判定

    void Start()
    {
        movePosition = moveRandomPosition();  //②実行時、オブジェクトの目的地を設定
    }
    void Update()
    {
        if (movePosition == player.transform.position)  //②playerオブジェクトが目的地に到達すると、
        {
            movePosition = moveRandomPosition();  //②目的地を再設定
        }
        this.player.transform.position = Vector3.MoveTowards(player.transform.position, movePosition, speed * Time.deltaTime);  //①②playerオブジェクトが, 目的地に移動, 移動速度
        // SpriteRendererコンポーネントを取得
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (player.transform.position.x < movePosition.x)
        {
            if (spriteRenderer.flipX == false)
            {
                // X軸に反転を適用
                spriteRenderer.flipX = true;
            }
        }


        if (player.transform.position.x > movePosition.x)
        {
            if (spriteRenderer.flipX == true)
            {
                // X軸に反転を適用
                spriteRenderer.flipX = false;
            }
        }
    }
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

    private Vector3 moveRandomPosition()  // 目的地を生成、xとyのポジションをランダムに値を取得 
    {
        Vector3 randomPosi = new Vector3(Random.Range(-4, 10), Random.Range(-5, 5), 5);
        return randomPosi;
    }
}