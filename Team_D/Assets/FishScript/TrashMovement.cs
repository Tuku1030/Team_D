using UnityEngine;

/// <summary>
/// 画面の「右側 2/3」だけを跳ね返りながら漂うゴミ用移動スクリプト
/// ・ランダム方向に進む
/// ・画面端で反射する
/// ・範囲はカメラ基準（Viewport）
/// </summary>
public class TrashMovement : MonoBehaviour
{
    // =============================
    // 移動スピード
    // =============================
    public float speed = 2f;

    // =============================
    // 移動方向（速度ベクトル）
    // 正規化された方向 × speed が入る
    // =============================
    private Vector2 velocity;

    // =============================
    // メインカメラ参照
    // 画面サイズ取得用
    // =============================
    private Camera cam;

    // =============================
    // スプライト反転用（向き表現）
    // =============================
    private SpriteRenderer sr;

    // =============================
    // 画面左側の制限（割合）
    // 0.33 = 画面の右 2/3
    // =============================
    [Range(0f, 1f)]
    public float leftLimit = 0.33f;

    void Start()
    {
        // メインカメラを取得
        cam = Camera.main;

        // SpriteRenderer を一度だけ取得（毎フレーム取らない）
        sr = GetComponent<SpriteRenderer>();

        // ランダムな方向ベクトルを作る
        // insideUnitCircle → 円の中のランダム値
        // normalized → 長さ1にする（方向だけ）
        velocity = Random.insideUnitCircle.normalized * speed;
    }

    void Update()
    {
        // =============================
        // ① まず現在の速度で移動
        // =============================
        transform.Translate(velocity * Time.deltaTime);

        // 現在位置を取得
        Vector3 pos = transform.position;

        // =============================
        // ② カメラからの距離（z）を計算
        // ViewportToWorldPoint に必要
        // =============================
        float z = Mathf.Abs(cam.transform.position.z - transform.position.z);

        // =============================
        // ③ 移動可能範囲を計算
        // Viewport 座標：
        // (0,0) = 左下
        // (1,1) = 右上
        // =============================

        // 左端を「画面の 1/3 の位置」に設定
        Vector3 min = cam.ViewportToWorldPoint(
            new Vector3(leftLimit, 0f, z)
        );

        // 右上は画面端
        Vector3 max = cam.ViewportToWorldPoint(
            new Vector3(1f, 1f, z)
        );

        // =============================
        // ④ X方向の反射処理
        // =============================
        if (pos.x < min.x || pos.x > max.x)
        {
            // 進行方向を反転
            velocity.x *= -1;

            // 範囲内に強制的に戻す
            pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        }

        // =============================
        // ⑤ Y方向の反射処理
        // =============================
        if (pos.y < -5 || pos.y > max.y)
        {
            // 上下方向を反転
            velocity.y *= -1;

            // 範囲内に戻す
            pos.y = Mathf.Clamp(pos.y, min.y=-5, max.y);
        }

        // =============================
        // ⑥ 最終的な位置を反映
        // =============================
        transform.position = pos;

        // =============================
        // ⑦ 見た目の向き調整（任意）
        // 進行方向に応じて左右反転
        // =============================
        if (sr != null)
        {
            sr.flipX = velocity.x > 0;
        }
    }
}
