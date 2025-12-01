using UnityEngine;
using TMPro;

public class ResultScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultScoreText; // Inspector でドラッグ



    void Start()
    {
        if (resultScoreText != null && TotalScoreManager.Instance != null)
        {
            // TotalScoreManager から合計スコアを取得して表示
            int totalScore = TotalScoreManager.Instance.GetTotalScore();
            resultScoreText.text = $"Result: {totalScore:0000}";
            Debug.Log($"Result画面スコア表示: {totalScore}");
            // 次のゲームのためにリセット
            TotalScoreManager.Instance.ResetScore();
        }
        else
        {
            if (resultScoreText == null)
                Debug.LogWarning(" ResultScoreText が Inspector にセットされていません！");
            if (TotalScoreManager.Instance == null)
                Debug.LogWarning(" TotalScoreManager.Instance が null です！");
        }
    }
}
