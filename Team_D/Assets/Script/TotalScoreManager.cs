using UnityEngine;
using UnityEngine.SceneManagement;

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

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            //  ステージシーンなら必ずスコア初期化
            if (scene.name.StartsWith("Stage"))
            {
                totalScore = 0;

                // ステージ開始時初期化
                StageClearManager.isClear = false;

                GameOverManager.isGameOver = false;

            }
        }


    }
    void OnEnable()
    {
        totalScore = 0;
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
