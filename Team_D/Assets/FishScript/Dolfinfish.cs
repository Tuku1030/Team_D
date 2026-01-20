using UnityEngine;
using Random = UnityEngine.Random;
using FishGame;

public class Dolfinfish : MonoBehaviour, IFish
{
    // ===== IFish =====
    public NetScoreCalculator scoreCalculator { get; set; }

    [Header("移動設定")]
    public float speed = 6f;

    private Vector3 movePosition;
    private bool isCaptured = false;

    [Header("魚データ設定")]
    public string fishName = "Dolfinfish";
    public float addRate = 0.8f;
    public int baseScore = 100;

    void Start()
    {
        // 最初の目的地を決める
        movePosition = GetRandomPosition();

        // ScoreCalculator を自動取得
        if (scoreCalculator == null)
        {
            scoreCalculator = Object.FindFirstObjectByType<NetScoreCalculator>();
        }
    }

    void Update()
    {
        if (isCaptured) return;

        // 目的地に近づいたら次の目的地へ
        if (Vector3.Distance(transform.position, movePosition) < 0.1f)
        {
            movePosition = GetRandomPosition();
        }

        // 自分自身を移動（← ここ重要！）
        transform.position = Vector3.MoveTowards(
            transform.position,
            movePosition,
            speed * Time.deltaTime
        );

        // 向き調整
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.flipX = movePosition.x > transform.position.x;
        }
    }

    // 網に当たったとき
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

            Destroy(gameObject);
        }
    }

    // ランダムな移動先（画面内）
    private Vector3 GetRandomPosition()
    {
        Camera cam = Camera.main;
        if (cam == null)
        {
            return new Vector3(
                Random.Range(-4f, 10f),
                Random.Range(-5f, 5f),
                0f
            );
        }

        float x = Random.Range(0.4f, 1.0f); // 右寄り
        float y = Random.Range(0.1f, 0.9f);

        Vector3 worldPos = cam.ViewportToWorldPoint(new Vector3(x, y, 10f));
        worldPos.z = 0f;
        return worldPos;
    }
}
