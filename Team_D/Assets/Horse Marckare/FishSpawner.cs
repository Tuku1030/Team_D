using UnityEngine;
using Random = UnityEngine.Random;
using FishGame;

public class FishSpawner : MonoBehaviour
{
    public GameObject[] fishPrefabs;              // 複数の魚Prefabに対応
    public NetScoreCalculator scoreCalculator;    // Scene上のScoreManagerをセット

    [Header("生成設定")]
    public float spawnInterval = 7.0f;
    public int maxFishCount = 10;

    private float timeElapsed = 0f;

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= spawnInterval)
        {
            SpawnSingleFish();
            timeElapsed -= spawnInterval;
        }
    }

    private void SpawnSingleFish()
    {
        if (fishPrefabs == null || fishPrefabs.Length == 0)
        {
            Debug.LogError("Fish Prefabが設定されていません！");
            return;
        }

        GameObject[] currentFish = GameObject.FindGameObjectsWithTag("Fish");
        if (currentFish.Length >= maxFishCount) return;

        Vector3 spawnPosition = GetRandomSpawnPositionInRightThreeFifths();

        // ランダムに魚を選ぶ
        int index = Random.Range(0, fishPrefabs.Length);
        GameObject fishObj = Instantiate(fishPrefabs[index], spawnPosition, Quaternion.identity);

        IFish fish = fishObj.GetComponent<IFish>();
        if (fish != null)
        {
            fish.scoreCalculator = scoreCalculator;
        }
        else
        {
            Debug.LogWarning("生成した魚にIFishがアタッチされていません！");
        }
    }

    private Vector3 GetRandomSpawnPositionInRightThreeFifths()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
            return new Vector3(Random.Range(2.5f, 7f), Random.Range(-4f, 4f), 3.0f);

        float viewportMinX = 1.0f - (3.0f / 5.0f);
        float viewportMaxX = 1.0f;
        float randomViewportX = Random.Range(viewportMinX, viewportMaxX);

        float randomViewportY = Random.Range(0f, 1f);

        Vector3 randomWorldPosition = mainCamera.ViewportToWorldPoint(new Vector3(randomViewportX, randomViewportY, 3.0f));
        return new Vector3(randomWorldPosition.x, randomWorldPosition.y, 3.0f);
    }
}
