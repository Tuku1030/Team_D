using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public GameObject trash1;
    public GameObject trash2;
    public GameObject trash3;

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
        int r = Random.Range(1, 4);

        GameObject prefabToSpawn = null;
        if (r == 1) prefabToSpawn = trash1;
        if (r == 2) prefabToSpawn = trash2;
        if (r == 3) prefabToSpawn = trash3;

        Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
    }
}
