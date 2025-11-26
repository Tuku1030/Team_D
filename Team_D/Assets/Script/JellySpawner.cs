using UnityEngine;

public class JellySpawner : MonoBehaviour
{
    public GameObject jellyPrefab;

    [Header("設定")]
    public float spawnInterval = 5f;
    public int maxJellyCount = 3;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            TrySpawnJelly();
            timer = 0f;
        }
    }

    void TrySpawnJelly()
    {
        // 今いるクラゲの数
        int current = GameObject.FindGameObjectsWithTag("JellyFish").Length;

        if (current >= maxJellyCount) return;

        // 生成
        Instantiate(jellyPrefab, transform.position, Quaternion.identity);
    }
}
