using UnityEngine;
using Random = UnityEngine.Random;
using FishGame;

public class Trash : MonoBehaviour, IFish
{
    public HeartUIController heartUI;

    public NetScoreCalculator scoreCalculator { get; set; }

    private bool damaged = false;
    private bool isCaptured = false;

    [Header("魚データ設定")]
    public string fishName = "Trash";
    public float addRate = -0.2f;
    public int baseScore = 0;

    void Start()
    {

        if (scoreCalculator == null)
        {
            scoreCalculator = Object.FindFirstObjectByType<NetScoreCalculator>();
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
