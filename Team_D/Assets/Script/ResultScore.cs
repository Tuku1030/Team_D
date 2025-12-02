using UnityEngine;
using TMPro;

public class ResultScore : MonoBehaviour
{
    public TextMeshProUGUI resultScoreText;



    void Start()
    {
        resultScoreText.text =
            "Result: " + TotalScoreManager.Instance.GetTotalScore();
        // 次プレイのためにリセット
        TotalScoreManager.Instance.ResetScore();
    }
}