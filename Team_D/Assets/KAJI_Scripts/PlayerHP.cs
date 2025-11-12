using UnityEngine;
using UnityEngine.SceneManagement;  // ← 追加
public class PlayerHP : MonoBehaviour
{
    [Header("最大HP")]
    public int maxHP = 3;
    [Header("現在のHP")]
    public int currentHP;

    [Header("Heart UIスクリプト")]
    public HeartUI heartUI;  // InspectorでHeartUIをアタッチ

    void Start()
    {
        currentHP = maxHP;
        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP < 0) currentHP = 0;

        UpdateUI();

        if (currentHP == 0)
        {
            GameOver();
        }
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP) currentHP = maxHP;

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (heartUI != null)
        {
            // OKな書き方
            heartUI.UpdateHearts(currentHP);
        }
        else
        {
            Debug.LogWarning("HeartUI がセットされていません！");
        }
    }
    private void GameOver()
    {
        // "GameOverScene" はあなたのゲームオーバーシーンの名前に置き換えてください
        SceneManager.LoadScene("GameOver");
    }
}
