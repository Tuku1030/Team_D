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

        // カメラの右側3分の2のランダム位置
        Camera cam = Camera.main;
        float randomX = Random.Range(0.33f, 1f); // 右側3分の2
        float randomY = Random.Range(0f, 1f);    // 上下全体
        Vector3 viewportPos = new Vector3(randomX, randomY, cam.nearClipPlane + 1f);
        Vector3 worldPos = cam.ViewportToWorldPoint(viewportPos);

        // 生成
        Instantiate(jellyPrefab, worldPos, Quaternion.identity);
    }

}
