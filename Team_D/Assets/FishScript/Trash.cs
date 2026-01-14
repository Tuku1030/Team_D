using UnityEngine;
using Random = UnityEngine.Random;
using FishGame;

public class Trash : MonoBehaviour, IFish
{
    public HeartUIController heartUI;

    public NetScoreCalculator scoreCalculator { get; set; }
    public float speed = 3f;

    private Rigidbody2D rb;
    private bool damaged = false;
    private bool isCaptured = false;

    [Header("魚データ設定")]
    public string fishName = "Trash";
    public float addRate = -0.2f;
    public int baseScore = 0;

    // 右2/3制限用
    private float leftLimit;
    private float rightLimit;
    private float topLimit;
    private float bottomLimit;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 重力なし
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        // ランダムな初期方向
        Vector2 dir = Random.insideUnitCircle.normalized;
        rb.linearVelocity = dir * speed;

        // カメラから画面範囲を取得
        Camera cam = Camera.main;
        float height = cam.orthographicSize;
        float width = height * cam.aspect;

        // 右2/3エリア
        leftLimit = -width / 3f;
        rightLimit = width;
        topLimit = height;
        bottomLimit = -height;

        if (scoreCalculator == null)
        {
            scoreCalculator = Object.FindFirstObjectByType<NetScoreCalculator>();
        }
    }

    void Update()
    {
        if (isCaptured) return;

        Vector2 pos = transform.position;
        Vector2 vel = rb.linearVelocity;

        // 左右反射
        if (pos.x <= leftLimit || pos.x >= rightLimit)
        {
            vel.x *= -1;
        }

        // 上下反射
        if (pos.y >= topLimit || pos.y <= bottomLimit)
        {
            vel.y *= -1;
        }

        rb.linearVelocity = vel;

        // 向き反転（見た目用）
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.flipX = rb.linearVelocity.x > 0;
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
