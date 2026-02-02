using UnityEngine;

public class bigBullettimer : MonoBehaviour
{
    public float cooldownTime = 5f;
    private float currentCooldown = 0f;

    public bool CanUse => currentCooldown <= 0f;

    void Update()
    {
        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    public void UseNet()
    {
        if (!CanUse) return;
        currentCooldown = cooldownTime;
    }

    public float GetCooldownRate()
    {
        if (cooldownTime <= 0f) return 1f;
        return 1f - Mathf.Clamp01(currentCooldown / cooldownTime);
    }
}
