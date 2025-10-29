using UnityEngine;
using Random = UnityEngine.Random;

public class FishSpawner : MonoBehaviour
{
    // Inspectorから設定する生成対象の魚のプレハブ
    public GameObject fishPrefab;

    [Header("生成設定")]
    // 💡 魚を生成する間隔
    public float spawnInterval = 7.0f;

    // 💡 【追加】同時にシーンに存在できる魚の最大数
    public int maxFishCount = 10;

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

        // 💡 【追加】最大数のチェック
        // 現在シーンに存在する魚（プレハブと同じ名前を持つオブジェクト）の数をカウント
        GameObject[] currentFish = GameObject.FindGameObjectsWithTag("Fish");

        if (currentFish.Length >= maxFishCount)
        {
            // 最大数に達しているため、生成をスキップ
            // Debug.Log("Max fish count reached: " + maxFishCount); // 必要に応じてログ出力
            return;
        }

        // 画面の右側5分の3のランダムな位置を取得
        Vector3 spawnPosition = GetRandomSpawnPositionInRightThreeFifths();

        // プレハブを生成（Instantiate）
        Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
    }

    // 💡 関数名を変更 (右側5分の3のワールド座標を計算する関数)
    private Vector3 GetRandomSpawnPositionInRightThreeFifths()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            // フォールバック
            return new Vector3(Random.Range(2.5f, 7f), Random.Range(-4f, 4f), 3.0f);
        }

        // --- X座標の計算 (画面の右側5分の3) ---

        // 画面全体は 1.0 です。
        // 右側 3/5 (0.6) が始まるのは、全体 1.0 から左側 2/5 (0.4) を引いた位置です。

        // 💡 修正点: ビューポートX座標の最小値を 1.0 - (3.0 / 5.0) ではなく、
        // 1.0 から 3/5 を残すので、1.0 - (2/5) = 0.4 が開始位置です。
        // 1.0 (右端) - 3.0 / 5.0 = 0.4 
        float viewportMinX = 1.0f - (3.0f / 5.0f); // 1.0 - 0.6 = 0.4

        float viewportMaxX = 1.0f; // 画面の右端
        float randomViewportX = Random.Range(viewportMinX, viewportMaxX); // 0.4から1.0の範囲
        // ... (以下 Y座標の計算は変更なし) ...

        // --- Y座標の計算 (画面の高さ全体) ---
        float viewportMinY = 0.0f;
        float viewportMaxY = 1.0f;
        float randomViewportY = Random.Range(viewportMinY, viewportMaxY);

        // ビューポート座標をワールド座標に変換
        Vector3 randomWorldPosition = mainCamera.ViewportToWorldPoint(
            new Vector3(randomViewportX, randomViewportY, 3.0f)
        );

        // Z軸を3.0fに固定して返す
        return new Vector3(randomWorldPosition.x, randomWorldPosition.y, 3.0f);
    }
}