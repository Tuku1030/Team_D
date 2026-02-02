using UnityEngine;

public class CaptureMoveEffect : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float minDistance = 0.05f; // 到達判定

    private bool isActive = false;
    private Transform target;

    private Vector3 startScale;
    private float startDistance;

    public void Play(Transform player)
    {
        target = player;
        isActive = true;

        startScale = transform.localScale;
        startDistance = Vector3.Distance(transform.position, target.position);

        // 当たり判定＆物理停止（省略可）
        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        var rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }
    }

    void Update()
    {
        if (!isActive || target == null) return;

        float currentDistance = Vector3.Distance(transform.position, target.position);

        // 距離割合（1 → 0）
        float t = Mathf.Clamp01(currentDistance / startDistance);

        // ★ 距離に同期して縮小
        transform.localScale = startScale * t;

        // 移動（プレイヤーは動くので毎フレーム）
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime
        );

        // 到達したら消す
        if (currentDistance <= minDistance)
        {
            transform.localScale = Vector3.zero; // 念のため
            Destroy(gameObject);
        }
    }
}
