using UnityEngine;

public class TotalScoreManager : MonoBehaviour
{
    public static TotalScoreManager Instance;



    private int totalScore = 0;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // ‚±‚ê‚¾‚¯‚ÅOK
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddScore(int score)
    {
        totalScore += score;
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