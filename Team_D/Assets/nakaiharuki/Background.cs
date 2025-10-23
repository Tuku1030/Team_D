using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    float scrollSpeed = -1f; // スクロール速度
    Vector3 cameraRectMin;

    void Start()
    {
        cameraRectMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);

        // カメラ外に出たら右端にワープ
        if (transform.position.x < (cameraRectMin.x - Camera.main.transform.position.x) * 2)
        {
            transform.position = new Vector2((Camera.main.transform.position.x - cameraRectMin.x) * 2, transform.position.y);
        }
    }
}