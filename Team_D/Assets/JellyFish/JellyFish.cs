using UnityEngine;
using Random = UnityEngine.Random;

public class JellyFish : MonoBehaviour
{
    [Header("移動設定")]
    public float speed = 3f;
    private Vector3 movePosition;

    [Header("移動範囲")]
    public float minX = -4f, maxX = 10f, minY = -5f, maxY = 5f;
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
        return new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCaptured) return;

        // BigNet に当たったら HP 減らしてクラゲ消す
        if (other.CompareTag("BigNet"))
        {
            PlayerHP playerHP = other.GetComponent<PlayerHP>();
            if (playerHP == null)
            {
                // BigNet が PlayerHP を持っていない場合はシーン内から探す
                playerHP = FindObjectOfType<PlayerHP>();
            }

            if (playerHP != null)
            {
                playerHP.TakeDamage(1);
            }

            isCaptured = true;
            Destroy(gameObject);
        }

        // Net に当たったらクラゲ消すだけ
        else if (other.CompareTag("Net"))
        {
            isCaptured = true;
            Destroy(gameObject);
        }
    }
}
