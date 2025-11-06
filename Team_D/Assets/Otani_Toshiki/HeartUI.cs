using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    [Header("ハートのImageを配列に入れる")]
    public Image[] hearts;        // Sceneに置いたハートImageを3つドラッグ

    [Header("ハートのスプライト")]
    public Sprite fullHeart;      // 赤ハート
    public Sprite emptyHeart;     // 空ハート

    [Header("現在の体力")]
    public int currentHealth = 3; // 体力（0～3）

    /// <summary>
    /// 体力に応じてハートを更新
    /// </summary>
    public void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }

    void Start()
    {
        UpdateHearts(); // 最初にハートを更新
    }
}
