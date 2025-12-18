using UnityEngine;
using TMPro;

public class ScoreTMPUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;

    void Update()
    {
        if (TotalScoreManager.Instance == null) return;

        scoreText.text = "Score: " + TotalScoreManager.Instance.totalScore.ToString();
    }
}
