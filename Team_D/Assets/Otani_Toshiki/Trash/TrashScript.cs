using UnityEngine;

public class TrashScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 当たった相手が "Net" タグのオブジェクトなら
        if (collision.CompareTag("Net"))
        {
            // この袋を消す
            Destroy(gameObject);
        }
    }
}
