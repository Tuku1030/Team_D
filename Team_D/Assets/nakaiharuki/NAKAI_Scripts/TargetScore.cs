using UnityEngine;
using TMPro;

public class TargetScoreUI : MonoBehaviour
{
    public TextMeshProUGUI targetText;
    public TextMeshProUGUI achievedText;

    private int needScore;
    private bool achieved = false;

    void Start()
    {
        var stageOver = FindFirstObjectByType<STAGEOverManager>();

        if (stageOver == null)
        {
            Debug.LogError("STAGEOverManager が見つかりません");
            return;
        }

        needScore = stageOver.needScore;

        targetText.text = $"目標スコア：{needScore}";
        achievedText.text = ""; // 最初は非表示
    }

    void Update()
    {
        if (achieved) return;

        int score = TotalScoreManager.Instance.GetTotalScore();

        if (score >= needScore)
        {
            achieved = true;
            achievedText.text = "目標達成！";
            achievedText.color = Color.yellow;
        }
    }
}