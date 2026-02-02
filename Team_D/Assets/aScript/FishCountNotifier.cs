using UnityEngine;

public class FishCountNotifier : MonoBehaviour
{
    private FishSpawner spawner;
    private FishSpawnSetting setting;
    private bool notified = false;

    // š FishSpawnSetting ‚ğó‚¯æ‚é
    public void Init(FishSpawner spawner, FishSpawnSetting setting)
    {
        this.spawner = spawner;
        this.setting = setting;
    }

    void OnDestroy()
    {
        if (notified) return;
        notified = true;

        if (spawner != null && setting != null)
        {
            spawner.OnFishRemoved(setting);
        }
    }
}
