using UnityEngine;
using Random = UnityEngine.Random;

public class FishRandomMove : MonoBehaviour
{
    public float speed = 3f;          // š ‹›‚²‚Æ‚ÉInspector‚Å’²®
    public GameObject target;         // ˆÚ“®‘ÎÛiŠî–{‚Í©•ªj

    private Vector3 movePosition;
    private bool isStopped = false;

    void Start()
    {
        if (target == null)
            target = gameObject;

        movePosition = GetRandomPosition();
    }

    void Update()
    {
        if (isStopped) return;

        if (target.transform.position == movePosition)
        {
            movePosition = GetRandomPosition();
        }

        target.transform.position =
            Vector3.MoveTowards(target.transform.position, movePosition, speed * Time.deltaTime);

        FlipSprite();
    }

    public void StopMove()
    {
        isStopped = true;
    }

    Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(-4f, 10f),
            Random.Range(-5f, 4f),
            0
        );
    }

    void FlipSprite()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (!sr) return;

        if (target.transform.position.x < movePosition.x)
            sr.flipX = true;
        else
            sr.flipX = false;
    }
}
