using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class NetScoreCalculator : MonoBehaviour
{
    public static NetScoreCalculator Instance;

    [Header("カウント")]
    public int fishCount;
    public int trashCount;

    [Header("スコア")]
    public int totalScore;
    private float rateBonus = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 魚を捕獲したときに呼ばれる
    /// </summary>
    public void AddCapturedFish(string fishName, float addRate, int baseScore)
    {
        Debug.Log("魚カウント増えた！");
        fishCount++;

        rateBonus += addRate;

        int addScore = Mathf.RoundToInt(baseScore * rateBonus);
        totalScore += addScore;

        Debug.Log(
            $"魚捕獲: {fishName} / +" +
            $"{addScore}点（倍率 {rateBonus:F2}）"
        );
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            NetScoreCalculator.Instance.fishCount++;
            // ★ SEは鳴らさない
        }
    }

    /// <summary>
    /// ゴミを捕獲したとき
    /// </summary>
    public void AddCapturedTrash(int penalty)
    {
        trashCount++;
        totalScore -= penalty;
    }

    /// <summary>
    /// リセット用
    /// </summary>
    public void ResetScore()
    {
        fishCount = 0;
        trashCount = 0;
        totalScore = 0;
        rateBonus = 1f;
    }
}