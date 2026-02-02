using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HeartUIController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image heartIcon;       // 1個のハート画像
    [SerializeField] private TextMeshProUGUI heartText; // 「×5」と表示するテキスト

    // 現在のハート数を更新する関数
    public void UpdateHearts(int currentHeart)
    {
        if (heartIcon != null)
            heartIcon.enabled = true; // ハート画像は常に1つだけ表示

        if (heartText != null)
            heartText.text = $"× {currentHeart}"; // テキストで個数表示
    }
}
