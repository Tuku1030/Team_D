using UnityEngine;

public class PlayerExitFadeMover : MonoBehaviour
{
    public float moveSpeed = 5f;           // 進む速さ
    public float fadeDuration = 1.5f;      // 完全に消えるまでの時間
    public Vector2 direction = Vector2.right; // 右へ進む方向（左にしたいなら Vector2.left）

    private SpriteRenderer sr;
    private float timer = 0;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // ① 横へ進む
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

        // ② 徐々に透明にする
        timer += Time.deltaTime;
        float alpha = 1 - (timer / fadeDuration);

        if (sr != null)
        {
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;
        }

        // ③ 完全に消えたら削除
        if (alpha <= 0)
        {
            Destroy(gameObject);
        }
    }
}