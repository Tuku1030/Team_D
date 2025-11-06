using UnityEngine;

public class Bullet : MonoBehaviour
{
    // “G‚Æ“–‚½‚Á‚½‚ç’e‚ğÁ‚·
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trash"))
        {
            Destroy(gameObject);       // ’e‚ğÁ‚·
            Destroy(other.gameObject); // “G‚àÁ‚·
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject); // ‰æ–ÊŠO‚Éo‚½‚ç’e‚ğÁ‚·
    }
}
