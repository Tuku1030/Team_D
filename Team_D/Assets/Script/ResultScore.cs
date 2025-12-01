using UnityEngine;
using TMPro;

public class ResultScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultScoreText;

    void Start()
    {
        if (resultScoreText != null && GameScoreManager.Instance != null)
        {
            resultScoreText.text = $"Score: {GameScoreManager.Instance.GetTotalScore():0000}";
        }
        else
        {
            Debug.LogWarning("ResultScoreUI: TMP Ç‹ÇΩÇÕ GameScoreManager Ç™ null Ç≈Ç∑ÅI");
        }
    }
}
