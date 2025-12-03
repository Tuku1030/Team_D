using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public GameObject trash1;
    public GameObject trash2;
    public GameObject trash3;
    public GameObject trash4;

    [Header("設定")]
    public float spawnInterval = 5f;  // 5秒に1回
    public int maxTrashCount = 10;    // 最大数

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            TrySpawnTrash();
            timer = 0f;
        }
    }

    void TrySpawnTrash()
    {
        // すでに存在しているごみの数を数える
        int currentTrash = GameObject.FindGameObjectsWithTag("Trash").Length;

        if (currentTrash >= maxTrashCount)
        {
            // 最大数に達しているので生成しない
            return;
        }

        // ランダム選択
        int r = Random.Range(1, 5);
        GameObject prefabToSpawn = null;
        if (r == 1) prefabToSpawn = trash1;
        if (r == 2) prefabToSpawn = trash2;
        if (r == 3) prefabToSpawn = trash3;
        if (r == 4) prefabToSpawn = trash4;

        // カメラの右側3分の2のランダム位置
        Camera cam = Camera.main;
        float randomX = Random.Range(0.33f, 1f); // 右側3分の2
        float randomY = Random.Range(0f, 1f);    // 上下全体
        Vector3 viewportPos = new Vector3(randomX, randomY, cam.nearClipPlane + 1f); // zはカメラから少し前
        Vector3 worldPos = cam.ViewportToWorldPoint(viewportPos);

        Instantiate(prefabToSpawn, worldPos, Quaternion.identity);
    }
}