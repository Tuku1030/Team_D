using UnityEngine;
using Random = UnityEngine.Random;
using FishGame;

public class FishSpawner : MonoBehaviour
{
    int fishtimer;

    [Header("生成する魚たち")]
    public GameObject[] fishPrefabs;              // 複数の魚Prefabに対応
    public NetScoreCalculator scoreCalculator;    // Scene上のScoreManagerをセット

    [Header("スポーン設定")]
    [Tooltip("このSpawnerでの生成間隔（秒）")]
    public float spawnInterval = 7.0f;

    [Tooltip("このSpawnerで同時に生成できる魚の上限")]
    public int maxFishCount = 10;
    private float timeElapsed = 0f;

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= spawnInterval)
        {
            SpawnSingleFish();
            timeElapsed = 0f; // 安全策として -= よりも0にリセット
        }
    }

    private void SpawnSingleFish()
    {
        if (fishPrefabs == null || fishPrefabs.Length == 0)
        {
            Debug.LogError($"{name} に Fish Prefab が設定されていません！");
            return;
        }

        GameObject[] currentFish = GameObject.FindGameObjectsWithTag("Fish");


        if (currentFish.Length >= maxFishCount) return;

        Vector3 spawnPosition = GetRandomSpawnPositionInRightThreeFifths();

        int index = Random.Range(0, fishPrefabs.Length);
        GameObject fishObj = Instantiate(fishPrefabs[index], spawnPosition, Quaternion.identity);
        // スコア設定
        IFish fish = fishObj.GetComponent<IFish>();
        if (fish != null)
        {
            fish.scoreCalculator = scoreCalculator;
        }

    }

    private Vector3 GetRandomSpawnPositionInRightThreeFifths()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
            return new Vector3(Random.Range(2.5f, 7f), Random.Range(-4f, 4f), 0f); // Z = 0 に修正（2D用）

        float viewportMinX = 1.0f - (3.0f / 5.0f);
        float viewportMaxX = 1.0f;
        float randomViewportX = Random.Range(viewportMinX, viewportMaxX);

        float randomViewportY = Random.Range(0f, 1f);

        Vector3 randomWorldPosition = mainCamera.ViewportToWorldPoint(new Vector3(randomViewportX, randomViewportY, 10f));
        return new Vector3(randomWorldPosition.x, randomWorldPosition.y, 0f); // Z = 0 に統一
    }
}
