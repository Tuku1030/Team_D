using UnityEngine;
using UnityEngine.UI;

public class NetCooldownUI : MonoBehaviour
{
    public bigBullettimer netCooldown;
    public Image cooldownImage;

    void Update()
    {
        cooldownImage.fillAmount =
            netCooldown.GetCooldownRate();
    }
}
