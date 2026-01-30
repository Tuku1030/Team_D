using UnityEngine;
using TMPro;

public class TargetScoreUI : MonoBehaviour
{
    public TMP_Text targetScoreText;
    public int targetScore = 2000;

    public Color normalColor = Color.white;
    public Color achievedColor = Color.yellow;

    private bool achieved = false;

    void Start()
    {
        targetScoreText.text = $"Goal Score : {targetScore}";
        targetScoreText.color = normalColor;
    }

    void Update()
    {
        if (achieved) return;

        if (TotalScoreManager.Instance == null) return;

        int currentScore = TotalScoreManager.Instance.GetTotalScore();

        if (currentScore >= targetScore)
        {
            achieved = true;
            targetScoreText.color = achievedColor;
        }
    }
}
