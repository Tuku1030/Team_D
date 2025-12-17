using UnityEngine;

public class TotalScoreManager : MonoBehaviour
{
    public static TotalScoreManager Instance;

    public int totalScore = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // これだけでOK
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // HP情報も受け取るAddScore
    public PlayerController player; // Inspectorでアタッチ

    public void AddScore(int score)
    {
        int finalScore = score;
        if (player.currentHP >= player.maxHP)
        {
            finalScore *= 2;
        }
        totalScore += finalScore;
        Debug.Log("TOTAL SCORE = " + totalScore);
    }


    public int GetTotalScore()
    {
        return totalScore;
    }

    public void ResetScore()
    {
        totalScore = 0;
    }
}
