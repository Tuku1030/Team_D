using UnityEngine;
using TMPro;

public class TotalScoreManager : MonoBehaviour
{
    public static TotalScoreManager Instance;

    [SerializeField] private TextMeshProUGUI totalScoreText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateTotalScore()
    {
        if (totalScoreText != null && GameScoreManager.Instance != null)
        {
            totalScoreText.text = $"Total Score: {GameScoreManager.Instance.GetTotalScore():0000}";
        }
    }
}
