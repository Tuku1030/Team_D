using UnityEngine;

public class TrashMovement : MonoBehaviour
{
    public float speed = 3f;
    private Vector2 velocity;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        // ランダムな初速（一直線）
        velocity = Random.insideUnitCircle.normalized * speed;
    }

    void Update()
    {
        Vector3 pos = transform.position;

        // カメラの画面範囲を取得
        Vector3 min = cam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 max = cam.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));

        // X反射
        if (pos.x < min.x || pos.x > max.x)
        {
            velocity.x *= -1;
            pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        }

        // Y反射（★ここが重要）
        if (pos.y < min.y || pos.y > max.y)
        {
            velocity.y *= -1;
            pos.y = Mathf.Clamp(pos.y, min.y, max.y);
        }

        transform.position = pos;
        transform.Translate(velocity * Time.deltaTime);

        // 向き（任意）
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.flipX = velocity.x > 0;
        }
    }
}
