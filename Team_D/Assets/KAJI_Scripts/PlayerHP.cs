using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public GameObject HPIcon;       // HPアイコンのプレハブ
    public int maxHP = 3;            // 最大HP
    public int currentHP;            // 現在のHP

    private Image[] icons;           // 生成したアイコンを保持

    void Start()
    {
        currentHP = maxHP;
        CreateHPIcon();
        UpdateHPIcons();
    }

    private void CreateHPIcon()
    {
        icons = new Image[maxHP];
        for (int i = 0; i < maxHP; i++)
        {
            GameObject obj = Instantiate(HPIcon);
            obj.transform.SetParent(transform, false);
            icons[i] = obj.GetComponent<Image>();
        }
    }
    private void UpdateHPIcons()
    {
        for (int i = 0; i < icons.Length; i++)
        {
            if (icons[i] != null)
                icons[i].gameObject.SetActive(i < currentHP);
        }
    }

    // ダメージを受ける
    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP < 0) currentHP = 0;

        UpdateHPIcons();
    }

    // 回復する
    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP) currentHP = maxHP;

        UpdateHPIcons();
    }
}
