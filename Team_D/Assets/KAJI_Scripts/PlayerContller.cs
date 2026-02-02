using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("移動設定")]
    public float Speed = 5f;  // スピード（小さすぎたので5くらいが一般的）
    private Rigidbody2D Rbody;

    [Header("HP設定")]
    public int maxHP = 5;
    public int currentHP;
    public HeartUIController heartUI;  // InspectorでHeartUIをアタッチ

    [Header("サウンド")]
    public AudioSource audioSource;
    public AudioClip damageSE;

    [Header("ダメージ演出")]
    public float invincibleTime = 1.0f;   // 無敵時間
    public float blinkInterval = 0.1f;    // 点滅間隔

    private bool isInvincible = false;
    private float invincibleTimer = 0f;
    private float blinkTimer = 0f;

    private SpriteRenderer spriteRenderer;


    void Start()
    {
        Rbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // ★
        currentHP = maxHP;
        UpdateUI();
        transform.position = new Vector3(-8f, -0f, 0f);
    }


    void Update()
    {
        MovePlayer();
        ClampPosition();

        HandleInvincible(); // ★追加
    }


    private void MovePlayer()
    {
        Vector2 position = transform.position;

        if (Input.GetKey(KeyCode.W)) position.y += Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) position.y -= Speed * Time.deltaTime;

        transform.position = position;
    }

    private void ClampPosition()
    {
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, -9.5f, 3.0f),
            Mathf.Clamp(transform.position.y, -3.8f, 3.5f)
        );
    }

    // HP操作
    public void TakeDamage(int amount)
    {
        if (isInvincible) return; // ★ 無敵中は無視

        currentHP -= amount;
        if (currentHP < 0) currentHP = 0;

        // ダメージSE
        if (audioSource != null && damageSE != null)
        {
            audioSource.PlayOneShot(damageSE);
        }

        UpdateUI();

        // ★ 無敵＆点滅スタート
        isInvincible = true;
        invincibleTimer = invincibleTime;
        blinkTimer = 0f;

        if (currentHP <= 0)
            GameOver();
    }


    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP) currentHP = maxHP;

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (heartUI != null)
            heartUI.UpdateHearts(currentHP);
        else
            Debug.LogWarning("HeartUI がセットされていません！");
    }

    private void GameOver()
    {
        GameOverManager.GameOver();
        // SceneManager.LoadScene("GameOverScene");
    }

    private void HandleInvincible()
    {
        if (!isInvincible) return;

        invincibleTimer -= Time.deltaTime;
        blinkTimer -= Time.deltaTime;

        // 点滅
        if (blinkTimer <= 0f)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            blinkTimer = blinkInterval;
        }

        // 無敵終了
        if (invincibleTimer <= 0f)
        {
            isInvincible = false;
            spriteRenderer.enabled = true; // 必ず表示状態に戻す
        }
    }


    public int GetPlayerHP()
    {
        return currentHP;
    }
}
