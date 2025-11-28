using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;



    private TextMeshProUGUI scoreText;
    public int totalScore = 0;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // シーンのどこにあっても"ScoreText"という名前のオブジェクトを探す
        var scoreObj = GameObject.Find("ScoreText");
        if (scoreObj != null)
        {
            scoreText = scoreObj.GetComponent<TextMeshProUGUI>();
            UpdateText();
        }
        else
        {
            Debug.Log("ScoreText が見つからない");
        }
    }
    public void AddScore(int value)
    {
        totalScore += value;
        UpdateText();
    }
    void UpdateText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score : " + totalScore;
        }
    }
}


