using UnityEngine;
using Random = UnityEngine.Random;

public class FishSpawner : MonoBehaviour
{
    public GameObject fishPrefab;                   // 生成する魚のPrefab
    public NetScoreCalculator scoreCalculator;     // Scene上のScoreManagerをセット

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
        if (fishPrefab == null)
        {
            Debug.LogError("Fish Prefabが設定されていません！");
            return;
        }

        GameObject[] currentFish = GameObject.FindGameObjectsWithTag("Fish");
        if (currentFish.Length >= maxFishCount) return;

        Vector3 spawnPosition = GetRandomSpawnPositionInRightThreeFifths();

        GameObject fishObj = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);

        // ⚡ 生成した魚に ScoreManager をセット
        Sardine sardine = fishObj.GetComponent<Sardine>();
        if (sardine != null)
            sardine.scoreCalculator = scoreCalculator;
        else
            Debug.LogWarning("生成した魚にSardineスクリプトがアタッチされていません！");
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
