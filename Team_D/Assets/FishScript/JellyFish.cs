using UnityEngine;
using Random = UnityEngine.Random;

public class JellyFish : MonoBehaviour
{
    [Header("移動設定")]
    public float speed = 3f;
    private Vector3 movePosition;

    [Header("移動範囲")]
    public HeartUI heartUI; // JellyFish スクリプト内
    private bool isCaptured = false;
    private bool damaged = false; // 一度だけダメージを入れるフラグ

    void Start()
    {
        movePosition = GetRandomPosition();
    }

    void Update()
    {
        if (isCaptured) return; // 捕獲済みなら動かさない

        // ランダム移動
        if (Vector3.Distance(transform.position, movePosition) < 0.1f)
        {
            movePosition = GetRandomPosition();
        }

        transform.position = Vector3.MoveTowards(transform.position, movePosition, speed * Time.deltaTime);

        // 向き反転
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (transform.position.x < movePosition.x && !spriteRenderer.flipX)
            spriteRenderer.flipX = true;
        else if (transform.position.x > movePosition.x && spriteRenderer.flipX)
            spriteRenderer.flipX = false;
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-4f, 10f), Random.Range(-5f, 5f), 1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCaptured || damaged) return;

        if (other.CompareTag("BigNet"))
        {
            damaged = true;
            isCaptured = true;

            // PlayerController を取得してダメージ
            PlayerController playerHP = Object.FindFirstObjectByType<PlayerController>();
            if (playerHP != null)
            {
                playerHP.TakeDamage(1);
            }

            Destroy(gameObject);
        }
        else if (other.CompareTag("Net"))
        {
            isCaptured = true;
            Destroy(gameObject);
        }
    }
}
