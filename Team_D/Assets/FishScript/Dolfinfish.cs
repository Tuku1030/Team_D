using UnityEngine;
using FishGame;

public class Dolfinfish : MonoBehaviour, IFish
{
    // ===== IFish =====
    public NetScoreCalculator scoreCalculator { get; set; }

    [Header("魚データ設定")]
    public string fishName = "Dolfinfish";
    public float addRate = 0.8f;
    public int baseScore = 100;

    private bool isCaptured = false;
    private CaptureMoveEffect captureEffect;
    private Transform playerTransform;

    void Awake()
    {
        captureEffect = GetComponent<CaptureMoveEffect>();
    }

    void Start()
    {
        if (scoreCalculator == null)
        {
            scoreCalculator = Object.FindFirstObjectByType<NetScoreCalculator>();
        }

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

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

            if (captureEffect != null && playerTransform != null)
            {
                captureEffect.Play(playerTransform);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
