using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    [Header("ハートのImageを配列に入れる")]
    public Image[] hearts;        // Sceneに置いたハートImageを3つドラッグ

    [Header("ハートのスプライト")]
    public Sprite fullHeart;      // 赤ハート
    public Sprite emptyHeart;     // 空ハート

    /// <summary>
    /// 体力に応じてハートを更新
    /// </summary>
    public void UpdateHearts(int currentHealth)
    {
        if (hearts == null || hearts.Length == 0)
        {
            Debug.LogWarning("ハートが設定されていません！");
            return;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null)
                hearts[i].sprite = (i < currentHealth) ? fullHeart : emptyHeart;
        }
    }

    void Start()
    {
        // 初期表示は全回復状態
        UpdateHearts(hearts.Length);
    }
}
