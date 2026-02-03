using Unity.VisualScripting;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    [Header("弾の設定")]
    public GameObject Bullet;        // 小網の弾プレハブ
    public GameObject BigBullet;     // 巨大網の弾プレハブ
    public float Speed = 10f;        // 弾の速度

    [Header("発射位置オフセット")]
    public Vector3 BulletPoint = Vector3.zero;
    public Vector3 BigBulletPoint = Vector3.zero;

    [Header("効果音（発射）")]
    public AudioClip bulletSound;     // 小網の発射音
    public AudioClip bigBulletSound;  // 大網の発射音

    [Header("クールダウン")]
    public float bulletCooldownTime = 1.0f;   // 小網CT
    public bigBullettimer bigBulletCooldown;  // 巨大網CT（別スクリプト）

    [Header("SE再生用（別オブジェクト）")]
    public AudioSource seAudioSource; // ★ここに別オブジェクトのAudioSourceをドラッグ


    // 内部
    private float bulletTimer = 0f;

    void Start()
    {
        if (seAudioSource == null)
        {
            Debug.LogError("SE用AudioSourceがInspectorで設定されていません！");
        }

        if (bigBulletCooldown == null)
        {
            Debug.LogError("bigBulletCooldown が Inspector で設定されていません！");
        }
    }

    void Update()
    {
        bulletTimer += Time.deltaTime;

        // マウス位置（ワールド座標）
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // ----------------------
        // 小網（左クリック）
        // ----------------------
        if (Input.GetMouseButtonDown(0) && bulletTimer >= bulletCooldownTime)
        {
            FireBullet(Bullet, mousePos);
            bulletTimer = 0f;
        }

        // ----------------------
        // 巨大網（スペースキー）
        // ----------------------
        if (Input.GetKeyDown(KeyCode.Space) &&
            bigBulletCooldown != null &&
            bigBulletCooldown.CanUse)
        {
            FireBigBullet(BigBullet);
            bigBulletCooldown.UseNet();
        }
    }

    /// <summary>
    /// 小網を発射
    /// </summary>
    private void FireBullet(GameObject bulletPrefab, Vector2 targetPos)
    {
        if (bulletPrefab == null) return;

        GameObject bullet = Instantiate(
            bulletPrefab,
            transform.position + BulletPoint,
            Quaternion.identity
        );

        Vector2 dir = (targetPos - (Vector2)transform.position).normalized;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = dir * Speed;
        }

        PlaySound(bulletSound);
        Destroy(bullet, 2.5f);
    }

    /// <summary>
    /// 巨大網を発射（結果音は別で鳴る）
    /// </summary>
    private void FireBigBullet(GameObject bigBulletPrefab)
    {
        if (bigBulletPrefab == null) return;

        GameObject bigBullet = Instantiate(
            bigBulletPrefab,
            transform.position + BigBulletPoint,
            Quaternion.identity
        );

        PlaySound(bigBulletSound);
        // 網は短時間で消える想定
        Destroy(bigBullet, 0.2f);
    }

    /// <summary>
    /// 効果音再生（別オブジェクトのAudioSource）
    /// </summary>
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && seAudioSource != null)
        {
            seAudioSource.PlayOneShot(clip);
        }
    }
}
