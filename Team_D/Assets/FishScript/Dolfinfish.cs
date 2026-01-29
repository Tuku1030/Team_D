using UnityEngine;
using FishGame;

public class Dolfinfish : MonoBehaviour, IFish
{
    // ===== IFish =====
    public NetScoreCalculator scoreCalculator { get; set; }

    private Vector3 movePosition;
    private bool isCaptured = false;

    [Header("魚データ設定")]
    public string fishName = "Dolfinfish";
    public float addRate = 0.8f;
    public int baseScore = 100;

    void Start()
    {
        // ScoreCalculator を自動取得
        if (scoreCalculator == null)
        {
            scoreCalculator = Object.FindFirstObjectByType<NetScoreCalculator>();
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
}
