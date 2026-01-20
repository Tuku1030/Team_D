using UnityEngine;
using FishGame;

public class FishSpawner : MonoBehaviour
{
    [Header("魚ごとの生成設定（孤立インターバル）")]
    public FishSpawnSetting[] fishSettings;

    public NetScoreCalculator scoreCalculator;

    void Update()
    {
        foreach (var setting in fishSettings)
        {
            if (setting.currentCount >= setting.maxCount)
                continue;

            setting.timer += Time.deltaTime;

            if (setting.timer >= setting.spawnInterval)
            {
                Spawn(setting);
                setting.timer = 0f;
            }
        }
    }

    void Spawn(FishSpawnSetting setting)
    {
        Vector3 pos = GetRandomSpawnPositionInRightThreeFifths();
        GameObject fishObj = Instantiate(setting.prefab, pos, Quaternion.identity);

        setting.currentCount++;

        IFish fish = fishObj.GetComponent<IFish>();
        if (fish != null)
        {
            fish.scoreCalculator = scoreCalculator;
        }

        // ★ ここ重要：setting を渡す
        FishCountNotifier notifier = fishObj.AddComponent<FishCountNotifier>();
        notifier.Init(this, setting);
    }

    // ★ FishCountNotifier から呼ばれるメソッド
    public void OnFishRemoved(FishSpawnSetting setting)
    {
        setting.currentCount--;
    }

    private Vector3 GetRandomSpawnPositionInRightThreeFifths()
    {
        Camera cam = Camera.main;
        if (cam == null) return Vector3.zero;

        float x = Random.Range(0.4f, 1f);
        float y = Random.Range(0.1f, 0.9f);
        Vector3 pos = cam.ViewportToWorldPoint(new Vector3(x, y, 10f));
        pos.z = 0f;
        return pos;
    }
}
