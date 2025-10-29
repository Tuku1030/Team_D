using UnityEngine;
using Random = UnityEngine.Random;

public class FishSpawner : MonoBehaviour
{
    // Inspectorから設定する生成対象の魚のプレハブ
    public GameObject fishPrefab;

    [Header("生成設定")]
    // 💡 修正点: 魚を生成する間隔を7.0秒に設定
    public float spawnInterval = 7.0f;

    // 時間を計測するためのカウンタ変数
    private float timeElapsed = 0f;

    void Update()
    {
        // 毎フレームの時間をtimeElapsedに加算
        timeElapsed += Time.deltaTime;

        // timeElapsedが設定した間隔（7秒）を超えたら生成処理を実行
        if (timeElapsed >= spawnInterval)
        {
            SpawnSingleFish();

            // タイマーをリセット
            timeElapsed -= spawnInterval;
        }
    }

    // 魚を一つ生成する処理
    private void SpawnSingleFish()
    {
        if (fishPrefab == null)
        {
            Debug.LogError("Fish Prefabが設定されていません！Inspectorで設定してください。");
            return;
        }

        // 画面の右側3分の2のランダムな位置を取得
        Vector3 spawnPosition = GetRandomSpawnPositionInRightTwoThirds();

        // プレハブを生成（Instantiate）
        Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
    }

    // 画面の幅の右側3分の2のワールド座標を計算する関数
    private Vector3 GetRandomSpawnPositionInRightTwoThirds()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            // フォールバック
            return new Vector3(Random.Range(2.5f, 7f), Random.Range(-4f, 4f), 3.0f);
        }

        // --- X座標の計算 (画面の右側3分の2) ---
        float viewportMinX = 1.0f / 3.0f;
        float viewportMaxX = 1.0f;
        float randomViewportX = Random.Range(viewportMinX, viewportMaxX);

        // --- Y座標の計算 (画面の高さ全体) ---
        float viewportMinY = 0.0f;
        float viewportMaxY = 1.0f;
        float randomViewportY = Random.Range(viewportMinY, viewportMaxY);

        // ビューポート座標をワールド座標に変換
        // Z値は魚が動く平面に合わせて3.0fに固定（2D環境では通常Z=0が多いですが、既存の値に合わせます）
        Vector3 randomWorldPosition = mainCamera.ViewportToWorldPoint(
            new Vector3(randomViewportX, randomViewportY, 3.0f)
        );

        // Z軸を3.0fに固定して返す
        return new Vector3(randomWorldPosition.x, randomWorldPosition.y, 3.0f);
    }
}