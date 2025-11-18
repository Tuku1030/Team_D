using UnityEngine;
using TMPro;

public class ResultScoreUI : MonoBehaviour
{
    public TextMeshProUGUI resultScore;

    void Start()
    {
        // GameScoreManager からスコアを取得
        float score = GameScoreManager.Instance.GetTotalScore();

        // 表示
        resultScore.text = "Score : " + score.ToString("0");
    }
}
