using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    [Header("弾の設定")]
    public GameObject Bullet;      // 小網の弾プレハブ
    public GameObject BigBullet;   // 巨大網の弾プレハブ
    public float Speed = 10f;      // 弾の速度



    [Header("効果音")]
    public AudioClip bulletSound;     // 小網の発射音
    public AudioClip bigBulletSound;  // 巨大網の発射音
                                      // 内部変数
    private AudioSource audioSource;  // 効果音再生用
    private float bulletTimer;        // 小網用タイマー
    public float bigBulletTimer;
    // 追加！
    public bigBullettimer bigBulletCooldown;// 巨大網用タイマー
                                            // 発射位置オフセット（必要ならInspectorで調整可能）
    public Vector3 BulletPoint = Vector3.zero;
    public Vector3 BigBulletPoint = Vector3.zero;
    void Start()
    {
        // AudioSourceがなければ自動で追加
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    void Update()
    {
        bulletTimer += Time.deltaTime;
        bigBulletTimer += Time.deltaTime;
        // マウス位置を取得（2D座標）
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // --- 小網発射（左クリック） ---
        if (Input.GetMouseButtonDown(0) && bulletTimer > 1.0f)
        {
            FireBullet(Bullet, mousePos, bulletSound);
            bulletTimer = 0f;
        }
        // --- 巨大網発射（スペースキー） ---
        // --- 巨大網発射（スペースキー） ---
        if (Input.GetKeyDown(KeyCode.Space) && bigBulletCooldown.CanUse)
        {
            FireBigBullet(BigBullet, mousePos, bigBulletSound);
            bigBulletCooldown.UseNet();
        }

    }
    /// <summary>
    /// 小網を発射する処理
    /// </summary>
    private void FireBullet(GameObject bulletPrefab, Vector2 targetPos, AudioClip sound)
    {
        // 弾生成
        GameObject bullet = Instantiate(bulletPrefab, transform.position + BulletPoint, Quaternion.identity);



        // 発射方向計算
        Vector2 angle = (targetPos - (Vector2)transform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().linearVelocity = angle * Speed;
        // 効果音再生
        PlaySound(sound);
        // 時間で自動削除（2.5秒後）
        Destroy(bullet, 2.5f);
    }
  
    /// <summary>
    /// 巨大網を発射する処理
    /// </summary>
    private void FireBigBullet(GameObject bigBulletPrefab, Vector2 targetPos, AudioClip sound)
    {
        GameObject bigBullet = Instantiate(bigBulletPrefab, transform.position + BigBulletPoint, Quaternion.identity);
        PlaySound(sound);
        
        // すぐ消す設定（必要なら残してOK）
         Destroy(bigBullet, 0.2f);
    }
    /// <summary>
    /// 効果音を再生する
    /// </summary>
    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }



}
public class bigBullettimer : MonoBehaviour
{
    public float cooldownTime = 5f;   // CT秒数
    private float currentCooldown = 0f;

    public bool CanUse => currentCooldown <= 0f;

    void Update()
    {
        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    public void UseNet()
    {
        if (!CanUse) return;

        // 🕸️ 網を出す処理
        Debug.Log("Net Used!");

        currentCooldown = cooldownTime;
    }

    public float GetCooldownRate()
    {
        return Mathf.Clamp01(currentCooldown / cooldownTime);
    }
}