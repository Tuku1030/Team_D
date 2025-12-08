using UnityEngine;

public class CollectEffectController : MonoBehaviour
{
    public Transform player;         // プレイヤーを Inspector でセット
    public float moveDuration = 2f;  // プレイヤーに到着する時間
    public AudioClip collectSound;   // 効果音
    public ParticleSystem sparkleFX; // きらきらエフェクト

    private Vector3 startScale;
    private Vector3 startPos;
    private float timer = 0f;
    private AudioSource audioSource;

    void Start()
    {
        startPos = transform.position;
        startScale = transform.localScale;

        // 音を鳴らす
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(collectSound);

        // パーティクル再生
        if (sparkleFX != null)
            sparkleFX.Play();
    }

    void Update()
    {
        if (player == null) return;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / moveDuration);

        // プレイヤーへ移動（Lerp）
        transform.position = Vector3.Lerp(startPos, player.position, t);

        // サイズを小さくする
        transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);

        // 完了（サイズ0到達）
        if (t >= 1f)
        {
            Destroy(gameObject);
        }
    }
}
