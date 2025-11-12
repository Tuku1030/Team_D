using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class TrashSpawner : MonoBehaviour
{
    [Header("生成するTrashプレハブ")]
    public GameObject[] trashPrefabs; // Trashを全部ここに入れる

    [Header("生成設定")]
    public float spawnInterval = 7f;  // 生成間隔（秒）

    // タグごとの最大数を設定
    private Dictionary<string, int> tagLimits = new Dictionary<string, int>()
    {
        { "JellyFIish", 3 },
        { "Trash", 5 },
        { "Can", 3 },
    };

    private float timeElapsed = 0f;

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= spawnInterval)
        {
            SpawnTrash();
            timeElapsed = 0f;
        }
    }

    private void SpawnTrash()
    {
        if (trashPrefabs == null || trashPrefabs.Length == 0)
        {
            Debug.LogError("Trash Prefab が設定されていません！");
            return;
        }

        // ランダムで生成するPrefabを選ぶ
        int index = Random.Range(0, trashPrefabs.Length);
        GameObject prefab = trashPrefabs[index];
        string tag = prefab.tag;

        // タグごとの上限を取得
        int limit = tagLimits.ContainsKey(tag) ? tagLimits[tag] : 5; // デフォルト5

        // 現在の数をカウント
        int currentCount = GameObject.FindGameObjectsWithTag(tag).Length;

        // 上限に達していたら生成スキップ
        if (currentCount >= limit)
        {
            Debug.Log($"{tag} は上限 {limit} に達しているため生成スキップ");
            return;
        }

        // 生成位置
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // 生成
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
            return new Vector3(Random.Range(2f, 7f), Random.Range(-4f, 4f), 0f);

        float viewportMinX = 0.4f; // 画面右三分の二に出す場合
        float viewportMaxX = 1f;
        float randomViewportX = Random.Range(viewportMinX, viewportMaxX);
        float randomViewportY = Random.Range(0f, 1f);

        Vector3 randomWorldPosition =
            mainCamera.ViewportToWorldPoint(new Vector3(randomViewportX, randomViewportY, 10f));
        return new Vector3(randomWorldPosition.x, randomWorldPosition.y, 0f);
    }
}
