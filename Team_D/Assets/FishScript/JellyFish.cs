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

    void Start()
    {
        movePosition = GetRandomPosition();
    }

    void Update()
    {
        // ランダム移動
        if (Vector3.Distance(transform.position, movePosition) < 0.1f)
        {
            movePosition = GetRandomPosition();
        }

        transform.position = Vector3.MoveTowards(transform.position, movePosition, speed * Time.deltaTime);

        // 向き反転
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (transform.position.x < movePosition.x)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-4, 10), Random.Range(-5, 5), 1);
    }

    private bool damaged = false; // 一度だけダメージを入れるフラグ

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCaptured || damaged) return;

        if (other.CompareTag("BigNet"))
        {
            damaged = true;
            isCaptured = true;

            PlayerController playerHP = FindObjectOfType<PlayerController>();
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
