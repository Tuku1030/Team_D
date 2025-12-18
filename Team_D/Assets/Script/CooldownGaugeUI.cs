using UnityEngine;
using UnityEngine.UI;

public class CooldownGaugeUI : MonoBehaviour
{
    [Header("参照")]
    public bigBullettimer cooldown; // クールダウン管理
    public Image gaugeImage;        // ゲージUI

    void Update()
    {
        if (cooldown == null || gaugeImage == null) return;

        // クールダウン率をUIに反映
        gaugeImage.fillAmount = cooldown.GetCooldownRate();
    }
}
